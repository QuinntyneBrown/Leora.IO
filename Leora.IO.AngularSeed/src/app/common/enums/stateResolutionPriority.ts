module app.common {
    "use strict";

    export enum stateResolutionPriority {
        requestStart = 0,
        beforeAuthentication = 1,
        afterAuthentication = 200
    }
} 