/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////
//Validation Methods
/////////////////
Cat.UI.Validation = (function ($) {
    //
    //Return if the date is greater than or equal to today
    //
    var dateGreaterOrEqualToToday = function (value, element) {
        if (this.optional(element)) {
            return true;
        }

        var myDate = Date.parse(value);
        var now = new Date();
        var today = new Date(now.getFullYear(), now.getMonth(), now.getDate());

        return myDate >= today;
    };

    //Add client validation method to 
    $.validator.unobtrusive.adapters.addBool("dategreaterthanorequaltotoday");
    $.validator.addMethod("dategreaterthanorequaltotoday", dateGreaterOrEqualToToday);

    return {
        dateGreaterOrEqualToToday: dateGreaterOrEqualToToday
    };
})(jQuery);
