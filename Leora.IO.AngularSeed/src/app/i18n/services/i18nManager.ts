module app.i18n {
    
    export class I18nManager implements II18nManager {
        constructor(private $rootScope: ng.IRootScopeService,
            private $window: ng.IWindowService,
            private localStorageService: common.ILocalStorageService,
            private phrases:string[]) {
            
        }
    
        private _cultureCode: string;

        public get cultureCode() {

            if (this._cultureCode)
                return this._cultureCode;

            if (this.localStorageService.get("cultureCode")) {
                this._cultureCode = this.localStorageService.get("cultureCode");
                return this._cultureCode;          
            }

            if (this.acceptedCultureCodes.indexOf(this.$window.navigator.userLanguage || this.$window.navigator.language) > 0) {
                this._cultureCode = this.$window.navigator.userLanguage || this.$window.navigator.language;    
                this.localStorageService.set("cultureCode", this._cultureCode);
                return this._cultureCode;
            }
               
            return "en-US";
        }

        public set cultureCode(value:string) {
            this._cultureCode = value;
            this.localStorageService.set("cultureCode", value);
            if (this.translatedPhrases && this.translatedPhrases.cultureCode != this._cultureCode) {
                this.$rootScope.$broadcast("cultureCodeChanged");
            }
        }

        private _translatedPhrases: II18nTranslatedPhrases = {
            cultureCode: null,
            phrases:[]
        }

        public get translatedPhrases() {
            if (this._translatedPhrases)
                return this._translatedPhrases;

            this._translatedPhrases = this.localStorageService.get("translatedPhrases" + this.cultureCode);

            return this._translatedPhrases;

        }

        public set translatedPhrases(value: II18nTranslatedPhrases) {
            this._translatedPhrases = value;
            this.localStorageService.set("translatedPhrases" + this.cultureCode, value);
        }

        public get acceptedCultureCodes() {
            return ["en-US", "fr-CA"];
        }

        public get defaultCultureCode() {
            return "en-US";
        }
    }

    angular.module("app.i18n").service("i18nManager", ["$rootScope", "$window", "localStorageService", "phrases", I18nManager]);
} 