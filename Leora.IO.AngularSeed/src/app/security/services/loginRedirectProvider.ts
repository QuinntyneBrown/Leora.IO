/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.security {

    "use strict";

    export class LoginRedirectProvider implements ILoginRedirectProvider {
        constructor() {
            
        }

        public loginUrl:string;

        public _lastPath:string;

        public get lastPath() {
            return this._lastPath;
        }

        public set lastPath(value) {

            if (value == this.loginUrl) {
                return;
            }

            this._lastPath = value;
        }

        public defaultPath:string;

        public setLoginUrl =  (value) => {
            this.loginUrl = value;
        };

        public setDefaultUrl = (value) => {
            this.defaultPath = value;
        };

        public $get = ["$q","$location",($q: ng.IQService,$location:ng.ILocationService) => {
            return {

                setLastPath: (path:string) => {
                    this._lastPath = path;
                },

                getLoginUrl:() => {
                    return this.loginUrl;
                },

                responseError:  (response) => {
                    if (response.status == 401) {
                        this.lastPath = $location.path();
                        $location.path(this.loginUrl);
                    }
                    return $q.reject(response);
                },

                redirectPostLogin: () => {
                    if (this.lastPath) {
                        $location.path(this.lastPath);
                        this.lastPath = "";
                    } else {
                        $location.path(this.defaultPath);
                    }
                },

                redirectToLogin: () => {
                    this.lastPath = $location.path();
                    $location.path(this.loginUrl);
                }
            };            
        }]
    }

    angular.module("app.security").provider("loginRedirect", [LoginRedirectProvider])
        .config(["$httpProvider", config]);

    function config($httpProvider) {
        $httpProvider.interceptors.push("loginRedirect");
    }
} 
