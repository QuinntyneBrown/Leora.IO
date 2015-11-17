module app.common {
    
    export function fromCacheOrService($http:ng.IHttpService, $q:ng.IQService, cacheKey:string, action: IHttpAction, storage:any) {

        var deferred = $q.defer();

        var cachedData = storage.getByName({ name: action.url + JSON.stringify(action.params) });

        if (!cachedData || !cachedData.value) {
            $http({ method: action.method, url: action.url, data: action.data, params: action.params }).then((results) => {
                storage.put({ category: cacheKey, name: action.url + JSON.stringify(action.params), value: results });
                deferred.resolve(results);
            }).catch((error) => {
                deferred.reject(error);
            });
        } else {
            deferred.resolve(cachedData.value);
        }
        return deferred.promise;
    }

    angular.module("app.common").value("fromCacheOrService", fromCacheOrService);
} 