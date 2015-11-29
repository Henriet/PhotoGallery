$(function () {

    "use strict";

    // REMOVE # FROM URL
    $('a[href="#"]').click(function (e) {
        e.preventDefault();
    });

    // Backstretch
    $(".home").backstretch([
	  "images/slider/slider-img1.jpg",
	  "images/slider/slider-img2.jpg"
    ], { duration: 3000, fade: 750 });

    // TOOLTIP	
    $(".menu li .fa").tooltip({
        placement: "right"
    });

    // Gallery FILTERS
    var $grid = $('#gallery-grid');
    $grid.shuffle({
        itemSelector: '.gallery-grid', // the selector for the items in the grid
        speed: 500 // Transition/animation speed (milliseconds)
    });
    /* reshuffle when user clicks a filter item */
    $('#gallery-filter li a').click(function (e) {
        // set active class
        $('#gallery-filter li a').removeClass('active');
        $(this).addClass('active');
        // get group name from clicked item
        var groupName = $(this).attr('data-group');
        // reshuffle grid
        $grid.shuffle('shuffle', groupName);
    });

    //MAGNIFIC POPUP
    $('#gallery-grid').magnificPopup({
        delegate: 'a',
        type: 'image',
        gallery: {
            enabled: true
        }
    });

    //AJAX CONTACT FORM
    $(".contact-form").submit(function () {
        var rd = this;
        var url = "sendemail.php"; // the script where you handle the form input.
        $.ajax({
            type: "POST",
            url: url,
            data: $(".contact-form").serialize(), // serializes the form's elements.
            success: function (data) {
                $(rd).prev().text(data.message).fadeIn().delay(3000).fadeOut();
            }
        });
        return false; // avoid to execute the actual submit of the form.
    });

    // GOOGLE MAP
    function initialize($) {
        var mapOptions = {
            zoom: 8,
            center: new google.maps.LatLng(17.421306, 78.457553),
            disableDefaultUI: true
        };
        var map = new google.maps.Map(document.querySelector('.map'), mapOptions);
    }
    google.maps.event.addDomListener(window, 'load', initialize);

});