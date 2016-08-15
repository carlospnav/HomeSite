
//Sets the details of each new content update square on the center element of the page.
function mouseOverElementDetails() {
    $('.squared-unit').on('mouseover', '.squared-unit-background', function () {

        $('#squared-unit-details-background h2').fadeTo(100, 0.15);

        $('.details-font-container').fadeTo('slow', 1);

        var element = $(this).find('a');

        $('.details-font:first-of-type p').text(element.attr('data-name'));

        $('.details-font:nth-of-type(2) p').text(element.attr('data-date'));
        
        $('.details-font:last-of-type p').text(element.attr('data-type'));

    });
    
}

//Entry point.
(function () {
    $(document).ready(function () {
        mouseOverElementDetails();
    });
})();