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

var paths = {
    scripts: ["Scripts/**/*.js", "Scripts/**/*.ts", "Scripts/**/*.map"],
};

gulp.task('sass', () => gulp.src('Styles/**/*.s[a|c]ss')
    .pipe(sass({
        sourceMap: false
    }))
    .pipe(gulp.dest('wwwroot/css/'))
);

gulp.task('min:script', () => gulp.src(["wwwroot/js/!(*.min).js"], { base: '.' })
    .pipe(uglify())
    .pipe(gulp.dest('.'))
);

gulp.task('min:css', () => gulp.src("wwwroot/css/**/!(*.min).css", { base: '.' })
    .pipe(minifyCSS())
    .pipe(rename({        
        suffix: '.min',
    }))
    .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
    .pipe(gulp.dest('.'))
);

gulp.task('min', gulp.series(['sass', 'min:css']));

gulp.task('clean', () =>
    del([...bundleconfig.map(bundle => bundle.outputFileName), "wwwroot/js", "wwwroot/css"])
);

gulp.task('watch', () => {
    getBundles(regex.js).forEach(
        () => gulp.watch("Scripts/**/*.[js|ts]", gulp.series(["min:script"])));

    gulp.watch("Styles/**/*.s[a|c]ss", gulp.series(["sass", "min:css"]));

    getBundles(regex.css).forEach(
        bundle => gulp.watch(bundle.inputFiles, gulp.series(["min:css"])));
});

const getBundles = regexPattern =>
    bundleconfig.filter(bundle =>
        regexPattern.test(bundle.outputFileName)
    );

gulp.task("script", () =>
    gulp.src(paths.scripts)
        .pipe(gulp.dest("wwwroot/js"))
);

gulp.task('default', gulp.series(['script', 'min']));