/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.security {
    
    "use strict";

    export class SecurityService implements ISecurityService {
        
        constructor(
            public $q: ng.IQService,
            public dataService: common.IDataService,
            public formEncode: common.IFormEncode,
            public apiEndpoint: common.IApiEndpointConfig,
            public configurationManager: configuration.IConfigurationManager) {

        }

        public tryToLogin (username: string, password: string) {
            var deferred = this.$q.defer();

            var configuration = {
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            };

            var data = this.formEncode({
                username: this.configurationManager.configuration.applicationName + "." + username,
                password: password,
                grant_type: "password"
            });

            this.dataService.fromService({ method: "POST", url: this.apiEndpoint.getBaseUrl("login"), data:data, configuration: configuration }).then((results) => {
                    deferred.resolve({ username: username, data: results.data });
            }).catch((Error) => {
                deferred.reject();
            });

            return deferred.promise;
        }
        
        public getCurrentUser = () => {

            var deferred = this.$q.defer();

            this.dataService.fromService({ method: "GET", url: this.apiEndpoint.getBaseUrl("currentUser") }).then((results) => {
                deferred.resolve(results);
            }).catch((error) => {
                deferred.reject(error);
            });

            return deferred.promise;
        }
    }

    angular.module("app.security").service("securityService", ["$q", "dataService", "formEncode", "apiEndpoint","configurationManager", SecurityService]);
} 
