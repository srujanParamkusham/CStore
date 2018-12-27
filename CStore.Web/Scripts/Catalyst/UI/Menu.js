/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////////////////////////////////////////
//
//Menu Rendering/Helper Methods
//
/////////////////////////////////////////////////////
Cat.UI.Menu = (function ($) {
    /////////////////////////////////////////////////////
    //
    //initializeTopDropDownMenu
    //This method is needed to wire up the appropriate events on the
    //top navigation menu so that it supports multi-level menu items rendering
    //
    /////////////////////////////////////////////////////
    var initializeTopDropDownMenu = function () {
        $('ul.dropdown-menu [data-toggle=dropdown]').on('click', function (event) {
            // Avoid following the href location when clicking
            event.preventDefault();
            // Avoid having the menu to close when clicking
            event.stopPropagation();
            // If a menu is already open we close it
            //$('ul.dropdown-menu [data-toggle=dropdown]').parent().removeClass('open');
            // opening the one you clicked on
            $(this).parent().addClass('open');

            var menu = $(this).parent().find("ul");
            var menupos = menu.offset();

            if ((menupos.left + menu.width()) + 30 > $(window).width()) {
                var newpos = -menu.width();
            } else {
                var newpos = $(this).parent().width();
            }
            menu.css({ left: newpos });
        });

    };


    /////////////////////////////////////////////////////
    //
    //initializeStickySideMenu
    //This method is needed to wire up the appropriate events on the
    //side menu so that it remains expanded in the last item that was opened
    //
    /////////////////////////////////////////////////////
    var initializeStickySideMenu = function () {

        //
        //Get the current area and route from the url and update the left navigation with current item
        //Find the sidebars on the page and then find their active menu option.
        //
        //console.log("Path:" + window.location.pathname);
        var pathArray = window.location.pathname.split('/');
        var route = "";
        var arrayLength = pathArray.length;
        if (pathArray.length > 1) {
            for (var i = 1; i < arrayLength; i++) {
                route += '/' + pathArray[i];
            }
        } else {
            route = "/";
        }        
        
        //
        //Try to find a route in our current path that matches an href in the sidebar.
        //For example, if our current path is /Admin/AppAnnouncementMaintenance/Edit, then we 
        //want to check the sidebar first for a menu item to /Admin/AppAnnouncementMaintenance/Edit.
        //If nothing is found to it, then we check for /Admin/AppAnnouncementMaintenance
        //
        for (var i = 1; i < arrayLength; i++) {
            //console.log("Trying route:" + route);
            //If we found a match, then break the loop.
            if ($('nav.navbar-static-side').find('a[href="' + route + '"]').length >= 1) {
                break;
            }
            //Otherwise strip off the last trailing slash and any content after it and try again
            else {
                var indexOfLastSlash = route.lastIndexOf("/");
                route = route.substring(0, indexOfLastSlash);
            }
        }        

        //
        //For each link that matches in the sidebar, add the active class to highlight it, and then for its parent li and
        //ul tags we need to add the attributes to set them as active, and expand any parent drop down lists.
        //
        $('nav.navbar-static-side').find('a[href="' + route + '"]').each(function (index) {
            $(this).closest('li').addClass("active");
            $(this).parentsUntil($("nav.navbar-static-side"), "li").addClass("active");
            $(this).parentsUntil($("nav.navbar-static-side"), "ul").attr("aria-expanded", "true");
            $(this).parentsUntil($("nav.navbar-static-side"), "ul").addClass("in");
        });
        

        //TODO write logic to write a cookie for the menu item clicked as well.
        //Then if the above logic doesnt find any match for a menu item to expand, then
        //we will highlight the last menu item that exists in the cookie
        /*
        $('.nav a').bind('click', function(event) {
            if ($('.ace-icon').hasClass('fa-angle-right')) {
                $.cookie("ace-menu-expanded", "true");
            } else {
                $.cookie("ace-menu-expanded", "false");
            }
        });
        */
    };
    
    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        initializeTopDropDownMenu: initializeTopDropDownMenu,
        initializeStickySideMenu: initializeStickySideMenu
    };
})(jQuery);
