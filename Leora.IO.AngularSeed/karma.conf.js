// Karma configuration
// Generated on Mon Jun 08 2015 14:23:11 GMT-0400 (Eastern Daylight Time)

module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
        'scripts/jquery-2.1.3.js',
        'scripts/jquery.signalR-2.2.0.js',
        'scripts/numeral/numeral.js',
        'scripts/numeral/languages/fr-CA.js',
        'scripts/angular.js',
        'scripts/angular-ui/ui-bootstrap.js',
        'scripts/angular-ui/ui-bootstrap-tpls.js',
        'scripts/angular-route.js',
        'scripts/angular-ui-router.js',
        'scripts/angular-mocks.js',
        'scripts/moment/moment.js',
        'scripts/moment/locale/fr-ca.js',
        'scripts/modernizr-2.8.3.js',
        'scripts/angular-moment.js',
        'scripts/html2canvas.js',
        'scripts/ng-infinite-scroll.js',
        'app.js',
        'tests.js'
    ],


    // list of files to exclude
    exclude: [
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: ['Chrome'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false
  });
};
