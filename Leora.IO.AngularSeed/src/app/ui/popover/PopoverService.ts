module app.ui {

    "use strict";

    export class PopoverService implements IPopoverService {
        constructor(private uiHelper: common.IUIHelper,
            private prioritizedPromisesService: common.IPrioritizedPromisesService,
            private $http: ng.IHttpService,
            private $q: ng.IQService,
            private $templateCache: ng.ITemplateCacheService,
            private $timeout: ng.ITimeoutService
            ) {
            
        }

        public showPopover = (options: any) => {
            
            var deferred = this.$q.defer();

            // 1. Cancel possible scheduled hide function

            // 2. If 

            //      Double click cancel

            //      Otherwise continue

            // 3. Initialize State

            // 4. Fetch and Set Template

            // 5. Style

            // 6. Append To Body

            // 7. Schedule Hide


            return deferred.promise;
        }

        public initialize = () => {

            var deferred = this.$q.defer();

            deferred.resolve();

            return deferred.promise;

        }

        public fetchAndSetTemplate = (options:any) => {
            var deferred = this.$q.defer();

            this.$http({ method: "GET", url: options.templateUrl, cache: this.$templateCache }).then((results: any) => {
                this.htmlTemplate = results.data;
                deferred.resolve();
            });

            return deferred.promise;
        }

        public style = () => {
            var deferred = this.$q.defer();

            deferred.resolve();

            return deferred.promise;
        }

        public appendToBody = () => {
            var deferred = this.$q.defer();

            deferred.resolve();

            return deferred.promise;
        }

        public scheduleHide = () => {
            var deferred = this.$q.defer();

            deferred.resolve();

            return deferred.promise;
        }

        private triggerElementRect: ClientRect;

        private timeoutPromise: ng.IPromise<any>;

        private transitionDurationInMilliseconds: number;

        private visibilityDurationInMilliseconds: number;

        private triggerElement: HTMLElement;

        private popUpElement: HTMLElement;

        private triggerScope: IPopUpTriggerScope;

        private htmlTemplate: string;

        private alignment: string;

        private margin: string;

        private displayArrow: boolean;

        private position: string;

    }

    angular.module("app.ui").service("popoverService", ["$compile",
        "$http",
        "$q",
        "$templateCache",
        "$timeout",
        "prioritizedPromisesService",
        "uiHelper",
        PopoverService]);
} 