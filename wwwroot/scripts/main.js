$(function () {
    $(".collapse-section-header").on("click", function() {        
        var sectionBody = $(this).next(".collapse-section-body");   
        sectionBody.slideToggle();
    });
});