module.exports = function (config) {

    config.set({

        basePath: '',

        frameworks: ['jasmine'],

        coverageReporter: {
            type: 'text',
            dir: 'coverage/',
            file: null
        },

        exclude: [],

        reporters: ['progress', 'coverage'],

        preprocessors: { './dist/components.js': ['coverage'] },

        port: 9876,

        colors: true,

        logLevel: config.LOG_INFO,

        autoWatch: false,

        browsers: ['PhantomJS'],

        singleRun: false,

    });
};