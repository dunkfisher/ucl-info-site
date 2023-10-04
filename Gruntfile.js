/// <binding AfterBuild='less' ProjectOpened='watch:scripts' />
module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        less: {
            development: {
                files: {
                    'wwwroot/css/main.css': 'wwwroot/css/main.less'
                }
            },
        },
        watch: {
            scripts: {
                files: ['**/*.less'],
                tasks: ['less'],
                options: {                    
                    livereload: true
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-watch');
        
    grunt.registerTask('default', ['less']);
};