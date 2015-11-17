/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.common {

    "use strict";

    export class ManualWorkSpinner implements ng.IDirective {

        constructor(public $timeout: ng.ITimeoutService) {

        }

        public static instance($timeout: ng.ITimeoutService) {
            return new ManualWorkSpinner($timeout);
        }

        public transclude: boolean = true;

        public restrict: string = "EA";

        public replace: boolean = true;

        public templateUrl = "/src/app/ui/manualWorkSpinner/manualWorkSpinner.html";

        public link = (scope: ng.IScope, element:ng.IAugmentedJQuery) => {
            
            var _isBusy: boolean = false;

            
            var visibilityDurationInMilliseconds = 300;

            var timeout: any;

            scope.$watch("isBusy",() => {
                if (scope["isBusy"]) {
                    show();
                    _isBusy = true;
                } else {
                    if (_isBusy) {                        
                        timeout = this.$timeout(() => {
                            timeout = null;
                            hide();
                        }, visibilityDurationInMilliseconds);
                        _isBusy = false;
                    }
                }
            });

            function hide() {
                element.find("i.fa-spinner").css(
                    {
                        "display": "none"
                    });
            }

            function show() {
                element.find("i.fa-spinner").css(
                    {
                        "display": "inline-block"
                    });
            }
        };

    }

    angular.module("app.ui").directive("manualWorkSpinner", ["$timeout",ManualWorkSpinner.instance]);
} 
