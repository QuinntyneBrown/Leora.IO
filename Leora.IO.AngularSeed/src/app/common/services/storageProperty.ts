module app.common {

    "use strict";

    export class StorageProperty implements IStorageProperty {

        constructor(public commonStorage: common.IStorage) {

        }

        public name: string;

        public data: any = null;

        public get = () => {

            if (this.data && this.data.length > 0) {
                return this.data;
            }

            try {
                var storageData = this.commonStorage.getByName({ name: this.name }).value;

                if (storageData && storageData.length > 0) {
                    this.data = storageData;
                }

            } catch (error) {

            }

            return this.data;

        }

        public set = (value: any, limit?: number) => {

            if (limit) {
                var diff = value.length - limit;
                if (diff > 0) {
                    for (var i = 0; i < diff; i++) {
                        value.splice(i, 1);
                    }
                }
            }
            this.data = value;
            this.commonStorage.put({ name: this.name, value: value });
        }

        public push = (value: any) => {
            var items = this.get();

            if (items) {
                items.push(value);
            } else {
                items = [value];
            }
            this.set(items);
        }

        public instance = (name: string, initialValue?: any) => {
            var storageProperty = new StorageProperty(this.commonStorage);
            storageProperty.name = name;

            if (initialValue && !storageProperty.get()) {
                storageProperty.set(initialValue);
            }

            return storageProperty;
        }
    }

    angular.module("app.common").service("storageProperty", ["commonStorage", StorageProperty]);
} 