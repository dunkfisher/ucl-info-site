/// <binding AfterBuild='less' />
module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        less: {
            development: {
                files: {
                    'wwwroot/css/main.css': 'wwwroot/css/main.less'
                }
            },
        }
    });

    grunt.loadNpmTasks('grunt-contrib-less');
        
    grunt.registerTask('default', ['less']);
};