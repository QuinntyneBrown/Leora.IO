module app.common {

    "use strict";

    export class StateParamsHelper implements IStateParamsHelper {

        constructor(public $location: ng.ILocationService, public $state: ng.ui.IStateService, public pathRegExp:any) {
            
        }

        public getParams = (paramIds: string[],stateUrl:string) => {

            var params = {};

            var currentState: any;

            if (stateUrl) {
                currentState = this.$state.get().filter((state) => {
                    return state.url===stateUrl;
                });
            } else {
                currentState = this.$state.get().filter((state) => {
                    return state.url.indexOf("{" + paramIds[0] + "}") > 0;
                });                
            }


            var url;

            if (currentState[0].prefix) {
                url = currentState[0].prefix + currentState[0].url;
            } else {
                url = currentState[0].url;
            }
            
            paramIds.forEach((param) => {
                url = url.replace("{" + param + "}", ":" + param);
            });

            var regEx = this.pathRegExp(url, {});

            var results = regEx.regexp.exec(this.$location.path());

            paramIds.forEach((paramId, index) => {
                params[paramId] = results[index + 1];
            });

            return params;
        }
    }

    angular.module("app.common")
        .service("stateParamsHelper", ["$location", "$state", "pathRegExp", StateParamsHelper])
        .run(["$state", ($state:ng.ui.IStateService) => {
                // figure out to make a function to find duplicate query parameters
            }
        ]);
} 