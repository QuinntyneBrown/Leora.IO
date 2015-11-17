/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.common {

    "use strict";

    class CommonStorage implements IStorage {
        constructor(private storage: any) {
            this._storage = storage.instance("commonStorage");
        }

        private _storage: any;

        public storageId = "commonStorage";

        public get = () => {
            return this._storage.get();
        }

        public getByName = (params: INameValuePair) => {
            return this._storage.getByName(params);
        }

        public put = (params: INameValuePair) => {
            this._storage.put(params);
        }

        public clear = () => {
            
        }
    }

    angular.module("app.common").service("commonStorage", ["storage",CommonStorage]);

} 
