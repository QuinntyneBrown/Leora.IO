declare module app.feedback {

    export interface IFeedbackModalService {       
        showModal();
    }

    export interface IFeedbackService {
        save(options: any): ng.IPromise<any>;
        update(options: any): ng.IPromise<any>;
    }

    export interface IFeedbackManager {
        guid:string;
    }
} 