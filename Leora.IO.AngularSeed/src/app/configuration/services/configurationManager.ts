module app.configuration {

    "use strict";

    import IStorageProperty = common.IStorageProperty;

    export class ConfigurationManager implements IConfigurationManager {
        constructor(storageProperty: IStorageProperty) {
            this._configuration = storageProperty.instance("configuration", {});            
        }

        private _configuration: IStorageProperty;

        public get configuration() {
            return this._configuration.get();
        }

        public set configuration(value:any) {
            this._configuration.set(value);
        }

    }

    angular.module("app.configuration").service("configurationManager", ["storageProperty", ConfigurationManager]);
} 