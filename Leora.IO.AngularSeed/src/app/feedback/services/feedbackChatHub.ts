module app.feedback {

    "use strict";

    export class FeedbackChatHub {
        constructor(private $rootScope: ng.IRootScopeService, $: any) {

            this.connection = $.hubConnection();

            this.hub = this.connection.createHubProxy("feedbackChatHub");

            this.connection.start(() => {

            });

        }

        public connection: any = null;

        public hub: any = null;


    }

    angular.module("app.feedback").service("feedbackChatHub", ["$rootScope","$",FeedbackChatHub]);
} 