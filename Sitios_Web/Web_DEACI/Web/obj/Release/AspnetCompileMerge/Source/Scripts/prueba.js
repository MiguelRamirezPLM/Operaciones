// Side Menu

$(document).ready(function () {
    $('nav').hover(function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('nav').addClass('nav-hover');
        $('.main-content').addClass('push');
        $('nav ul .menu-item').stop().fadeIn(1200);
    }, function () {
        $('.subnav').fadeOut('fast');
        $('.menu-item').hide();
        $('nav').removeClass('nav-hover');
        $('.main-content').removeClass('push');

    });

    // Display Sub Menu

    $('nav ul .has-sub').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('.subnav').filter(':not(:animated)').slideToggle();
    });

    // Header User Panel

    $('header .userpanel .name').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('header .userpanel .name').toggleClass('active');
        $('header .userpanel .name span').toggleClass('arrow');
        $("header .userpanel .drop").stop().fadeToggle(500);
    });
    $('.main-content').click(function () {
        $('header .userpanel .name').removeClass('active');
        $('header .userpanel .name span').removeClass('arrow');
        $("header .userpanel .drop").filter(':not(:animated)').fadeOut();
    });

    // Container Scroll Bar

    $("html").niceScroll({ styler: "fb", cursorcolor: "#e8403f", cursorwidth: '6', cursorborderradius: '10px', background: '#404040', spacebarenabled: false, cursorborder: '', zindex: '1000' });
});