module app.common {

    "use strict";

    export class ApplicationProvider implements common.IApplicationProvider  {
        
        constructor() {

        }

        public configure = (options: any) => {
            this.application.name = options.applicationName;
        }

        public application:any = {};


        public $get = [() => {
            return this.application;
        }];
    }

    angular.module("app.common").provider("application", [ApplicationProvider]);
} 