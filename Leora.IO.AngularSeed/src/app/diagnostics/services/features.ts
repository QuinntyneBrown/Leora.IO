/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.diagnostics {

    "use strict";

    export class Features {
        constructor(private modernizrService: IModernizrService) {

        }

        public set = (value:any) => {
            throw new Error("Can not set browswer Features");
        }

        public get = (): IFeature[]=> {

            var features: IFeature[] = [];

            features.push({ name: "autofocus", state: this.modernizrService.autofocus });
            features.push({ name: "fontface", state: this.modernizrService.fontface });
            features.push({ name: "backgroundsize", state: this.modernizrService.backgroundsize });
            features.push({ name: "borderimage", state: this.modernizrService.borderimage });
            features.push({ name: "borderradius", state: this.modernizrService.borderradius });
            features.push({ name: "boxshadow", state: this.modernizrService.boxshadow });
            features.push({ name: "flexbox", state: this.modernizrService.flexbox });
            features.push({ name: "hsla", state: this.modernizrService.hsla });
            features.push({ name: "multiplebgs", state: this.modernizrService.multiplebgs });
            features.push({ name: "opacity", state: this.modernizrService.opacity });
            features.push({ name: "rgba", state: this.modernizrService.rgba });
            features.push({ name: "textshadow", state: this.modernizrService.textshadow });
            features.push({ name: "cssanimations", state: this.modernizrService.cssanimations });
            features.push({ name: "csscolumns", state: this.modernizrService.csscolumns });
            features.push({ name: "generatedcontent", state: this.modernizrService.generatedcontent });
            features.push({ name: "cssgradients", state: this.modernizrService.cssgradients });
            features.push({ name: "cssreflections", state: this.modernizrService.cssreflections });
            features.push({ name: "csstransforms", state: this.modernizrService.csstransforms });
            features.push({ name: "csstransforms3d", state: this.modernizrService.csstransforms3d });
            features.push({ name: "csstransitions", state: this.modernizrService.csstransitions });
            features.push({ name: "applicationcache", state: this.modernizrService.applicationcache });
            features.push({ name: "canvas", state: this.modernizrService.canvas });
            features.push({ name: "canvastext", state: this.modernizrService.canvastext });
            features.push({ name: "draganddrop", state: this.modernizrService.draganddrop });
            features.push({ name: "hashchange", state: this.modernizrService.hashchange });
            features.push({ name: "history", state: this.modernizrService.history });
            features.push({ name: "audio", state: this.modernizrService.audio });
            features.push({ name: "video", state: this.modernizrService.video });
            features.push({ name: "indexeddb", state: this.modernizrService.indexeddb });
            features.push({ name: "input", state: this.modernizrService.input });
            features.push({ name: "inputtypes", state: this.modernizrService.inputtypes });
            features.push({ name: "localstorage", state: this.modernizrService.localstorage });
            features.push({ name: "postmessage", state: this.modernizrService.postmessage });
            features.push({ name: "sessionstorage", state: this.modernizrService.sessionstorage });
            features.push({ name: "websockets", state: this.modernizrService.websockets });
            features.push({ name: "websqldatabase", state: this.modernizrService.websqldatabase });
            features.push({ name: "webworkers", state: this.modernizrService.webworkers });
            features.push({ name: "geolocation", state: this.modernizrService.geolocation });
            features.push({ name: "inlinesvg", state: this.modernizrService.inlinesvg });
            features.push({ name: "smil", state: this.modernizrService.smil });
            features.push({ name: "svg", state: this.modernizrService.svg });
            features.push({ name: "svgclippaths", state: this.modernizrService.svgclippaths });
            features.push({ name: "Touch", state: this.modernizrService.touch });
            features.push({ name: "webgl", state: this.modernizrService.webgl });

            return features;
        }
    }

    angular.module("app.diagnostics").service("features", ["modernizrService", Features])
        .config([
        "sessionProvider", (sessionProvider: common.ISessionProvider) => {
            sessionProvider.configure("features");
        }]);

} 