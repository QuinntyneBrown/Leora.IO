module app.ui {

    "use strict";

    export class AppFooterConfigProvider implements IAppFooterConfigProvider {
        constructor() {
            
        }

        public appFooterConfig:any = {};

        public configure = (options:any) => {
            this.appFooterConfig.appManager = options.appManager;
        };

        public $get = [() => { return this.appFooterConfig; }];
    }

    angular.module("app.ui").provider("appFooterConfig", [AppFooterConfigProvider]);
} 