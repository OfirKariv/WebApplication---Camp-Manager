$(document).ready(function () {
    $('#myCarousel').carousel({
        interval: 4000
    });

    $("#ajaxTmp").click(function () {
        $.ajax({
            url: "~/Lib/text.txt", success: function (result) {
                $("#ajaxTmp").html("<button> Hello </button>");
            }
        });
     });
    $("button").click(function () {
        $.ajax({
            url: "demo_test.txt", success: function (result) {
                $("#div1").html(result);
            }
        });
    });
    var clickEvent = false;
    $('#myCarousel').on('click', '.nav a', function () {
        clickEvent = true;
        $('.nav li').removeClass('active');
        $(this).parent().addClass('active');
    }).on('slid.bs.carousel', function (e) {
        if (!clickEvent) {
            var count = $('.nav').children().length - 1;
            var current = $('.nav li.active');
            current.removeClass('active').next().addClass('active');
            var id = parseInt(current.data('slide-to'));
            if (count == id) {
                $('.nav li').first().addClass('active');
            }
        }
        clickEvent = false;
    });
});