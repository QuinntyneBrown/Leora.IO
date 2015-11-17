module app.ui {


    export class AppHeader implements ng.IDirective {
        constructor(private $compile:ng.ICompileService,
            private $filter: ng.IFilterService,
            private appHeaderConfig: IAppHeaderConfig,
            private $injector: ng.auto.IInjectorService) {

            this.securityManager = $injector.get(appHeaderConfig.securityManager);
            this.i18nManager = $injector.get(appHeaderConfig.i18nManager);
            this.appManager = $injector.get(appHeaderConfig.appManager);
        }

        public static instance($compile: ng.ICompileService, $filter: ng.IFilterService, appHeaderConfig: IAppHeaderConfig, $injector: ng.auto.IInjectorService) {
            return new AppHeader($compile, $filter, appHeaderConfig, $injector);
        }

        private securityManager: security.ISecurityManager;

        private i18nManager: i18n.II18nManager;

        private appManager: common.IAppManager;

        public templateUrl: string = "/src/app/ui/appHeader/appHeader.html";

        public restrict: string = "E";

        public replace: boolean = true;

        public scope:any = {
        };

        public link = (scope: any, element: ng.IAugmentedJQuery, attributes: ng.IAttributes) => {

            scope.getTranslations = () => {
                return this.i18nManager.translatedPhrases;
            };

            scope.appTitle = () => {
                return this.$filter("translate")(this.appHeaderConfig.appTitle);
            };

            scope.isAuthenticated = () => {
                return this.securityManager.isAuthenticated;
            }

            scope.getCurrentUser = () => {
                return this.securityManager.currentUser;
            }

            scope.navigationLinks = this.appManager.navigationLinks;

        }
    }

    angular.module("app.ui")
        .directive("appHeader", ["$compile", "$filter", "appHeaderConfig", "$injector", AppHeader.instance]);
} 