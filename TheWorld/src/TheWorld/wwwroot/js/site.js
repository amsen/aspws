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

    $("#sideBarToggle").on("click",function(){
        $sideMenuAndWrapper.toggleClass("hide-sidebar");

        if ($sideMenuAndWrapper.hasClass("hide-sidebar")) {
            $(this).text("Show Sidebar");
        }
        else {
            $(this).text("Hide Sidebar");
        };

    });


})();