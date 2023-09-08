$(function () {
    $(".card-header").on("click", function() {        
        var cardBody = $(this).next(".card-body");   
        cardBody.slideToggle();
    });
});