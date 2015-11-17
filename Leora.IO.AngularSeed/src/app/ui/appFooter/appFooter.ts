module app.ui {

    "use strict";

    export class AppFooter implements ng.IDirective {
        constructor(private $injector: ng.auto.IInjectorService, private appFooterConfig: ui.IAppFooterConfig) {
            this.appManager = $injector.get(appFooterConfig.appManager);
        }

        public static instance($injector: ng.auto.IInjectorService, appFooterConfig: ui.IAppFooterConfig) {
            return new AppFooter($injector, appFooterConfig);
        }

        public appManager: common.IAppManager;

        public templateUrl: string = "/src/app/ui/appFooter/appFooter.html";

        public restrict: string = "E";

        public replace: boolean = true;

        public scope: any = {

        };

        public link = (scope: any, element: ng.IAugmentedJQuery, attributes: ng.IAttributes) => {
            scope.appManager = this.appManager;
        }
    }

    angular.module("app.ui").directive("appFooter", ["$injector","appFooterConfig", AppFooter.instance]);

    
} 