/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.diagnostics {

    "use strict";

    export class RequestProfiler implements IRequestProfiler {

        constructor(private $q: ng.IQService, private diagnosticsManager: IDiagnosticsManager) {

        }

        public static instance = ($q, diagnosticsManager: IDiagnosticsManager) => {
            return new RequestProfiler($q, diagnosticsManager);
        }

        public request = (config) => {
            config.timestamp = Date.now();
            var requestProfiles = this.diagnosticsManager.requestProfiles;
            requestProfiles.push({
                url: config.url,
                startTime: config.timestamp
            });
            this.diagnosticsManager.requestProfiles = requestProfiles;
            return this.$q.when(config);
        }

        public response = (response) => {
            response.config.elapsed = Date.now() - response.config.timestamp;
            this.diagnosticsManager.updateRequestProfile(response);
            return this.$q.when(response);
        }

    }

    angular.module("app.diagnostics").factory("requestProfiler", ["$q", "diagnosticsManager",RequestProfiler.instance])
        .config([
        "$httpProvider", ($httpProvider) => {
            $httpProvider.interceptors.push("requestProfiler");
        }
    ]);
} 
