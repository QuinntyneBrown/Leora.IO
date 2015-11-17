module app.i18n {

    "use strict";
      
     export class PhrasesProvider {
         constructor() {

         }

         public configure = (value:any) => {
             if (angular.isArray(value)) {
                 value.forEach((item: any) => {
                     this.phrases.push({
                         text: item,
                         hashCode: item.hashCode()
                     });
                 });
             } else {
                 this.phrases.push({
                     text: value,
                     hashCode: value.hashCode()
                 });
             } 
         }

         public phrases: IPhrase[] = [];

         public $get = [() => {
             return this.phrases;
         }];
     }

     angular.module("app.i18n").provider("phrases", [PhrasesProvider]);
 }