module app.feedback {

    "use strict";

    export class FeedbackService implements IFeedbackService {
        constructor(
            private apiEndpointConfig: common.IApiEndpointConfig,
            private dataService: common.IDataService
            ) {
            
        }

        public save = (options: any) => {

            var data = {
                screenCaptures: options.screenCaptures,
                consoleMessages: options.consoleMessages,
                urls: options.urls,
                features: options.features,
                comments: options.comments,
                rating: options.rating
            };

            return this.dataService.fromService({ method: "POST", data: data, url: this.baseUri + "/feedback/" });
        }

        public update = (options: any) => {

            var data = {
                guid: options.guid,
                comments: options.comments,
                rating: options.rating
            };

            return this.dataService.fromService({ method: "PUT", data: data, url: this.baseUri + "/feedback/" });
        }

        public get baseUri() {
            return this.apiEndpointConfig.getBaseUrl();
        }

    }

    angular.module("app.feedback").service("feedbackService", [
        "apiEndpoint",
        "dataService",
        FeedbackService]);
} 