module app.common {

    "use strict";

    export function formatNumber() {
        return (value: number, culture: string) => {

            var format = "0, 0.00";

            switch(culture) {            
                case "fr-CA":
                    format = "0, 0,00";
                    break;
            }

            return numeral(value).format(format);

            
        };
    }

    angular.module("app.common").filter("formatNumber", [formatNumber]);
} 