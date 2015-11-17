'use strict';

var gulp = require('gulp'),
    debug = require('gulp-debug'),
    inject = require('gulp-inject'),
    jasmine = require('gulp-jasmine'),
    tsc = require('gulp-typescript'),
    tslint = require('gulp-tslint'),
    sourcemaps = require('gulp-sourcemaps'),
    clean = require('gulp-clean'),
    concat = require('gulp-concat'),
    Config = require('./gulpfile.config'),
    typedoc = require("gulp-typedoc"),
    child_process = require("child_process");

var config = new Config();

/**
 * Generates the app.d.ts references file dynamically from all application *.ts files.
 */
gulp.task('gen-ts-refs', function () {
    var target = gulp.src(config.appTypeScriptReferences);
    var sources = gulp.src([config.allTypeScript, '!./src/app/**/*.spec.ts'], { read: false });
    return target.pipe(inject(sources, {
        starttag: '//{',
        endtag: '//}',
        transform: function (filepath) {
            return '/// <reference path="../..' + filepath + '" />';
        }
    })).pipe(gulp.dest(config.typings));
});

/**
 * Lint all custom TypeScript files.
 */
gulp.task('ts-lint', function () {
    return gulp.src(config.allTypeScript).pipe(tslint()).pipe(tslint.report('prose'));
});

/**
 * Compile TypeScript and include references to library and app .d.ts files.
 */
gulp.task('compile-ts', ['clean-ts'], function () {
    var sourceTsFiles = [config.allTypeScript,                //path to typescript files
                         config.libraryTypeScriptDefinitions, //reference to library .d.ts files
                         config.appTypeScriptReferences];     //reference to app.d.ts files

    var tsResult = gulp.src(sourceTsFiles)
                       .pipe(sourcemaps.init())
                       .pipe(tsc({
                           target: 'ES5',
                           declarationFiles: false,
                           noExternalResolve: true
                       }));

    tsResult.dts.pipe(gulp.dest(config.tsOutputPath));
    return tsResult.js
                    .pipe(sourcemaps.write('.'))
                    .pipe(gulp.dest(config.tsOutputPath));
});

gulp.task('compile-ts-tests', ['clean-ts'], function () {
    var sourceTsFiles = [config.libraryTypeScriptDefinitions,
                         config.appTypeScriptReferences,
                        './src/app/**/*.spec.ts'];

    var tsResult = gulp.src(sourceTsFiles)
                       .pipe(sourcemaps.init())
                       .pipe(tsc({
                           target: 'ES5',
                           declarationFiles: false,
                           noExternalResolve: true
                       }));

    tsResult.dts.pipe(gulp.dest(config.tsOutputPath));
    return tsResult.js
                    .pipe(sourcemaps.write('.'))
                    .pipe(gulp.dest(config.tsOutputPath));
});


gulp.task('concat-compiled-ts', ['compile-ts'], function () {
    return gulp.src(config.allJavaScript)
      .pipe(concat('app.js'))
      .pipe(gulp.dest('.'));
});

gulp.task('concat-compiled-ts-tests', ['compile-ts'], function () {
    return gulp.src(config.allJavaScriptTests)
      .pipe(concat('tests.js'))
      .pipe(gulp.dest('.'));
});

gulp.task('run-unit-tests', ['concat-compiled-ts-tests'], function () {
    return gulp.src('tests.js')
        .pipe(jasmine());
});

gulp.task('concat-less', function () {
    return gulp.src(config.allLess)
      .pipe(concat('app.less'))
      .pipe(gulp.dest('.'));
});

/**
 * Remove all generated JavaScript files from TypeScript compiltion.
 */
gulp.task('clean-ts', function () {
    var typeScriptGenFiles = [config.tsOutputPath,            // path to generated JS files
                              config.sourceApp + '**/*.js',    // path to all JS files auto gen'd by editor
                              config.sourceApp + '**/*.js.map' // path to all sourcemap files auto gen'd by editor
    ];

    // delete the files
    return gulp.src(typeScriptGenFiles, { read: false })
        .pipe(clean());
});

gulp.task('clean-less', function () {
    var lessGenFiles = [config.sourceApp + '**/*.css',    // path to all CSS files auto gen'd by editor
                              config.sourceApp + '**/*.css.map' // path to all sourcemap files auto gen'd by editor
    ];

    // delete the files
    return gulp.src(lessGenFiles, { read: false })
        .pipe(clean());
});

gulp.task('watch', function () {
    gulp.watch([config.allFiles], ['gen-ts-refs', 'clean-ts', 'clean-less', 'compile-ts', 'concat-less', 'concat-compiled-ts', 'concat-compiled-ts-tests', 'typedoc']);
});

gulp.task("typedoc", function () {
    child_process.exec("typedoc --out ./docs --target es5 --name FarApp ./src/");
});

gulp.task('default', ['clean-ts', 'clean-less', 'compile-ts', 'concat-compiled-ts', 'concat-compiled-ts-tests', 'concat-less', 'gen-ts-refs', 'typedoc', 'watch']);
