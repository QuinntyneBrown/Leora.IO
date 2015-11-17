module app.common {

    "use strict";

    /**
     * Provider to stream line the data that's resolve in every state change.
     */
    export class StateResolverProvider implements IStateResolverProvider {

        /**
         * register state promise during config
         */
        public configure = (statePromise: IStatePromise) => {
            this.statePromises.push(statePromise);
        }

        public statePromises: IStatePromise[] = [];

        public deactivationStatePromises: IStatePromise[] = [];

        public stateParams: ng.ui.IStateParamsService;

        /**
         * Get All State Promises that have been registered during the angular config phase
         * filter the promises by stateName defined in the registration function
         * sort the State Promises by priority. Lowest priority executes first
         */
        public getStatePromises = (stateName: string) => {
            return this.statePromises.filter((statePromise: IStatePromise) => {
                if (statePromise.state == null || statePromise.state == stateName) {

                    if (!statePromise.exludedStates)
                        return true;

                    if (statePromise.exludedStates.indexOf(stateName) < 0)
                        return true;

                    return false;
                } else {
                    return false;
                }
            }).sort((statePromise1: IStatePromise, statePromise2: IStatePromise) => {
                return statePromise1.priority - statePromise2.priority;
            });
        }
        
        /**
         * Group StatePromises by Priority
         */
        public groupStatePromisesByPriority = (statePromises: IStatePromise[]) => {

            var priorities: any = [];

            var statePromisesPrioritizedGroups = [];

            statePromises.forEach((promise) => {
                if (priorities.indexOf(promise.priority) < 0)
                    priorities.push(promise.priority);
            });

            priorities.forEach((priority, index) => {
                statePromisesPrioritizedGroups.push({
                    promises: statePromises.filter((promise) => { return promise.priority == priority; }),
                    priority: priority,
                    isLast: index == priorities.length - 1
                });
            });

            return statePromisesPrioritizedGroups;
        }

        /**
         * Invoke Group Promises Asynchrounosly.
         * After you reach the last group, call the promise resolve callback
         */
        public invoke = ($injector: ng.auto.IInjectorService, $q: ng.IQService, groups: IStatePromisesPrioritizedGroup[], currentGroupIndex: number, callback: any, resolvedRouteData: any) => {

            var excutedPromises = [];

            var currentGroup = groups[currentGroupIndex];

            currentGroup.promises.forEach((statePromise: IStatePromise) => {
                excutedPromises.push($injector.invoke(statePromise.promise));
            });

            $q.all(excutedPromises).then((results) => {
                results.forEach((result, index) => {
                    if (currentGroup.promises[index].key) {
                        resolvedRouteData[currentGroup.promises[index].key] = results[index];
                    }
                });

                if (currentGroup.isLast) {
                    callback();
                } else {
                    this.invoke($injector, $q, groups, currentGroupIndex + 1, callback, resolvedRouteData);
                }

            });
        }

        /**
        * Inject stateResolver        
         */
        public $get = ["$injector", "$q", "$state", ($injector: ng.auto.IInjectorService, $q: ng.IQService, $state: ng.ui.IStateService) => {
            return {
                resolve: (stateName: string, stateParams?: ng.ui.IStateParamsService) => {
                    this.stateParams = stateParams;                    
                    var deferred = $q.defer();
                    var resolvedRouteData: any = {};
                    var statePromises = this.getStatePromises(stateName);
                    var prioritizedGroups = this.groupStatePromisesByPriority(statePromises);
                    var priorities = [];

                    statePromises.forEach((promise) => {
                        if (priorities.indexOf(promise.priority) < 0)
                            priorities.push(promise.priority);
                    });

                    this.invoke($injector, $q, prioritizedGroups, 0,() => {
                        deferred.resolve(resolvedRouteData);
                    }, resolvedRouteData);

                    return deferred.promise;
                },
                getStateParams:() => {
                    return this.stateParams;
                },
                registerDeactivation: (deactivatableObject: IDeactivatable) => {
                    
                }
            };
        }]
    }

    angular.module("app.common").provider("stateResolver", [StateResolverProvider]);
} 