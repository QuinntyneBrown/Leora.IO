module app.common {

    "use strict";

    export class StateDeactivatorProvider {
        constructor() {
            
        }
    }

    angular.module("app.common").service("stateDeactivator", [StateResolverProvider]);
} 