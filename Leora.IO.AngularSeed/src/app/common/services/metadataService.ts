module app.common {
    
    export class MetadataService implements IMetadataService {

        constructor(public $q: ng.IQService,
            public apiEndpoint: common.IApiEndpointConfig,
            public dataService: common.IDataService) {
        }

        public getMetadata() {
            var deferred = this.$q.defer();

            deferred.resolve();

            return deferred.promise;
        }

        public getMetadataForEntity() {
            var deferred = this.$q.defer();


            return deferred.promise;            
        }

        public getLocalMetadataForEntity() {
            return null;
        }
    }

    angular.module("app.common").service("metadataService", ["$q","apiEndpoint","dataService",MetadataService]);

} 