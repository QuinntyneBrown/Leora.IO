/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.common {

    "use strict";

    export var fire = (target: HTMLElement, type: string, properties: any) => {
        var htmlEvent = document.createEvent("HTMLEvents");

        htmlEvent.initEvent(type, true, true);

        for (var j in properties) {
            htmlEvent[j] = properties[j];
        }

        target.dispatchEvent(htmlEvent);
    }

    angular.module("app.common").value("fire",fire);

} 
