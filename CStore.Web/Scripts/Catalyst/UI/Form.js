/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////////////////////////////////////////
//
//Form and Input Related Helper Methods
//This library contains helpers for forms and input fields
//to do things like set focus to the first available input field on a form.
//
/////////////////////////////////////////////////////
Cat.UI.Form = (function ($) {

    /////////////////////////////////////////////////////
    //
    //setFocusToFirstInputField
    //This method is used to set the focus to the first found input field on a page.
    //Params:
    //startingElementSelector - if null then the all elements in the body will be searched. The first found input element will get focus.
    //                          if not null, then it should be a jquery selector (i.e., #elementId) to start at, and only elements under that starting element will be evaluated.
    //
    /////////////////////////////////////////////////////
    var setFocusToFirstInputField = function (startingElementSelector) {
        if (!startingElementSelector) {
            startingElementSelector = "body";
        }

        //console.log("setFocusToFirstInputField startingElementSelector is " + startingElementSelector);

        //Many times the focus will not get set when a page first loads. 
        //Slowing things down with a minimal timeout callback to set the focus seems to do the trick.
        $(startingElementSelector).find('input,select').each(function () {
            var element = $(this);
            var elementFound = false;
            if (!element.is(':disabled') && element.hasClass("catalyst-select2")) {
                console.log("Setting focus to select 2 item");
                elementFound = true;
                setTimeout(function () {
                    //element.select2('open');
                    element.select2('focus');
                }, 5);
            }
            else if (element.is(':visible') && !element.is(':disabled')) {
                elementFound = true;
                setTimeout(function () {
                    element.focus();
                }, 5);
            }

            //Break out of the loop, we found the first element to focus on
            if (elementFound) {
                return false;
            }
        });
    };

    /////////////////////////////////////////////////////
    //
    //onFormResetClearCustomControls
    //This method is used to clear out any custom controls like select2 and date picker when a 
    //form reset button is clicked.
    //
    //The method will work for ALL forms on the page because we are targetting the body and then
    //targetting all reset buttons.
    //
    /////////////////////////////////////////////////////
    var onFormResetClearCustomControls = function () {
        $(document).ready(function () {
            // We call .off and the .on so that we don't hook up the same click function twice.
            // .off will remove any existing click event for the reset buttons and then we 
            // wire it up again.
            $('body').off('click', ':reset').on('click', ':reset', function () {
                var $btn = $(this);

                setTimeout(function () {
                    var $form = $btn.closest('form');

                    $form.find('.catalyst-select2').val(null).trigger('change');
                    $form.find('.datepicker, .datetimepicker, .timepicker').find('input').val('').attr('value', '').trigger('change');
                }, 10);
            });
        });
    };

    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        setFocusToFirstInputField: setFocusToFirstInputField,
        onFormResetClearCustomControls: onFormResetClearCustomControls
    };
})(jQuery);
