 module app.ui {

     "use strict";

     export class AppHeaderConfigProvider implements IAppHeaderConfigProvider {
         constructor() {
             
         }

         public appHeaderConfig: any = {
         };

         public configure = (options: any) => {
             this.appHeaderConfig.securityManager = options.securityManager || "securityManager";
             this.appHeaderConfig.i18nManager = options.i18nManager || "i18nManager";
             this.appHeaderConfig.appTitle = options.appTitle || "Road Call Center";
             this.appHeaderConfig.appManager = options.appManager;
         }

         public $get = [() => { return this.appHeaderConfig; }];
     }

     angular.module("app.ui").provider("appHeaderConfig", [AppHeaderConfigProvider]);
 }