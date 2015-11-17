/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.common {

    "use strict";

    class WorkSpinner implements ng.IDirective {

        constructor(private $rootScope: ng.IRootScopeService, private requestCounter: IRequestCounter) {

        }

        public static instance = ($rootScope: ng.IRootScopeService, requestCounter: IRequestCounter) => {
            return new WorkSpinner($rootScope, requestCounter);
        }

        public restrict: string = "E";

        public replace: boolean = true;

        public scope: any = {};

        public template = "<div class='work-spinner' data-ng-show='requestCount && isNavigating'><i id='spin' class='fa fa-spinner fa-spin fade'></i></div>";

        public link = (scope: ng.IScope) => {
            scope.$watch(() => {
                return this.requestCounter.getRequestCount();
            },(requestCount: number) => {
                    scope["requestCount"] = requestCount;
            });

            this.$rootScope.$watch("isNavigating",() => {
                scope["isNavigating"] = this.$rootScope["isNavigating"];
            });
        };
    }

    angular.module("app.common").directive("workSpinner", ["$rootScope","requestCounter", WorkSpinner.instance]);
} 
