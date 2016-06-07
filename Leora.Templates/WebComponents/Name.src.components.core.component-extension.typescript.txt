var originalAngularModule = angular.module;
var componentStyles = {};

angular.module = function () {
    var m = originalAngularModule.apply(this, arguments);
    m.component = function (component) {
        var options = component.config;
        var styles;

        if (options.selector) {
            var componentNameCamelCase = options.selector.replace(/-([a-z])/g, (g) => {
                return g[1].toUpperCase();
            });
        }

        if (options.component.canActivate) {
            m.config(["routeResolverServiceProvider", function (routeResolverServiceProvider) {
                routeResolverServiceProvider.configure({
                    routeTemplate: `<${options.selector}></${options.selector}>`,
                    route: options.route,
                    routes: options.routes,
                    key: options.key,
                    promise: options.component.canActivate()
                });
            }
            ]);
        }

        if (options.component.prototype.canActivate) {
            m.config(["routeResolverServiceProvider", function (routeResolverServiceProvider) {
                routeResolverServiceProvider.configure({
                    routeTemplate: `<${options.selector}></${options.selector}>`,
                    route: options.route,
                    routes: options.routes,
                    key: options.key,
                    promise: options.component.prototype.canActivate()
                });
            }
            ]);
        }

        
        var directiveDefinitionObject: any = {
            restrict: options.restrict || "E",
            template: angular.isArray(options.template) ? options.template.join(" \n ") : options.template,
            templateUrl: options.templateUrl,
            replace: options.replace || true,
            scope: angular.isUndefined(options.scope) ? {} : options.scope,
            bindToController: options.bindToController || {},
            transclude: options.transclude,
            controllerAs: "vm",
            require: options.require,
            controller: componentNameCamelCase + "Component"
        }

        options.component.$inject = options.viewProviders

        if (options.inputs && options.inputs.length > 0) {
            for (var i = 0; i < options.inputs.length; i++) {
                if (options.inputs[i].substring(0, 2) === "on") {
                    directiveDefinitionObject.bindToController[options.inputs[i]] = "&";
                } else {
                    directiveDefinitionObject.bindToController[options.inputs[i]] = "=";
                }

            }
        }

        if ((options.component && options.component.styles) || options.styles) {
            styles = options.styles ? options.styles : options.component.styles;
            styles = angular.isArray(styles) ? styles.join(" \n ") : styles;
        }

        directiveDefinitionObject.compile = function (template: angular.IAugmentedJQuery) {
            return {
                pre: function (scope, element, attributes, controller, transcludeFn) {
                    if (options.transclude)
                        transcludeFn(scope, function (clone) {

                        });

                    if (styles && !componentStyles[options.selector]) {
                        componentStyles[options.selector] = true;

                        function addStyleTagToHead() {
                            var style = document.createElement("style");
                            style.setAttribute("data-selector", options.selector)
                            style.appendChild(document.createTextNode(styles));
                            document.head.appendChild(style);
                        }

                        if (document.readyState === "complete" || document.readyState === 'interactive') {
                            addStyleTagToHead();
                        }
                        else {
                            function onDocumentLoad() {
                                addStyleTagToHead();
                                window.removeEventListener("DOMContentLoaded", onDocumentLoad);
                            }
                            window.addEventListener("DOMContentLoaded", onDocumentLoad);
                        }
                    }

                    var $injector = angular.element(document.body.childNodes[0]).injector();
                    var store = $injector.get("store");
                    var safeDigest = $injector.get("safeDigest");

                    if (scope.vm && scope.vm.storeOnChange) {
                        var subscription = store.subscribe(state => {
                            scope.vm.storeOnChange(state);
                            safeDigest(scope);
                        });

                        scope.$on("$destroy", () => subscription.dispose());
                    }
                },
                post: function (scope: any, element, attributes, controller) {

                    if (options.require) {
                        var componentName = options.require.replace("^", "");
                        scope.vm[componentName] = controller;
                    }

                    if (options.transclude && scope.vm.$transclude)
                        scope.vm.$transclude(scope.$new(), function (clone: ng.IAugmentedJQuery) {
                            scope.vm.template = template;
                            
                            if (template[0].nodeType === 1)
                                var documentFragment = angular.element("<div></div>");

                            for (var i = 0; i < clone.length; i++) {
                                documentFragment.append(clone[i]);
                            }
                            scope.vm.clone = documentFragment;
                        });

                    if (scope.vm && scope.vm.ngOnInit)
                        scope.vm.ngOnInit();

                    if (scope.vm.ngOnDestroy)
                        scope.$on("$destroy", () => {
                            scope.vm.dispose();
                        });


                }
            }
        }

        m.directive(componentNameCamelCase,
            [() => { return directiveDefinitionObject; }]);

        m.controller(options.componentName ? options.componentName : componentNameCamelCase + "Component", options.component);
    }
    return m;
};

