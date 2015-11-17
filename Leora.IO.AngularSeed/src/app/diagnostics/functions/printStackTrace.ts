module app.diagnostics {

    "use strict";

    angular.module("app.diagnostics").value("printStackTrace", (error:Error) => {
        return printStackTrace({ e: error });
    });
} 