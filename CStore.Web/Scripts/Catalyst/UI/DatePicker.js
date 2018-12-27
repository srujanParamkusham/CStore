/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////////////////////////////////////////
//
//Date Picker Rendering/Helper Methods
//This library relies on the bootstrap datetimepicker library to render date/time pickers on
//fields that are specified to use the picker.
//
/////////////////////////////////////////////////////
Cat.UI.DatePicker = (function ($) {

    /////////////////////////////////////////////////////
    //
    //initializeDatePickers
    //This method is used to attach the date pickers and date/time pickers to elements
    //with the appropriate classes.
    //Params:
    //startingElementSelector - if null then the all elements in the body will be searched to determine if the date time pickers should be applied to them.
    //                          if not null, then it should be a jquery selector (i.e., #elementId) to start at, and only elements under that starting element will be evaluated (this is useful for modal forms).
    //
    /////////////////////////////////////////////////////
    var initializeDatePickers = function (startingElementSelector) {
        if (!startingElementSelector) {
            startingElementSelector = "body";
        }

        //console.log("startingElementSelector is " + startingElementSelector);

        $(startingElementSelector).find('.datepicker').datetimepicker({
            //http://eonasdan.github.io/bootstrap-datetimepicker/Options
            //If true, On show, will set the picker to the current date/time
            useCurrent: false,
            format: 'MM/DD/YYYY'
        });


        $(startingElementSelector).find('.datetimepicker').datetimepicker({
            //http://eonasdan.github.io/bootstrap-datetimepicker/Options
            //If true, On show, will set the picker to the current date/time
            useCurrent: false
        });
        
        $(startingElementSelector).find('.timepicker').datetimepicker({
            //http://eonasdan.github.io/bootstrap-datetimepicker/Options
            //If true, On show, will set the picker to the current date/time
            useCurrent: false,
            //Locale aware time format. See moment.js docs for formats.
            format: 'LT'
        });

        //Listen for the custom dp.change event the eonasdan datetimepicker fires when a date is picked through the GUI.
        //If it fires, then fire an on change event on its associated input field.
        $('body').on('dp.change', function (event) {
            if (event == null || event.target == null || event.target.firstElementChild == null) {
                return;
            }
            $(event.target.firstElementChild).change();
        });


        /*
        $('body').on('DOMNodeInserted', 'div', function (event) {
            console.log("datepicker dom node inserted called");
            $(this).datetimepicker({
                //http://eonasdan.github.io/bootstrap-datetimepicker/Options
                //If true, On show, will set the picker to the current date/time
                useCurrent: false,
                format: 'MM/DD/YYYY'
            });
        });

        $('body').on('DOMNodeInserted', '.datetimepicker', function (event) {
            console.log("datetimepicker dom node inserted called");
            $(this).datetimepicker({
                //http://eonasdan.github.io/bootstrap-datetimepicker/Options
                //If true, On show, will set the picker to the current date/time
                useCurrent: false
            });
        });
        */
    };
    
    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        initializeDatePickers: initializeDatePickers
    };
})(jQuery);
