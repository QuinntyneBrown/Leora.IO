declare module app.i18n {

    interface IPhrasesProvider {
        configure(value: string);    
    }

    export interface II18nService {
        get(cultureCode:string, phrases:string[]): ng.IPromise<any>;
    }

    export interface II18nSession {
        cultureCode: string;
        translations:any[];
    }

    export interface II18nManager {
        cultureCode: string;
        translatedPhrases: II18nTranslatedPhrases;
    }

    export interface II18nTranslatedPhrases {
        cultureCode: string;
        phrases: ITranslatedPhrase[];
    }

    export interface ITranslatedPhrase {
        hashCode: number;
        text: string;
        translation:string;
    }

    export interface IPhrase {
        text: string;
        hashCode: number;
    }

    export interface INumeral {
        language: string;
        acceptedCultureCodes: string[];
    }


    export interface IHashable {
        hashCode():number;
    }
} 

