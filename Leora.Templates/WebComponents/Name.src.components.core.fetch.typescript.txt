export interface IFetchOptions {
    method?: string;
    url: string;
    data?: any;
    params?: any;
    headers?: any;
}

export class Fetch {

    constructor(private $http: ng.IHttpService, private $q: ng.IQService, private localStorageManager) { }

    public inMemoryCache: any = {};

    public fromService = (options: IFetchOptions):angular.IPromise<any> => {
        var deferred = this.$q.defer();
        this.$http({ method: options.method, url: options.url, data: options.data, params: options.params, headers: options.headers }).then((results) => {
            deferred.resolve(results);
        }).catch((error: Error) => {
            deferred.reject(error);
        });
        return deferred.promise;
    }

    public fromCacheOrService = (options: any) => {
        var deferred = this.$q.defer();
        var cachedData = this.localStorageManager.get({ name: options.url });
        if (!cachedData) {
            this.fromService(options).then((results) => {
                deferred.resolve(results);
            }).catch((error: Error) => {
                deferred.reject(error);
            });
        } else {
            deferred.resolve(cachedData.value);
        }
        return deferred.promise;
    }

    public get bodyNativeElement() {
        return document.getElementsByTagName("body")[0];
    }

}

angular.module("fetch", ["localStorageManager"]).service("fetch", ["$http", "$q", "localStorageManager", Fetch]);
