/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.security {

    export class LoginController {
        constructor(
            public $compile:ng.ICompileService,
            public $location: ng.ILocationService,
            public $q: ng.IQService,
            public $rootScope: ng.IRootScopeService,
            public loginRedirect: security.ILoginRedirect,
            public securityManager: security.ISecurityManager,
            public securityService: security.ISecurityService) {

            this.username = this.securityManager.username;

            var link = <HTMLElement>document.getElementsByClassName("contact-link")[0];

            $rootScope["hi"] = () => {
                alert("hi");
            }

            var frag = document.createElement("div");

            

            var sp1 = document.createElement("a");
            sp1.innerHTML = "Sam";
            sp1.setAttribute("data-ng-click", "hi()");
            frag.appendChild(sp1);

            var linkFn = $compile(frag);
            var content = linkFn($rootScope);
            var x = content[0].children[0];

            link.parentNode.replaceChild(x,link);
        }

        public tryToLogin($form) {

            var deferred = this.$q.defer();

            this.$rootScope["isBusy"] = true;

            this.securityService.tryToLogin(this.username, this.password).then((results) => {
                this.securityManager.token = results.data.access_token;
                this.securityManager.username = this.username;
                this.securityManager.tokenExpiryDate = Date.now() + results.data.expires_in * 1000;
                
                this.$rootScope["isBusy"] = false;

                if (this.$location.path() == this.loginRedirect.getLoginUrl()) {
                    this.loginRedirect.redirectPostLogin();
                } else {
                    this.$rootScope.$emit("closeModal");
                }

                deferred.resolve();

            }).catch((error) => {

                this.$rootScope["isBusy"] = false;

                if (this.$location.path() != this.loginRedirect.getLoginUrl()) {
                    this.$rootScope.$emit("closeModal");
                    this.loginRedirect.redirectToLogin();
                }

                deferred.reject();
            });

            return deferred.promise;
        }

        public username:string;

        public password = "sirA4tec";

    }

    angular.module("app.security")
        .controller("loginController", ["$compile", "$location", "$q", "$rootScope","loginRedirect", "securityManager", "securityService", LoginController]);
} 