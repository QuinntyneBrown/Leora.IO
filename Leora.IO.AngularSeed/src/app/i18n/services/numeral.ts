/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.i18n {
    
    export class Numeral implements INumeral {

        public set language(value: string) {
            if (this.acceptedCultureCodes.indexOf(value) > 0) {
                numeral.language(value);
            } else {
                numeral.language("en");    
            }
        }

        public get acceptedCultureCodes() {
            return ["en-US", "fr-CA"];
        }
    }

    angular.module("app.i18n").service("numeral", [Numeral]);
} 