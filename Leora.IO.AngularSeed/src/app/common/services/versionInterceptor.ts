module app.common {

    "use strict";

    export class VersionInterceptor {
        constructor(private $q: ng.IQService) {
            
        }

        public serverVersion = null;

        public static instance = ($q:ng.IQService) => {
            return new VersionInterceptor($q);
        }

        public response = (response) => {

            if (response.headers().server && response.headers().server != "Microsoft-IIS/10.0")
                this.serverVersion = response.headers().server;

            return this.$q.when(response);
        }
    }

    angular.module("app.common").factory("versionInterceptor", ["$q", VersionInterceptor.instance])
        .config([
        "$httpProvider", ($httpProvider: ng.IHttpProvider) => {
            $httpProvider.interceptors.push("versionInterceptor");
        }
    ]);
} 