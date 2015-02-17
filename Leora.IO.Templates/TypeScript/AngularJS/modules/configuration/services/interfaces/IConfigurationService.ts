module ConfigurationModule {

    export interface IConfigurationService {
        get(): ng.IPromise<any>;
    }

}