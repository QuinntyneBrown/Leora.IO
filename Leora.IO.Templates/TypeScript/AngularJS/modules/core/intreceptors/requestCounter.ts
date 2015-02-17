module CoreModule {

    "use strict";

    function requestCounter($q) {

        var requests = 0;

        var request = (config) => {
            requests += 1;
            return $q.when(config);
        };

        var requestError = (error) => {
            requests -= 1;
            return $q.reject(error);
        };

        var response = (response) => {
            requests -= 1;
            return $q.when(response);
        };

        var responseError = (error) => {
            requests -= 1;
            return $q.reject(error);
        };

        var getRequestCount = () => {
            return requests;
        };

        return {
            request: request,
            response: response,
            requestError: requestError,
            responseError: responseError,
            getRequestCount: getRequestCount
        };

    }

    var interceptorId = "requestCounter";

    angular.module("core").factory(interceptorId, ["$q",requestCounter]);

}