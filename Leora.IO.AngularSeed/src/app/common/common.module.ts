/// <reference path="../../../scripts/typings/typescriptapp.d.ts" />

module app.common {
    angular.module("app.common", [
        "ui.router"
    ])
        .config(["stateResolverProvider", (stateResolverProvider) => {

        stateResolverProvider.configure({
            priority: 2,
            key: "metadataRouteData",
            promise: [
                "$q", "metadataService", (
                    $q: ng.IQService,
                    metaDataService: IMetadataService) => {

                    var deferred = $q.defer();

                    metaDataService.getMetadata().then((results) => {
                        deferred.resolve(true);
                    });

                    return deferred.promise;
                }]
            });
        }]);
} 