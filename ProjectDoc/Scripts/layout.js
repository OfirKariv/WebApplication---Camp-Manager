var flag;


// initializing
$(document).ready(function () {
    $("#regiForm").css("display", "none");
    $("#loginArea").css("display", "none");
    flag = 0;
});


// img stuff
$("#welcomeImg").click(function () {
      flag = 1;
      $(this).animate({ width: '550px' }, "slow");
      $(this).delay(200).animate({ width: '250px' }, "slow");
      $("#welcomeMsg").delay(250).fadeOut("slow");
      $("#loginArea").css("display", "block");
      $(this).css("border", "none");
});

$("#welcomeImg").mouseenter(function () {
    if (flag == 0) {
        $(this).css("border", "1px solid black");
        $(this).css("border-radius", "200px");
        $(this).animate({ width: '450px' }, "fast");
    }
  });

$("#welcomeImg").mouseleave(function () {
    if (flag == 0) {
        $(this).css("border", "none");
        $(this).animate({ width: '350px' }, "slow");
    }
});



// layout page log-in
  $("#login-form-link").click(function (e) {
      $("#loginForm").delay(100).fadeIn(100);
      $("#regiForm").fadeOut(100);
      $('#register-form-link').removeClass('active');
      $(this).addClass('active');
      e.preventDefault();
  });

  $("#register-form-link").click(function (e) {
      $("#regiForm").delay(100).fadeIn(100);
      $("#loginForm").fadeOut(100);
      $('#login-form-link').removeClass('active');
      $(this).addClass('active');
      e.preventDefault();
  });

  $("#register-submit").on("click", function () {
      $("#loginArea").css("display", "none");
  });

  $("#login-submit").on("click", function () {
      $("#loginArea").css("display", "none");
  });

// for about page carousel

    $('#custom_carousel').on("slide.bs.carousel", function (evt) {
      $('#custom_carousel .controls li.active').removeClass('active');
      $('#custom_carousel .controls li:eq('+$(evt.relatedTarget).index()+')').addClass('active');
    });

    // explore js 
    
    $(document).ready(function () {

	$('.star').on('click', function () {
      $(this).toggleClass('star-checked');
    });

    $('.ckbox label').on('click', function () {
      $(this).parents('tr').toggleClass('selected');
    });

    $('.btn-filter').on('click', function () {
      var $target = $(this).data('target');
      if ($target != 'all') {
        $('.table tr').css('display', 'none');
        $('.table tr[data-status="' + $target + '"]').fadeIn('slow');
      } else {
        $('.table tr').css('display', 'none').fadeIn('slow');
      }
    });

 });



    