/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.common {

    export class DataService implements IDataService {

        constructor(public $http: ng.IHttpService,
            public $q: ng.IQService,
            public commonStorage: IStorage) {

        }

        public fromCache = (action: IHttpAction) => {            
            return this.commonStorage.getByName({ name: action.url + JSON.stringify(action.params) });
        }

        public fromCacheOrService = (action: IHttpAction, cacheKey: string) => {
            var deferred = this.$q.defer();
            var cachedData = this.commonStorage.getByName({ name: action.url + JSON.stringify(action.params) });
            if (!cachedData || !cachedData.value) {
                this.fromService(action, cacheKey).then((results) => {                    
                    deferred.resolve(results);
                }).catch((error) => {
                    deferred.reject(error);
                });
            } else {
                deferred.resolve(cachedData.value);
            }
            return deferred.promise;
        }

        public fromService = (action: IHttpAction, cacheKey:string) => {
            var deferred = this.$q.defer();

            this.$http({ method: action.method, url: action.url, data: action.data, params: action.params }).then((results) => {
                this.commonStorage.put({ category: cacheKey, name: action.url + JSON.stringify(action.params), value: results });
                deferred.resolve(results);
            }).catch((error) => {
                deferred.reject(error);
            });

            return deferred.promise;
        }

        public invalidateCache = (cacheKey:string) => {
            this.commonStorage.get().forEach((item) => {
                if (item.category === cacheKey) {
                    this.commonStorage.put({ name: item.name, value: null });
                }
            });
        }

    }

    angular.module("app.common").service("dataService", ["$http", "$q", "commonStorage", DataService]);
} 
