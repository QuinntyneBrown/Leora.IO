module app.common {

    "use strict";
    
    export class PrioritizedPromisesService implements IPrioritizedPromisesService {
        constructor(private $q:ng.IQService) {
            
        }

        public instance = ($q: ng.IQService) => {
            return new PrioritizedPromisesService($q);
        }

        public add = (func: IPrioritizableAsyncFunc) => {
            this.stack.push(func);
            return this;
        }

        public resolve = () => {
            var deferred = this.$q.defer();
            this.processAll(this.stack, 0, () => {
                deferred.resolve();
            });
            return deferred.promise;
        }

        public processAll = (stack: IPrioritizableAsyncFunc[], index: number, callback:any) => {
            stack[index].func(this.stack[index].options).then((results) => {
                this.next(stack, index, callback);
            }).catch((error) => {
                this.next(stack, index, callback);
            });
        }

        public next = (stack: IPrioritizableAsyncFunc[], index: number, callback: any) => {
            index = index + 1;
            if (index < this.stack.length)
                this.processAll(stack, index, callback);
            else {
                callback();
            }
        }

        private stack:IPrioritizableAsyncFunc[] = [];
    }

    angular.module("app.common").service("prioritizedPromisesService", ["$q",PrioritizedPromisesService]);
} 