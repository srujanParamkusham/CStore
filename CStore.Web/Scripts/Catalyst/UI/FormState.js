/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");
//////////////////////////////
//save form state control
//////////////////////////////
Cat.UI.FormState = (function ($, document, window, undefined) {
    "use strict";

    // Default settings for the form state control
    var saveTimeout,
        onLoadCallbackName = 'form-state-on-load-callback',
        onSaveCallbackName = 'form-state-on-save-callback';

    /////////////////////////////////////////////
    // Initialization method
    //
    // This only needs to be called if you want 
    // the form to automatically save when any 
    // input changes.
    /////////////////////////////////////////////
    function publicInit(selector, options) {
        var defaults = {
            autoload: true,
            autosave: true
        }

        // If local storage isn't defined the user is in
        // an older browser so we should exit the method now.
        if (!localStorage)
            return;

        // $.extend will merge the contents of the defaults
        // with the options specified by the caller.
        var settings = $.extend({}, defaults, options),
            $elem = $(selector);

        if (settings.onLoad) {
            $elem.data(onLoadCallbackName, settings.onLoad);
        }

        if (settings.onSave) {
            $elem.data(onSaveCallbackName, settings.onSave);
        }

        // If the autoload setting is true, call the load
        // method for the form
        if (settings.autoload) {
            publicLoad(selector);
        }

        // If the form should autosave we call the save method
        // any time an input field changes.
        if (settings.autosave) {
            $('body').on('dp.change', function (event) {
                publicSave(selector);
            });

            $elem.find(':input').change(function () {
                // This clearTimeout/saveTimeout paradigm is
                // used so that if multiple inputs change very
                // quickly, we only save the form once.
                clearTimeout(saveTimeout);

                saveTimeout = setTimeout(function () {
                    publicSave(selector);
                }, 100);
            });
        }

        // See if the form has a reset button
        var $resetBtn = $elem.find('[type="reset"]');

        if ($resetBtn.length > 0) {
            // If the reset button was found add an event listener to it
            $resetBtn.on('click', function () {
                // Use set timeout so that the browser has time to clear the contents 
                // of the input elements before saving the form state.
                setTimeout(function () {
                    // We have to set the value attr on reset because we are setting it
                    // when the form loads.  This was causing issues with form reset 
                    // buttons.
                    $elem.find('input[type="text"]').attr('value', '');
                    setResetFlagOnLocalStorage(selector);
                }, 100);
            });
        }
    }

    /////////////////////////////////////////////
    // Clear form from local storage
    //
    // This method will clear the saved form from
    // local storage.  This will be used when the
    // user clicks the reset button of a form.
    /////////////////////////////////////////////
    function setResetFlagOnLocalStorage(selector) {

        if (!localStorage)
            return;

        // Get the element and the unique identifier
        var elem = $(selector),
            uid = getUid(selector);

        // Get the current value from local storage
        var ls = localStorage[uid];

        // Check if the local storage value exists
        if (ls != undefined && ls != null && ls != 'null') {
            // Parse the JSON back to an object and set a 'reset' value to true
            var formData = JSON.parse(ls);
            formData.reset = true;

            // Save the object back to local storage
            localStorage[uid] = JSON.stringify(formData);
            publicSave(selector);
        }
    }

    /////////////////////////////////////////////
    // Load method
    //
    // This method will restore form inputs to 
    // their previous state if the form has 
    // previously been saved into localStorage
    /////////////////////////////////////////////
    function publicLoad(selector) {
        try {
            // If local storage isn't defined the user is in
            // an older browser so we should exit the method now.
            if (!localStorage)
                return;

            var elem = $(selector),
                uid = getUid(selector);

            // Get the contents of the form from localStorage
            var ls = localStorage[uid];

            // Verify that the localStorage contents exist before moving on
            if (ls != undefined && ls != null && ls != 'null') {
                var formData = JSON.parse(ls);

                // Check if the form has expired (defaults to 1 hour)
                var expired = formHasExpired(formData.time);

                if (expired) {
                    // Clear the localStoreage contents
                    localStorage[uid] = null;
                } else {
                    // Loop through the form inputs and restore them
                    // to their previous state.
                    for (var i = 0; i < formData.data.length; i++) {
                        restoreInput(formData.data[i], selector);
                    }

                    // Only execute the callback if the form exists and is not expired
                    executeCallback(elem.data(onLoadCallbackName));
                }
            }
        } catch (err) {
            console.log(err);
        }
    }

    /////////////////////////////////////////////
    // Save method
    //
    // This method will serialize the form input
    // elements and save them to localStorage
    /////////////////////////////////////////////
    function publicSave(selector) {
        // Get our form and see if it has a save timeout defined
        var elem = $(selector),
            saveTimeout = elem.data('form-state-save-timeout');

        // If a save timeout was set, clear it out.  This prevents
        // running the save logic multiple times quickly.
        if (saveTimeout) {
            clearTimeout(saveTimeout);
        }

        // Create a new timeout that will execute our private
        // implementation of the save logic.
        saveTimeout = setTimeout(function () {
            save(selector);
        }, 200);

        // Save the new saveTimeout to the form
        elem.data('form-state-save-timeout', saveTimeout);
    }

    /////////////////////////////////////////////
    // Private save method
    //
    // This method is not exposed outside of the 
    // plugin.  It is called internally from the
    // publicSave method.
    /////////////////////////////////////////////
    function save(selector) {
        try {
            // If local storage isn't defined the user is in
            // an older browser so we should exit the method now.
            if (!localStorage)
                return;

            // Serialize the form into javascript objects
            var data = serializeForm(selector),
                elem = $(selector),
                uid = getUid(selector);

            var ls = localStorage[uid];
            var wasTriggeredFromReset = false;

            if (ls != undefined && ls != null && ls != 'null') {
                var formData = JSON.parse(ls);

                if (formData.reset === true) {
                    wasTriggeredFromReset = true;
                }
            }

            if (wasTriggeredFromReset) {
                // If save was triggered when the user reset the form, we need to clear
                // out the contents of local storage.
                localStorage[uid] = null;
            } else {
                // Add the current time to the data so we can track
                // when the last time the data was saved.
                var form = {
                    time: new Date().getTime(),
                    data: data
                };

                // Save the data in localStorage
                localStorage[uid] = JSON.stringify(form);

                executeCallback(elem.data(onSaveCallbackName));
            }
        } catch (err) {
            console.log(err);
        }
    }

    /////////////////////////////////////////////
    // Execute callback
    //
    // This method will execute a callback function
    // that was supplied in the plugin settings.
    /////////////////////////////////////////////
    function executeCallback(callback) {
        if (callback && callback !== undefined) {
            if (typeof callback == 'function') {
                callback();
            } else {
                var fn = window[callback];

                if (typeof fn === 'function') {
                    fn();
                }
            }
        }
    }

    /////////////////////////////////////////////
    // Check if the form has expired
    //
    // This method will check if the form hasn't 
    // been modified in the last hour.  This is 
    // default expiration time.
    /////////////////////////////////////////////
    function formHasExpired(formTime) {
        var now = new Date();
        var dif = now.getTime() - formTime;

        // Get the difference in seconds between the last time
        // the form was saved and the current time.
        var seconds = dif / 1000;
        seconds = Math.abs(seconds);

        if (seconds > 60 * 60 * 1) {
            return true;
        }

        return false;
    }

    /////////////////////////////////////////////
    // Restore an Input
    //
    // This method will restore an input to its
    // previous state.
    /////////////////////////////////////////////
    function restoreInput(input, context) {
        var name = input.name,
            value = input.value,
            type = input.type,
            multiple = input.multiple,
            select2 = input.select2;

        // Get the jQuery element for the input
        var $elem = getElement(input.name, input.type, input.value, context);
        var elemId = $elem.attr('id');

        // Different input types need to be handled differently
        switch (type) {
            case 'checkbox':
                var opt = value;

                // We need the value to be a boolean type so check that it actually
                // is and convert it to one if its not
                if (opt !== true && opt !== false) opt = opt == 'true' ? true : opt;
                if (opt !== true && opt !== false) opt = opt == 'on' ? true : false;

                $elem.prop('checked', opt);
                break;
            case 'radio':
                $elem.attr('checked', 'checked');
                break;
            case 'select':
                // Handle select lists that were made with select2 library
                if (select2) {
                    if (elemId == undefined || elemId == '') {
                        elemId = name;
                    }

                    var select2Function = window[elemId + '_LoadDisplayTextForValue'];

                    if (select2Function !== undefined && typeof select2Function === 'function') {
                        // Params: selectedValue, isInitializing, allowMultipleSelect) 
                        select2Function(value, true, multiple);
                    }
                } else if (multiple) {
                    // Set the selected value on the option for a multiple select list
                    $elem.find('option[value="' + value + '"]').attr('selected', 'selected');
                } else {
                    $elem.val(value);
                }
                break;
            default:
                // Setting both val and the value attr to avoid issues that sometimes arise with just using .val()
                $elem.val(value);
                $elem.attr('value', value);
                break;
        }
    }

    /////////////////////////////////////////////
    // Get jQuery Element
    //
    // This method will get the element wrapped
    // in the jQuery method.
    /////////////////////////////////////////////
    function getElement(name, type, value, context) {
        var element;

        switch (type) {
            case 'radio':
                // Radio buttons will have the same name attribute so we also
                // match on the value.  The context is used so jQuery can find
                // the element faster.
                element = $('input[name="' + name + '"][value="' + value + '"]', context);
                break;
            case 'select':
                // Try to find the element by ID first
                element = $('#' + name, context);

                // If the element wasn't found by ID, search by name
                if (element.length == 0) {
                    element = $('select[name="' + name + '"]', context);
                }
                break;
            default:
                // Try to find the element by ID first
                element = $('#' + name + '[type="' + type + '"]', context);

                // If the element wasn't found by ID, search by name and type
                if (element.length == 0) {
                    element = $('input[name="' + name + '"][type="' + type + '"]', context);
                }
                break;
        }

        return element;
    }

    /////////////////////////////////////////////
    // Serialize the Form
    //
    // This method will take all of the input
    // elements and save them into an array
    /////////////////////////////////////////////
    function serializeForm(selector) {
        var form = $(selector);

        var formData = [],   // Stores the input elements
            duplicates = [], // Keep track of any 'duplicate' elements (see below)
            formArray = form.serializeArray();

        // Loop through each input element
        for (var i = 0, n = formArray.length; i < n; ++i) {
            var input = formArray[i];

            // Check if the input is a duplicate
            //
            // An input is considered a duplicate if the name attribute
            // is the same on multiple input elements on the form.
            var isDuplicate = nameIsOnMultipleInputs(input.name, selector);

            // If the element is a duplicate, add it to a list
            // that will be processed later
            if (isDuplicate) {
                if ($.inArray(input.name, duplicates) == -1) {
                    duplicates.push(input.name);
                }
            } else {
                // Convert the element to a JSON object and store it in the array
                var elem = $('#' + input.name);
                formData.push(getInputElementJson(elem, input, selector))
            }
        }

        // Loop through the duplicate elemetns
        for (var i = 0; i < duplicates.length; i++) {
            // Find the elements with this current name attribute
            var elems = $('[name="' + duplicates[i] + '"]');

            // Loop through each element
            for (var j = 0; j < elems.length; j++) {
                var $elem = $(elems[j]),
                    input = { name: $elem.attr('name'), value: $elem.val() };

                // Get the element by using both name and type
                var ie = getInputElementJson($elem, input, selector);

                // If the input type is a checkbox, get its value by checking if
                // it is checked or not
                if (ie.type == 'checkbox') {
                    ie.value = $elem.is(':checked');
                }

                if (ie.type != 'radio' || (ie.type == 'radio' && ie.selected)) {
                    // ie = input element
                    //
                    // Add the input element to the list
                    formData.push(ie);
                }
            }
        }

        return formData;
    }

    /////////////////////////////////////////////
    // Convert Input Element to JSON
    //
    // This method will take a jQuery element and 
    // convert it to an anonymous JSON object
    /////////////////////////////////////////////
    function getInputElementJson(elem, input, selector) {
        // Get the type of the input (text, checkbox, etc...)

        var type = elem.attr('type');

        // Check for a standard input element using the name instead of ID
        if (type == undefined) {
            elem = $('input[name="' + input.name + '"]', selector);

            if (elem.length > 0) {
                type = elem.attr('type');
            }
        }

        // Check for a radio button or checkbox using the name instead of ID
        if (type == undefined) {
            elem = $('input[name="' + input.name + '"]:checked', selector);

            if (elem.length > 0) {
                type = elem.attr('type');
            }
        }

        // These 2 variables will be used for select lists.  Multiple will define if the
        // element allows multiple selects.  Select2 will define if the select list was
        // implemented using the select2 library.
        var multiple = false;
        var select2 = false;

        // The other input type would be a select
        if (type == undefined) {
            type = 'select';

            // Try to get a reference by ID
            var $select = $('#' + input.name);

            // Try to get a reference by name if the ID failed
            if ($select.length === 0)
                $select = $('[name="' + input.name + '"]');

            if ($select.length > 0) {
                if ($select.attr('multiple')) {
                    multiple = true;
                }

                if ($select.hasClass('catalyst-select2')) {
                    select2 = true;
                }
            }
        }

        var selected = false;

        if (type === 'radio' && elem && elem.is(':checked')) {
            selected = true;
        }

        return {
            value: input.value,
            selected: selected,
            name: input.name,
            type: type,
            multiple: multiple,
            select2: select2
        };
    }

    /////////////////////////////////////////////
    // Name Is On Multiple Inputs
    //
    // This method will take the name of an input
    // element and see if it exists on multiple 
    // input elements.  This can happen when radio
    // buttons are used.
    /////////////////////////////////////////////
    function nameIsOnMultipleInputs(name, context) {
        return $('[name="' + name + '"]', context).length > 1;
    }

    /////////////////////////////////////////////
    // Name Is On Multiple Inputs
    // 
    // Get a unique ID for the form.  This will be
    // used to differentiate one form from another.
    /////////////////////////////////////////////
    function getUid(selector) {
        selector = selector.replace('#', '');

        return getHashCode(selector);
    }

    /////////////////////////////////////////////
    // Name Is On Multiple Inputs
    // 
    // Turn a unique ID into a hashcode that will be
    // used to save a form to localstorage.
    /////////////////////////////////////////////
    function getHashCode(uid) {
        var hash = 0, i, chr, len;

        // A unique id is created by taking the current url and combining that
        // with the id of the form
        uid = window.location.href + uid;

        // Hash the key
        for (i = 0, len = uid.length; i < len; i++) {
            chr = uid.charCodeAt(i);
            hash = ((hash << 5) - hash) + chr;
            hash |= 0; // Convert to 32bit integer
        }

        return hash;
    }

    return {
        init: publicInit,
        save: publicSave,
        load: publicLoad
    };
})(jQuery, document, window);