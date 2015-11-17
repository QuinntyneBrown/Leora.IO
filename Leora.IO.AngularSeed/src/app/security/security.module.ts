/// <reference path="../../../scripts/typings/typescriptapp.d.ts" />

module app.security {
    angular.module("app.security", [
        "ui.bootstrap",
        "app.common",
        "app.ui"
    ]).run(["$http",
        "$location",
        "$rootScope",
        "$state",
        "$templateCache",
        "fire",
        "loginRedirect",
        "securityManager",
        ($http: ng.IHttpService,
            $location: ng.ILocationService,
            $rootScope: any,
            $state: ng.ui.IStateService,
            $templateCache: ng.ITemplateCacheService,
            fire: any,
            loginRedirect: security.ILoginRedirect,
            securityManager: security.ISecurityManager) => {

            var rootScope = $rootScope;
            var location = $location;
            var state = $state;
            var loginUrl = "/login";


            $rootScope.$on("$stateChangeStart",(event: ng.IAngularEvent, toState: ng.ui.IState) => {

                rootScope.isNavigating = true;

                if (toState.url == loginUrl) {
                    fire(document.getElementsByTagName("body")[0], "sessionTimeout");
                }

                if (toState && toState["authorizationRequired"]) {
                    if (securityManager.token == null || securityManager.tokenExpiryDate == null || Date.now() > securityManager.tokenExpiryDate) {
                        rootScope.$evalAsync(() => {
                            loginRedirect.redirectToLogin();
                        });
                    }
                }
            });

            $rootScope.$on("$viewContentLoaded",() => {
                $rootScope.isNavigating = false;

                if (state.current.name && state.current["authorizationRequired"] && (securityManager.currentUser == null || securityManager.currentUser == "")) {
                    rootScope.$evalAsync(() => {
                        loginRedirect.redirectToLogin();
                    });
                }

            });
        }
    ]);
} 