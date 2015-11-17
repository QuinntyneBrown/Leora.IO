module app.abstractDataTypes {
    
    export class List<T> implements IList<T> {
        constructor() {
            
        }

        private dataStore = [];

        public instance = () => {
            return new List();
        }

        public get listSize() {
            return this.dataStore.length;
        }

        public get pos() {
            return 0;
        }

        public get length() {
            return this.dataStore.length;
        }

        public clear = () => {
            throw new Error("Not Implemented");
        }

        public toString = () => {
            throw new Error("Not Implemented");
        }

        public getElement = (pos: number):T => {
            throw new Error("Not Implemented");
        }

        public insert = (elemnt: T) => {
            throw new Error("Not Implemented");
        }

        public append = (element: T) => {
            this.dataStore[this.pos++] = element;
        }

        public remove = (element: T) => {
            throw new Error("Not Implemented");
        }

        public front = () => {
            throw new Error("Not Implemented");
        }

        public end =() => {
            throw new Error("Not Implemented");
        }

        public prev = () => {
            throw new Error("Not Implemented");
        }

        public next = () => {
            throw new Error("Not Implemented");
        }

        public currPos = () => {
            throw new Error("Not Implemented");
        }

        public moveTo = () => {
            throw new Error("Not Implemented");
        }

    }

    angular.module("app.abstractDataTypes").service("list", [List]);
} 