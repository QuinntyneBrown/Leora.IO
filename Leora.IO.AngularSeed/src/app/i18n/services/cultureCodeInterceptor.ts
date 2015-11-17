/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.i18n {

    "use strict";

    export class CultureCodeInterceptor {
        constructor(private i18nManager: II18nManager) {

        }

        public static instance = (i18nManager: II18nManager) => {
            return new CultureCodeInterceptor(i18nManager);
        }

        public request = (config) => {
            config.headers.AcceptLanguages = this.i18nManager.cultureCode;
            return config;
        }
    }

    angular.module("app.i18n")
        .factory("cultureCodeInterceptor", ["i18nManager", CultureCodeInterceptor.instance])
        .config([
            "$httpProvider", ($httpProvider: ng.IHttpProvider) => {
                $httpProvider.interceptors.push("cultureCodeInterceptor");
            }
        ]);

}
