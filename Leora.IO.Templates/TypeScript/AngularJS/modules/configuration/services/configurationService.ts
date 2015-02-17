module ConfigurationModule {

    export class ConfigurationService implements IConfigurationService {
        
        constructor(private $http: ng.IHttpService, private $q: ng.IQService, private $rootScope: ICoreRootScope) {

        }

        public static serviceId = "configurationService";

        baseUri: string = "api/configuration/";

        get(): ng.IPromise<any> {
            var deferred = this.$q.defer();

            this.$http({ method: "GET", url: this.baseUri + "get" }).then((results) => {
                deferred.resolve(this.$rootScope.configuration = results.data);
            }).catch((error) => {
                deferred.reject(error);
            });

            return deferred.promise;
        }
    }

    angular.module("configuration").service(ConfigurationService.serviceId, ["$http", "$q", "$rootScope", ($http, $q, $rootScope) => new ConfigurationService($http,$q,$rootScope)]);
}