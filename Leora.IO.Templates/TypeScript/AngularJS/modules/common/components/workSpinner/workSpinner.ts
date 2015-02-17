module CommonModule {

    "use strict";

    var workSpinner = ($rootScope, requestCounter) => {
        return {
            restrict: "E",
            scope: {},
            template: "<div ng-show='requestCount' class='work-spinner'><i class='fa fa-spinner fa-spin fade'></i></div>",
            link: (scope) => {
                scope.$watch(() => {
                    return requestCounter.getRequestCount();
                }, (requestCount) => {
                    scope.requestCount = requestCount;
                });
            }
        };
    };

    var componentId = "workSpinner";


    angular.module("common").directive(componentId,["$rootScope", "requestCounter",workSpinner]);

}