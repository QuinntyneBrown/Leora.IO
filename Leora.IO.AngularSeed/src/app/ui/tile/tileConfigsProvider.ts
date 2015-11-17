 module app.ui {

     "use strict";

     export class TilesConfigProvider {
         constructor() {
             

         }

         private _configs: any[] = [];

         public get configs() {
             return this._configs;
         }

         public $get = [() => {
             return this.configs;
         }]
     }
 }