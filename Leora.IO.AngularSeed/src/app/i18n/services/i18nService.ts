module app.i18n {

    "use strict";

    export class I18nService implements  II18nService {

        constructor(public $http: ng.IHttpService,
            public $q: ng.IQService,
            public apiEndpoint: common.IApiEndpointConfig,
            public dataService: common.IDataService,
            public localStorageService: common.ILocalStorageService            
            ) {

            
        }

        public get = (cutlureCode, phrases) => {

            var deferred = this.$q.defer();

            var data = {
                cultureCode: cutlureCode,
                phrases: phrases
            };

            var cachedData = this.localStorageService.get(this.baseUri + cutlureCode);

            if (!cachedData) {
                this.$http({ method: "POST", url: this.baseUri, data: data }).then((results) => {
                    this.localStorageService.set(this.baseUri + cutlureCode, results);
                    deferred.resolve(results);
                }).catch((error) => {
                    deferred.reject();
                });
            } else {
                deferred.resolve(cachedData);
            }

            return deferred.promise;
        
        }

        public get baseUri() {
            return this.apiEndpoint.getBaseUrl() + "/i18n";
        }

    }

    angular.module("app.i18n").service("i18nService", ["$http", "$q", "apiEndpoint", "dataService", "localStorageService", I18nService]);
}
