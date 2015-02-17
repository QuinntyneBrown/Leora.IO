module CoreModule {
    class ApiEndpointProvider implements ng.IServiceProvider, IApiEndpointProvider {
        config: IApiEndpointConfig;

        configure(baseUrl: string): void {
            this.config = {
                baseUrl: baseUrl
            };
        }

        $get(): IApiEndpointConfig {
            return this.config;
        }
    }

    angular.module("core").provider("apiEndpointProvider", ApiEndpointProvider);

} 