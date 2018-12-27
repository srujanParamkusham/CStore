///////////////////////////////
//generic namespacing function
///////////////////////////////
var namespace = function (str, root) {
    var chunks = str.split('.');
    if (!root) {
        root = window;
    }
    var current = root;
    for (var i = 0; i < chunks.length; i++) {
        if (!current.hasOwnProperty(chunks[i])) {
            current[chunks[i]] = {};
        }
        current = current[chunks[i]];
    }
    return current;
};

/////////////////
//setup namespace
/////////////////
namespace("Cat");

/////////////////////////////////////////////////////
//
//Date Picker Rendering/Helper Methods
//This library relies on the bootstrap datetimepicker library to render date/time pickers on
//fields that are specified to use the picker.
//
/////////////////////////////////////////////////////
Cat = (function ($) {
    /////////////////////////////////////////////////////
    //
    //wireUpFormControls
    //This method is used to run the initialization logic for a screen. It should be called on every page load with
    //a null to the startingElementSelector, and on every modal dialog refresh or other refresh where html is dyanamically
    //written into the page. It will rebind things such as date pickers, set the input focus to the first found field, wire up
    //rich text editors, etc.
    //Params:
    //startingElementSelector - if null then the all elements in the body will be wired up.
    //                          if not null, then it should be a jquery selector (i.e., #elementId) to start at, and only elements under that starting element will be evaluated (this is useful for modal forms).
    //
    /////////////////////////////////////////////////////
    var wireUpFormControls = function(startingElementSelector) {
        if (!startingElementSelector) {
            startingElementSelector = "body";
        }

        //
        //Initialize date pickers
        //
        if (Cat.UI.DatePicker) {
            //console.log("Applying date pickers on id " + modalId);
            Cat.UI.DatePicker.initializeDatePickers(startingElementSelector);
        }

        //
        //Initialize numeric fields
        //
        if (Cat.UI.NumericField) {
            //console.log("Applying numerics on id " + modalId);
            Cat.UI.NumericField.initializeNumericFields(startingElementSelector);
        }

        //
        // Initialize masked fields
        //
        if (Cat.UI.MaskedField) {
            Cat.UI.MaskedField.initializeMaskedFields(startingElementSelector);
        }

        //
        //Initialize Rich Text Editors with TinyMCE
        //
        if (tinymce) {
            if (startingElementSelector && startingElementSelector == "body") {
                tinymce.remove({ selector: 'textarea.rich-text-editor' });
                tinymce.init({ selector: 'textarea.rich-text-editor' });
            }
            //
            //Special logic for modal windows
            //After tinymce has already been initialized, you need to remove and add it from the controls
            //for it to work.
            //
            else {
                $('textarea.rich-text-editor').each(function (index) {
                    var id = $(this).attr("id");
                    //console.log("reading editor to id " + id);
                    tinyMCE.execCommand('mceRemoveEditor', false, id);
                    tinymce.execCommand('mceAddEditor', false, id);
                });
            }
        }

        //
        //Wireup bootstrap tooltip display
        //
        $(startingElementSelector).find('[data-toggle="tooltip"]').tooltip({
            delay: { show: 1000, hide: 1000 }
        });


        //
        //Set focus to first input field
        //If the starting element is the body, then we replace that with the wrapper-content
        //class to ensure nothing in the header or menu becomes a candidate to set the focus to.
        //We want focus to go on one of the user input forms in the content area
        //
        var firstFieldToFocusSelector = startingElementSelector;
        if (firstFieldToFocusSelector && firstFieldToFocusSelector == "body") {
            if ($(".wrapper-content") != null && $(".wrapper-content").length) {
                firstFieldToFocusSelector = ".wrapper-content";
            }
        }
        Cat.UI.Form.setFocusToFirstInputField(firstFieldToFocusSelector);
        Cat.UI.Form.onFormResetClearCustomControls();
    };

    /////////////////////////////////////////////////////
    //
    // generateUniqueElementId
    // Generate a unique ID for an element.
    // This will take a prefix string, and append an _ and a string representing the
    // number of milliseconds since 1/1/1970.
    // Params:
    // elementPrefix - the string to be prepended to the number of ticks generated. An underscore will be
    // placed after this value and before the number of ticks.
    //
    /////////////////////////////////////////////////////
    function generateUniqueElementId(elementPrefix) {
        var d = new Date();
        //n will be the number of milliseconds since 1970/01/01. i.e., 1444833965003
        var ticks = d.getTime();
        var elementId = ticks;
        if (elementPrefix) {
            elementId = elementPrefix + "_" + ticks;
        }
        return elementId;
    }

    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        wireUpFormControls: wireUpFormControls,
        generateUniqueElementId: generateUniqueElementId
    };
})(jQuery);