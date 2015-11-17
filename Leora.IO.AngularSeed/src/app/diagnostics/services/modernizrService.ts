module app.diagnostics {

    "use strict";

    export class ModernizrService implements IModernizrService {
        constructor() {

        }

        public get autofocus(): boolean {
            return Modernizr.autofocus;
        }

        public get fontface(): boolean {
            return Modernizr.fontface;
        }

        public get backgroundsize(): boolean {
            return Modernizr.backgroundsize;
        }

        public get borderimage(): boolean {
            return Modernizr.borderimage;
        }

        public get borderradius(): boolean {
            return Modernizr.borderradius;
        }

        public get boxshadow(): boolean {
            return Modernizr.boxshadow;
        }

        public get flexbox(): boolean {
            return Modernizr.flexbox;
        }

        public get hsla(): boolean {
            return Modernizr.hsla;
        }

        public get multiplebgs(): boolean {
            return Modernizr.multiplebgs;
        }

        public get opacity(): boolean {
            return Modernizr.opacity;
        }

        public get rgba(): boolean {
            return Modernizr.rgba;
        }

        public get textshadow(): boolean {
            return Modernizr.textshadow;
        }

        public get cssanimations(): boolean {
            return Modernizr.cssanimations;
        }

        public get csscolumns(): boolean {
            return Modernizr.csscolumns;
        }

        public get generatedcontent(): boolean {
            return Modernizr.generatedcontent;
        }

        public get cssgradients(): boolean {
            return Modernizr.cssgradients;
        }

        public get cssreflections(): boolean {
            return Modernizr.cssreflections;
        }

        public get csstransforms(): boolean {
            return Modernizr.csstransforms;
        }

        public get csstransforms3d(): boolean {
            return Modernizr.csstransforms3d;
        }

        public get csstransitions(): boolean {
            return Modernizr.csstransitions;
        }

        public get applicationcache(): boolean {
            return Modernizr.applicationcache;
        }

        public get canvas(): boolean {
            return Modernizr.canvas;
        }

        public get canvastext(): boolean {
            return Modernizr.canvastext;
        }

        public get draganddrop(): boolean {
            return Modernizr.draganddrop;
        }

        public get hashchange(): boolean {
            return Modernizr.hashchange;
        }

        public get history(): boolean {
            return Modernizr.history;
        }

        public get audio(): boolean {
            return Modernizr.audio.mp3;
        }

        public get video(): boolean {
            return Modernizr.video.webm;
        }

        public get indexeddb(): boolean {
            return Modernizr.indexeddb;
        }

        public get input(): boolean {
            return Modernizr.input.required;
        }

        public get inputtypes(): boolean {
            return Modernizr.inputtypes.tel;
        }

        public get localstorage(): boolean {
            return Modernizr.localstorage;
        }

        public get postmessage(): boolean {
            return Modernizr.postmessage;
        }

        public get sessionstorage(): boolean {
            return Modernizr.sessionstorage;
        }

        public get websockets(): boolean {
            return Modernizr.websockets;
        }

        public get websqldatabase(): boolean {
            return Modernizr.websqldatabase;
        }

        public get webworkers(): boolean {
            return Modernizr.webworkers;
        }

        public get geolocation(): boolean {
            return Modernizr.geolocation;
        }

        public get inlinesvg(): boolean {
            return Modernizr.inlinesvg;
        }

        public get smil(): boolean {
            return Modernizr.smil;
        }

        public get svg(): boolean {
            return Modernizr.svg;
        }

        public get svgclippaths(): boolean {
            return Modernizr.svgclippaths;
        }

        public get touch(): boolean {
            return Modernizr.touch;
        }

        public get webgl(): boolean {
            return Modernizr.webgl;
        }


    }

    angular.module("app.diagnostics").service("modernizrService", [ModernizrService]);
} 