declare module app.configuration {
    
    export interface IConfigurationSerivce {
        get():ng.IPromise<any>;
    }
   
    export interface IConfigurationManager {
        configuration: any;
    }
} 