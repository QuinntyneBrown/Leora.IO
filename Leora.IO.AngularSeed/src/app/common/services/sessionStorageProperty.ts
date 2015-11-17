module app.common {

    "use strict";

    export class SessionStorageProperty implements ISessionStorageProperty  {

        constructor(public commonStorage: common.IStorage) {

        }

        public data: any = null;

        public name: string;

        public get = () => {

            if (this.data) {
                return this.data;
            }

            try {
                this.data = this.commonStorage.getByName({ name: this.name }).value;
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

            return this.set(items);
        }

        public instance = (name: string, initialValue?: any) => {

            var sessionStorageProperty = new SessionStorageProperty(this.commonStorage);

            sessionStorageProperty.name = name;
            if (initialValue && !sessionStorageProperty.get()) {
                sessionStorageProperty.set(initialValue);
            }

            document.getElementsByTagName("body")[0].addEventListener("sessionTimeout",() => {
                sessionStorageProperty.data = null;
                sessionStorageProperty.set(null);
            });

            return sessionStorageProperty;
        }

    }

    angular.module("app.common").service("sessionStorageProperty", ["commonStorage",SessionStorageProperty]);
} 