$(function () {
    // Burger menu
    $("#burger-trigger").on("click", function () {
        $(this).toggleClass("close");
        $("#burger-icon").toggleClass("close");
    });

    // Collapsible sections
    $(".collapse-section-header").on("click", function() {        
        var sectionBody = $(this).next(".collapse-section-body");   
        sectionBody.slideToggle();
    });
});