/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.diagnostics {

    "use strict";

    export class ErrorRedirectProvider implements IErrorRedirectProvider {
        constructor() {

        }

        public $get = ["$location", "diagnosticsManager", ($location: ng.ILocationService, diagnosticsManager:any) => {
            return {
                redirectPostError: (options:any) => {
                    for (var i = diagnosticsManager.urls.length - 1; i >= 0; i--) {
                        var path = diagnosticsManager.urls[i].path;
                        if (diagnosticsManager.urls[i].changeSuccess && ((options.timestamp - diagnosticsManager.urls[i].dateTime) > 500)) {
                            i = -1;
                            $location.path("/");
                        }
                    }
                }
            };
        }];
    }

    angular.module("app.diagnostics").provider("errorRedirect", [ErrorRedirectProvider]);

} 
