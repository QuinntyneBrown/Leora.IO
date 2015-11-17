/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.common {

    "use strict";

    export class Storage {
        constructor(private $window:ng.IWindowService) {
            $window.onbeforeunload = () => {
                localStorage.setItem(this._storageId, JSON.stringify(this._items));
            }

            
        }

        private _items: any[];

        private _storageId: string;

        public instance = (storageId:string) => {
            var storage = new Storage(this.$window);
            storage._storageId = storageId;
            return storage;
        }

        public get = () => {

            if (this._items) {
                return this._items;
            }

            var storageEntry = localStorage.getItem(this._storageId);
            
            if (storageEntry === "undefined" || storageEntry === null || storageEntry === "null") {
                this._items = [];
            } else {
                this._items = JSON.parse(storageEntry);
            }

            return this._items;
        }

        public getByName = (params: INameValuePair) => {
            var items = this.get();

            var storageItem = null;

            _.each(items, (item: any) => {
                if (params.name === item.name) {
                    storageItem = item;
                }
            });

            return storageItem;
        }

        public put = (params: INameValuePair) => {
            var items = this.get();

            var itemExist = false;

            _.forEach(items,(item: any) => {
                if (params.name === item.name) {
                    itemExist = true;
                    item.value = params.value;
                    item.category = params.category;
                }
            });

            if (!itemExist) {
                items.push(params);                
            }
        }

    }

    angular.module("app.common").service("storage", ["$window",Storage]);
} 
