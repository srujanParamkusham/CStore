/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////////////////////////////////////////
//
//NumericField Rendering/Helper Methods
//This library relies on the autoNumerc library
//
/////////////////////////////////////////////////////
Cat.UI.NumericField = (function ($) {

    /////////////////////////////////////////////////////
    //
    //initializeNumericFields
    //This method is used to attach the autoNumeric to elements
    //with the appropriate classes.
    //Params:
    //startingElementSelector - if null then the all elements in the body will be searched to determine if the date time pickers should be applied to them.
    //                          if not null, then it should be a jquery selector (i.e., #elementId) to start at, and only elements under that starting element will be evaluated (this is useful for modal forms).
    //
    /////////////////////////////////////////////////////
    var initializeNumericFields = function (startingElementSelector) {
        if (!startingElementSelector) {
            startingElementSelector = "body";
        }
        $(startingElementSelector).find('.numeric-field').each(function(){
            $(this).autoNumeric('init')
        }
        );
    };

    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        initializeNumericFields: initializeNumericFields
    };
})(jQuery);
