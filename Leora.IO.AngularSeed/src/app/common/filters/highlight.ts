module app.common {

    "use strict";

    export var highlight = ($sce:any) => {
        return (value, text) => {
            if (!value) {
                return $sce.trustAsHtml(text);
            }
            return $sce.trustAsHtml(text.replace(new RegExp(value, "gi"),(match) => {
                return '<span class="highlightedText">' + match + '</span>';
            }));

        }
    }

    angular.module("app.common").filter("highlight", ["$sce", highlight]);
} 