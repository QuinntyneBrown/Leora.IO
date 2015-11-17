module app.abstractDataTypes {

    "use strict";


    export class SequentialPromiseList {
        constructor(private $q: ng.IQService) {
            
        }

        public instance = () => {
            return new SequentialPromiseList(this.$q);
        }

        private promises: ISequentialPromise[] = [];

        private results: any[] = [];

        public push = (options:any) => {
            this.promises.push({
                index: this.promises.length,
                func: options.func,
                options: options.options
            });
        }

        public sorted:boolean = false;

        public resolve = (sequentialPromise?: ISequentialPromise) => {

            var deferred = this.$q.defer();

            if (!this.sorted) {
                this.promises = this.promises.sort((a, b) => {
                    return b.index - a.index;
                });
                this.sorted = true;
            }

            sequentialPromise = sequentialPromise || this.promises[this.promises.length -1];

            sequentialPromise.func(sequentialPromise.options).then((results) => {
                this.results.push(results);
                this.promises.pop();
                if (this.promises.length > 0) {
                    this.resolve(this.promises[this.promises.length - 1]);
                } else {
                    deferred.resolve(this.results);
                }
            });

            return deferred.promise;
        }
    }

    angular.module("app.abstractDataTypes").service("sequentialPromiseList", ["$q",SequentialPromiseList]);
} 