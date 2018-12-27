/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////////////////////////////////////////
//
//MaskedField Rendering/Helper Methods
//This library relies on the jquery.maskedinput library
//
/////////////////////////////////////////////////////
Cat.UI.MaskedField = (function ($) {

    /////////////////////////////////////////////////////
    //
    //initializeMaskedFields
    //This method is used to attach the jquery.maskedinput to elements
    //with the appropriate classes.
    //Params:
    //startingElementSelector - if null then the all elements in the body will be searched to determine if the date time pickers should be applied to them.
    //                          if not null, then it should be a jquery selector (i.e., #elementId) to start at, and only elements under that starting element will be evaluated (this is useful for modal forms).
    //
    /////////////////////////////////////////////////////
    var initializeMaskedFields = function (startingElementSelector) {
        //Define custom mask characters here before initializing the controls
        //$.mask.definitions['~'] = '[+-]';

        if (!startingElementSelector) {
            startingElementSelector = "body";
        }
        $(startingElementSelector).find('.masked-field').each(function(){
            $(this).mask($(this).data("mask-value"), {
                placeholder: $(this).data("placeholder-value")
            });
        }
        );
    };

    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        initializeMaskedFields: initializeMaskedFields
    };
})(jQuery);
