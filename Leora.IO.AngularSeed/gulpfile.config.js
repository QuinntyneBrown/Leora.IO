
'use strict';
var GulpConfig = (function () {

    function GulpConfig() {

        /* e2e */
        this.e2eSource = './Protractor/';

        this.source = './src/';
        this.sourceApp = this.source + 'app/';

        this.tsOutputPath = this.source + '/js';
        this.allJavaScript = [this.source + '/js/**/*.js', '!' + this.source + '/js/**/*.spec.js'];
        this.allJavaScriptTests = [this.source + '/js/**/*.spec.js'];
        this.allLess = [this.source + '**/*.less'];
        this.allTypeScript = this.sourceApp + '/**/*.ts';
        this.specsExclusion = "!" + this.sourceApp + '/**/*.spec.ts';

        this.allFiles = [this.allLess, this.allTypeScript];

        this.typings = './scripts/typings/';
        this.libraryTypeScriptDefinitions = './scripts/typings/**/*.ts';
        this.appTypeScriptReferences = this.typings + 'typeScriptApp.d.ts';
    }
    return GulpConfig;
})();
module.exports = GulpConfig;
