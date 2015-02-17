module CoreModule {

    "use strict";

    function authorizationInterceptor($q, $rootScope, token) {

        var self = this;

        self.request = (config) => {

            if (token.get()) {

                config.headers.Authorization = "Bearer " + token.get();

            }

            return config;
        };

        return self;

    };

    var interceptorId = "authorizationInterceptor";

    angular.module("core").factory(interceptorId, ["$q", "$rootScope", "token",authorizationInterceptor]);

}