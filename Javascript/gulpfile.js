// include gulp
var gulp = require('gulp');

// include plugins
var jshint = require('gulp-jshint');

// JS hint task
gulp.task('jshint', function () {
    gulp.src('./js/*.js')
    .pipe(jshint())
    .pipe(jshint.reporter('default'));

    gulp.src('./*.js')
    .pipe(jshint())
    .pipe(jshint.reporter('default'));
});


// default gulp task
gulp.task('default', ['jshint'], function () {
});