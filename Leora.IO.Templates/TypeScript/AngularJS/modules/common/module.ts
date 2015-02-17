module CommonModule {
    "use strict";

    angular.module("common", [
        "core",
        "configuration",
        "session",
        "ngAnimate",
        "ngRoute"]
        )
        .config(["$routeProvider",config])
        .run(run);


    function config($routeProvider) {


    }

    function run() {
        
    }

}