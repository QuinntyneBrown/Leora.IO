module app.feedback {

    "use strict";

    export class FeedbackModalService implements IFeedbackModalService {

        constructor(private $q:ng.IQService,private modalService: ui.IModalService) {
            
        }

        public showModal = () => {

            var deferred = this.$q.defer();

            this.modalService.showModal({
                controller: "feedbackModalController as feedbackModal",
                templateUrl: "/src/app/feedback/views/partials/feedbackModal.html"
            }, null).then((results) => {
                deferred.resolve();
            });

            return deferred.promise;
        }
    }

    angular.module("app.feedback").service("feedbackModalService", ["$q","modalService", FeedbackModalService]);
} 