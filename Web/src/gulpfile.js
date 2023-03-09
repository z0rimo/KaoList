/// <binding BeforeBuild='min' Clean='clean' ProjectOpened='watch' />
'use strict';

var gulp = require('gulp'),
    sass = require('gulp-sass')(require('sass')),
    uglify = require('gulp-uglify'),
    del = require('del'),
    minifyCSS = require('gulp-minify-css'),
    autoprefixer = require('gulp-autoprefixer'),
    rename = require('gulp-rename'),
    bundleconfig = require('./bundleconfig.json');

const regex = {
    css: /\.css$/,
    js: /\.js$/,
    sass: /\.s[a|c]ss$/
};

gulp.task('sass', () => gulp.src('client-app/src/scss/**/*.s[a|c]ss')
    .pipe(sass({
        sourceMap: false
    }))
    .pipe(gulp.dest('client-app/public/css/'))
);

gulp.task('min:css', () => gulp.src("client-app/src/css/**/!(*.min).css", { base: '.' })
    .pipe(minifyCSS())
    .pipe(rename({
        suffix: '.min',
    }))
    .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
    .pipe(gulp.dest('.'))
);

gulp.task('min', gulp.series(['sass', 'min:css']));

gulp.task('clean', () =>
    del([...bundleconfig.map(bundle => bundle.outputFileName), "client-app/src/css"])
);

gulp.task('watch', () => {
    gulp.watch("Styles/**/*.s[a|c]ss", gulp.series(["sass", "min:css"]));
});

const getBundles = regexPattern =>
    bundleconfig.filter(bundle =>
        regexPattern.test(bundle.outputFileNamez)
    );

gulp.task('default', gulp.series(['min']));
