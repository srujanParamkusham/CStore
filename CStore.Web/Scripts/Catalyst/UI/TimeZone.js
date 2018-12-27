/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////////////////////////////////////////
//
//TimeZone Handling Related Helper Methods
//
/////////////////////////////////////////////////////
Cat.UI.TimeZone = (function ($) {

    /////////////////////////////////////////////////////
    //
    //storeUserBrowserTimeZoneInCookie
    //This method is used to read the users timezone from their browser settings and store
    //it in a cookie named "timezone".
    //This will allow subsequent requests to read the timezone from the cookie within controller methods and such.
    //
    /////////////////////////////////////////////////////
    var storeUserBrowserTimeZoneInCookie = function () {
        if (!jstz) {
            return;
        }

        var tz = jstz.determine();
        Cookies.set('timezone', tz.name(), { path: '/' });
    };
    
    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        storeUserBrowserTimeZoneInCookie: storeUserBrowserTimeZoneInCookie
    };
})(jQuery);
