/////////////////
// setup namespace
/////////////////
namespace("Cat.UI");

//////////////////////////////
// Form State Control
//////////////////////////////
Cat.UI.UnsavedFormChanges = (function ($, document, window, undefined) {
    "use strict";

    var settings = {
        message: 'You have unsaved changes.',
        buttons: undefined,
        highlightChanges: true
    },
    $element,
    defaultState,
    inputState;

    /////////////////////////////////////////////////////
    // Initialize
    /////////////////////////////////////////////////////
    function publicInit(selector, options) {
        // If the calling code did not put the # in the selector, add it for them
        if (selector.substring(0, 1) != '#') {
            selector = '#' + selector;
        }

        // Cache a reference to the DOM element (the form)
        $element = $(selector);

        // If the element was found, run the init code
        if ($element.length > 0) {
            settings = $.extend({}, settings, options);

            // Get the default state of the form (fields and values)
            var defaultState = serializeToString();
            formatButtonIDs();

            // Store some initial values related to the form so we can track when it changes
            $element.data('monitor', 1)
                .data('changed', 0)
                .data('state', defaultState)
                .find('*')
                .change(function () {
                    onInputChange();
                    trackChanges($(this));
                });

            // This will fire if the user tries to navigate away from the current page
            $(window).bind('beforeunload', function () {
                // Still a work in progress.  The idea is that we highlight
                // the fields that were detected as changed so the user can
                // see what unsaved changes they have.
                if (settings.highlightChanges) {
                    highlightChanges();
                }

                // If there were changes, prompt the user to confirm they want to leave the page
                if (hasChanged()) {
                    return settings.message;
                }
            });

            // Disable the form related buttons by default.  They will be enabled when a change is made
            disableButtons();

            // Only store the defaults of each input if the calling code has elected to highlight changes
            if (settings.highlightChanges) {
                cacheInputDefaults();
            }
        }
    }

    /////////////////////////////////////////////////////
    // Cache Input Defaults
    /////////////////////////////////////////////////////
    function cacheInputDefaults() {
        inputState = new Array();

        var formID = getElementIdOrName($element);

        var idx = 0;

        $element.find('input, select, textarea').each(function () {
            var $this = $(this),
                id = getElementIdOrName($this);

            if (!id) {
                id = formID + '_input_' + idx;
                $this.attr('id', id).attr('name', id);
            }

            var iObj = { id: id, defaultValue: $this.val(), newValue: $this.val(), changed: false, defaultBorder: $this.css('border') };

            inputState.push(iObj);
        });
    }

    /////////////////////////////////////////////////////
    // Get Element Id or Name
    /////////////////////////////////////////////////////
    function getElementIdOrName($elem) {
        var id;

        if ($elem.length > 0) {
            id = $elem.attr('id');
            id = id != undefined ? id : $elem.attr('name');
        }

        return id;
    }

    /////////////////////////////////////////////////////
    // Track Changes
    /////////////////////////////////////////////////////
    function trackChanges($this) {
        if ($this.length > 0) {
            var id = $this.attr('id');

            for (var i = 0; i < inputState.length; i++) {
                var item = inputState[i];

                if (item.id == id) {
                    if (item.defaultValue != $this.val()) {
                        item.changed = true;
                        item.newValue = $this.val();
                    } else {
                        item.changed = false;
                        item.newValue = $this.val();
                    }
                }
            }
        }
    }

    /////////////////////////////////////////////////////
    // Highlight Changes
    /////////////////////////////////////////////////////
    function highlightChanges() {
        for (var i = 0; i < inputState.length; i++) {
            var $elem = $('#' + inputState[i].id);

            if (inputState[i].changed) {
                inputState[i].defaultBorder = $elem.css('border');
                $elem.css('border', '2px solid orange');
            } else {
                $elem.css('border', inputState[i].defaultBorder);
            }
        };
    }

    /////////////////////////////////////////////////////
    // On Input Change
    /////////////////////////////////////////////////////
    function onInputChange() {
        var c = serializeToString(),
            val;

        if (c == defaultState) val = 0
        else val = 1;

        $element
            .data('changed', val)
            .data('state', c);

        if (hasChanged()) {
            enableButtons();
        } else {
            disableButtons();
        }
    }

    /////////////////////////////////////////////////////
    // To Hash Code
    /////////////////////////////////////////////////////
    function toHashCode(input) {
        return input.split("").reduce(function (a, b) { a = ((a << 5) - a) + b.charCodeAt(0); return a & a }, 0);
    }

    /////////////////////////////////////////////////////
    // Serialize to String
    /////////////////////////////////////////////////////
    function serializeToString() {
        var jsonStr = JSON.stringify($element.serializeArray());

        return toHashCode(jsonStr);
    }

    /////////////////////////////////////////////////////
    // Has Changed
    /////////////////////////////////////////////////////
    function hasChanged() {
        return $element.data('changed');
    }

    /////////////////////////////////////////////////////
    // Format Button IDs
    /////////////////////////////////////////////////////
    function formatButtonIDs() {
        if (settings.buttons) {
            for (var i = 0; i < settings.buttons.length; i++) {
                var val = settings.buttons[i];

                if (val[0] != '#') {
                    settings.buttons[i] = '#' + val;
                }
            }
        }
    }

    /////////////////////////////////////////////////////
    // Enable Buttons
    /////////////////////////////////////////////////////
    function enableButtons() {
        for (var i = 0; i < settings.buttons.length; i++) {
            $(settings.buttons[i]).prop('disabled', false);
        }
    }

    /////////////////////////////////////////////////////
    // Disable Buttons
    /////////////////////////////////////////////////////
    function disableButtons() {
        for (var i = 0; i < settings.buttons.length; i++) {
            $(settings.buttons[i]).prop('disabled', true);
        }
    }

    return {
        init: publicInit
    }
})(jQuery, document, window);