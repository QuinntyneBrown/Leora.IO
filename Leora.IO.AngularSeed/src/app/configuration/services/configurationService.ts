module app.configuration {

    "use strict";

    export class ConfigurationService implements IConfigurationSerivce {
        constructor(public dataService: common.IDataService) {
            
        }

        public get = () => {
            return this.dataService.fromService({ url: "/api/configuration", method:"GET" }, "configuration");
        }
    }

    angular.module("app.configuration").service("configurationService", ["dataService",ConfigurationService]);
} 