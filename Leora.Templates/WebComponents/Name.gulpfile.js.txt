/// <binding AfterBuild='libs' Clean='clean' />

var gulp = require("gulp");
var concat = require('gulp-concat');
var Config = require('./gulpfile.config');
var karma = require("gulp-karma");
var gulpUtil = require("gulp-util");
var webpack = require("gulp-webpack");
var rename = require("gulp-rename");
var rimraf = require("rimraf");
var clean = require('gulp-clean');
var child_process = require("child_process");
var tsc = require('gulp-typescript');
var sourcemaps = require('gulp-sourcemaps');

var config = new Config();

var paths = {
    npm: './node_modules/',
    lib: './lib/'
};

var libs = [
    paths.npm + 'angular/angular.js',
    paths.npm + 'angular-route/angular-route.js',
    paths.npm + 'angular-sanitize/angular-sanitize.js',
    paths.npm + 'jquery/dist/jquery.js',
    paths.npm + 'rx/dist/rx.all.compat.js'
];

gulp.task("typedoc", function () {
    child_process.exec("typedoc --out ./docs/ ./wwwroot/ --module commonjs --jsx react --experimentalDecorators --ignoreCompilerErrors --exclude node_module");
});

gulp.task('libs', function () {
    return gulp.src(libs).pipe(gulp.dest(paths.lib));
});

gulp.task('remove-compiled-js', function () {
    return gulp.src(["./src/**/*.js", "./src/**/*.js.map"], { read: false })
    .pipe(clean());
});

gulp.task('clean', function (callback) {
    rimraf(paths.lib, callback);
});

gulp.task("webpack", ['remove-compiled-js'], function () {
    return gulp.src('src/main.ts')
    .pipe(webpack({
        resolve: {
            extensions: ["", ".js", ".ts"]
        },
        module: {
            loaders: [
                {
                    test: /\.ts$/, loader: "ts", exclude: [/node_modules/]
                },
                {
                    test: /\.css$/, exclude: [/node_modules/], loader: "style-loader!css-loader"
                },
                {
                    test: /\.html$/, loader: "raw"
                },
                {
                    test: /\.scss$/,
                    loaders: ["style", "css", "sass"]
                }
            ]
        }
    }))
    .pipe(rename("components.js"))
    .pipe(gulp.dest('dist/'));
});

gulp.task('run-unit-tests', ['compile-ts-tests'], function () {
    return gulp.src([
        './lib/jquery.js',
        './lib/rx.all.compat.js',
        './lib/angular.js',
        './lib/angular-route.js',
        './lib/angular-sanitize.js',
        './node_modules/angular-mocks/angular-mocks.js',
        './dist/components.js',
        './test/components.spec.js'
    ])
        .pipe(karma({
            configFile: 'karma.conf.js',
            action: 'run'
        }))
        .on('error', function (err) {
            console.log(err);
            this.emit('end');
        });
});


gulp.task('compile-ts-tests', ['remove-compiled-js'], function () {
    var sourceTsFiles = [config.appTypeScriptReferences,
                        './src/**/*.spec.ts'];

    var tsResult = gulp.src(sourceTsFiles)
                       .pipe(tsc({
                           target: 'ES5',
                           declarationFiles: false,
                           noExternalResolve: true
                       }));

    return tsResult.js
        .pipe(concat('components.spec.js'))
        .pipe(gulp.dest('./test/'));
});


gulp.task('watch', function () {
    gulp.watch([
        './src/**/*.ts', './src/**/*.html', './src/**/*.css', './src/**/*.scss'
    ], ['remove-compiled-js', 'webpack', 'run-unit-tests']);
});

gulp.task('default', ['remove-compiled-js','libs', 'webpack', 'run-unit-tests','watch']);