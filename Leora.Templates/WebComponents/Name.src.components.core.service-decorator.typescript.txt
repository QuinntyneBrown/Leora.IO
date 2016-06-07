export function Service(config: IServiceConfigurationOptions = {}) {
    return function (cls) {
        cls.serviceName = config.serviceName;
        cls.$inject = config.viewProviders;        
    };
}

export interface IServiceConfigurationOptions {
    service?: any;
    serviceName?:string,
    selector?: string,
    viewProviders?: Array<string>;
    moduleId?: string,
}