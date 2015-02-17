module CoreModule {

    "use strict";

    export interface IApiEndpointProvider {

        configure(baseUrl: string): void;

    }
} 