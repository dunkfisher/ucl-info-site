$(function () {
    // Burger menu
    $("#burger-trigger").on("click", function () {
        $(this).toggleClass("close");
        $("#burger-icon").toggleClass("close");
        $("body").toggleClass("no-scroll");
    });

    $(".sub-menu-toggle").on("click", function () {
        $(this).toggleClass("open");
    });

    // Collapsible sections
    $(".collapse-section-header").on("click", function() {        
        var sectionBody = $(this).next(".collapse-section-body");   
        sectionBody.slideToggle();
    });
});