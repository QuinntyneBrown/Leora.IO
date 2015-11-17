module app.common {
    
    export class LocalStorageService implements ILocalStorageService {
        constructor(private commonStorage: IStorage) {
            
        }

        public set = (key: string, value: any) => {
            this.commonStorage.put({ name: key, value: value });
            return this.commonStorage.getByName({ name: key });
        }

        public get = (key: string) => {

            var property = this.commonStorage.getByName({ name: key });

            if (property)
                return this.commonStorage.getByName({ name: key }).value;

            return null;
        }

        public remove = (key: string) => {
            this.commonStorage.put({ name: key, value: null });
            return true;
        }

        public clearAll = () => {
            
        }
        
        public bind = ($scope: ng.IScope, key: string, opts?: {
            defaultValue?: any;
            storeName?: string;
        }) => {
            return {};
        }

        public unbind = ($scope: ng.IScope, key: string, storeName?: string) => {
            return {};
        }        
    }

    angular.module("app.common").service("localStorageService", ["commonStorage",LocalStorageService]);
} 