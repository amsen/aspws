// site.js

(function () {
    //var ele = $("#username");
    //ele.text = "Divya Sengupta";

    //var main = $("#main");
    //main.on("mouseenter",function () {
    //    main.style.background = "#888";
    //});

    //main.on("mouseleave", function () {
    //    main.style.background = "";
    //});

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    var me = $(this);
    //    alert(me.text());
    //})

    var $sideMenuAndWrapper = $("#sidebar, #wrapper");
    var $icon = $("#sideBarToggle i.fa")


    $("#sideBarToggle").on("click",function(){
        $sideMenuAndWrapper.toggleClass("hide-sidebar");

        if ($sideMenuAndWrapper.hasClass("hide-sidebar")) {
            //$(this).text("Show Sidebar");
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        }
        else {
            //$(this).text("Hide Sidebar");
            $icon.removeClass("fa-angle-right");
            $icon.addClass("fa-angle-left");
        };

    });


})();