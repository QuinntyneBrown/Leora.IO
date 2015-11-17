module app.i18n {

    "use strict";

    angular.module("app.i18n", [
        "angularMoment",
        "ui.router",        
        "app.common",
        "app.configuration"
    ])
        .config([() => {
        if (typeof (<any>String.prototype).hashCode === "undefined") {
            (<any>String.prototype).hashCode = function () {
                var hash = 0, i, chr, len;
                if (this.length == 0) return hash;
                for (i = 0, len = this.length; i < len; i++) {
                    chr = this.charCodeAt(i);
                    hash = ((hash << 5) - hash) + chr;
                    hash |= 0;
                }
                return hash;
            };
        }
        }])
        .config(["stateResolverProvider", (stateResolverProvider: common.IStateResolverProvider) => {

        stateResolverProvider.configure({
            priority: 2,
            key: "i18nRouteData",
            promise: ["$q", "amMoment", "i18nManager", "i18nService", "numeral", "phrases",  (
                $q: ng.IQService,
                amMoment: any,
                i18nManager: II18nManager,
                i18nService: II18nService,
                numeral: INumeral,
                phrases: string[]                
                ) => {

                var deferred = $q.defer();

                amMoment.changeLocale(i18nManager.cultureCode);
                numeral.language = i18nManager.cultureCode;

                i18nService.get(i18nManager.cultureCode, phrases).then((results) => {
                    i18nManager.translatedPhrases = results.data;
                    deferred.resolve(results.data);
                }).catch((error) => {
                    deferred.resolve();
                });

                return deferred.promise;

            }]
        });
    }]);
} 