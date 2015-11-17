/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.i18n {

    "use strict";

    export function translate(i18nManager:II18nManager) {

        return (phrase: any, translatedPhrases?: II18nTranslatedPhrases) => {

            var currentTranslations: II18nTranslatedPhrases = translatedPhrases || i18nManager.translatedPhrases;

            var translatedPhrase:string = null;

            var phraseHashCode = phrase.hashCode();

            currentTranslations.phrases.forEach((item:ITranslatedPhrase) => {
                if (item.hashCode == phraseHashCode)
                    translatedPhrase = item.translation;
            });

            return translatedPhrase || phrase;
        };
    }

    angular.module("app.i18n").filter("translate", ["i18nManager",translate]);
} 