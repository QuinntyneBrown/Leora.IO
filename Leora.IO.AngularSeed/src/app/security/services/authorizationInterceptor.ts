/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.security {

    "use strict";

    export class AuthorizationInterceptor {

        constructor(private securityManager: ISecurityManager) {
            
        }

        public static instance = (securityManager: ISecurityManager) => {
            return new AuthorizationInterceptor(securityManager);
        }

        public request = (config) => {
            if (this.securityManager.token) {
                config.headers.Authorization = "Bearer " + this.securityManager.token;
            }

            return config;
        }
    }

    angular.module("app.security")
        .factory("authorizationInterceptor", ["securityManager", AuthorizationInterceptor.instance])
        .config([
            "$httpProvider", ($httpProvider) => {
                $httpProvider.interceptors.push("authorizationInterceptor");
            }
        ]).run([
        "$", "securityManager", ($: any, securityManager: ISecurityManager) => {

            $.ajaxPrefilter((options) => {
                if (!options.beforeSend) {
                    options.beforeSend = (xhr) => {
                        xhr.setRequestHeader("Authorization", "Bearer " + securityManager.token);
                    }
                }
            });

        }]);

}
