module app.configuration {

    angular.module("app.configuration", [
        "app.common",
        "ui.router"
    ])
        .config(["stateResolverProvider", (stateResolverProvider: common.IStateResolverProvider) => {
        
        stateResolverProvider.configure({
            priority: 0,
            promise: ["$http", "$q", "configurationManager", "configurationService", (
                $http: ng.IHttpService,
                $q: ng.IQService,
                configurationManager: IConfigurationManager,
                configurationService: IConfigurationSerivce) => {

                var deferred = $q.defer();
                
                configurationService.get().then((results) => {
                    configurationManager.configuration = results.data;
                    deferred.resolve();
                }).catch((error) => {
                    deferred.resolve();
                });

                return deferred.promise;
            }]
        }); 

    }]);

} 