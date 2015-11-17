/// <reference path="../../../scripts/typings/typescriptapp.d.ts" />

module app.security {

    export class States {

        public static configure($stateProvider: angular.ui.IStateProvider) {

            $stateProvider
                .state("login",
                {
                    url: "/login",
                    templateUrl: "/src/app/security/views/login.html",
                    resolve: {
                        routeData: ["stateResolver", (stateResolver: common.IStateResolver) => {
                            return stateResolver.resolve("login");
                        }]
                    }
                });

        }
    }
} 