module app.ui {

    //http://blogs.windows.com/buildingapps/2015/06/08/winjs-4-0-released/

    "use strict";

    export class Popover {        
        constructor(private configurationManager: configuration.IConfigurationManager, private popoverService: IPopoverService) {
            
        }

        public static instance = (configurationManager: configuration.IConfigurationManager, popoverService: IPopoverService) => {
            return new Popover(configurationManager, popoverService);
        }

        public restrict: string = "E";

        public replace: boolean = true;

        public scope: any = {
            options:"="
        };

        public templateUrl: string = "/src/app/ui/popover/popover.html";

        public link = ( scope: IPopoverScope, element: ng.IAugmentedJQuery, attributes:ng.IAttributes) => {

            initialize(scope, element, attributes);

            element[0].addEventListener(scope.options.triggerEvent,(event: MSEventObj) => {

                event.stopPropagation();

                this.popoverService.showPopover({
                    appendToBody: scope.options.appendToBody || true,
                    defaultDirection: scope.options.defaultDirection || "bottom",
                    element: <IHTMLElement>element[0],
                    popoverTitle: scope.options.popoverTitle,
                    templateUrl: scope.options.templateUrl,
                    template: template,
                    transitionDurationInMilliseconds: scope.options.transitionDurationInMilliseconds || 500,
                    triggerScope: scope,
                    visibilityDurationInMilliseconds: scope.options.visibilityDurationInMilliseconds || 3000
                });

            });

            function initialize(scope: IPopoverScope, element: ng.IAugmentedJQuery, attributes: ng.IAttributes) {

                scope.options.triggerEvent = scope.options.triggerEvent || "click";

                extendScope(scope, attributes);

                template = <HTMLElement>element[0].querySelectorAll("[popover-template]")[0];

                if (template) {
                    template.parentNode.removeChild(template);
                }
            }

            function extendScope(scope: IPopoverScope, attributes: ng.IAttributes) {
                angular.extend(scope, buildViewBagFromAttributes(attributes));
                angular.extend(scope, scope.options.viewBag);
            }

            function buildViewBagFromAttributes(attributes: ng.IAttributes) {

                var viewBag: any = {};

                for (var attr in attributes) {
                    if (attr.indexOf("$") < 0 && attr.indexOf("class") < 0 && attr.indexOf("popoverTrigger") < 0) {
                        if (!angular.isFunction(attributes[attr])) {
                            viewBag[attr] = attributes[attr];
                        }
                    }
                }

                return viewBag;
            }

            var template: HTMLElement;


        }
    }

    angular.module("app.ui").directive("popover", ["configurationManager",Popover.instance]);
}  