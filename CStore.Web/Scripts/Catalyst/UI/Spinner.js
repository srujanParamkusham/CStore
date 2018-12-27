/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

//////////////////////////////
//ajax spinner control
//////////////////////////////
Cat.UI.Spinner = (function ($, document, window, undefined) {
    "use strict";

    // Settings can be overidden when calling init
    var settings = {
        initialized: false,
        enabled: true
    },
    counter = 0,
    elemId = 'cat-spinner-main',
    overlayId = 'cat-spinner-overlay',
    cssSpinnerId = 'cat-spinner-css-loader',
    imgSpinnerId = 'cat-spinner-img-loader',
    messageId = 'cat-spinner-message',
    $elem = undefined,
    $overlay = undefined,
    messageStack = [],
    _message,
    _defaultDelay = 1200,
    _delay = _defaultDelay,
    _delayTimeout,
    resizeTimeout;

    /////////////////////////////////////////////////////
    // Initialize
    //
    // This method will create the markup required to 
    // display the spinner and register the events that
    // are listened for when showing the spinner.
    /////////////////////////////////////////////////////
    function publicInit(options) {
        settings = $.extend({}, settings, options);

        if (!settings.initialized) {
            createHTML(settings.message);
            registerAjaxEvents();
            registerWindowEvents();

            if (!settings.message) {
                settings.message = 'Loading...';
            }

            publicSetMessage(settings.message);
        }

        settings.initialized = true;
    }

    /////////////////////////////////////////////////////
    // Show
    //
    // Display the loading message
    /////////////////////////////////////////////////////
    function publicShow(message) {
        // If the main initialization was run there won't be
        // much that happens within publicInit
        publicInit();

        // If a specific message was wet, show that instead
        // of the default
        if (message) {
            publicSetMessage(message);
        }

        counter++;

        // If the loader isnt already showing, show it.
        if (!isShowing()) {
            show();
        }
    }


    /////////////////////////////////////////////////////
    // Hide
    //
    // Hide the ajax loader.  If there are multiple ajax
    // calls going on right now, dont hide the spinner
    // until all calls are done.
    /////////////////////////////////////////////////////
    function publicHide() {
        publicInit();

        if (counter > 0)
            counter--;

        if (counter == 0) {
            hide();
        }
    }


    /////////////////////////////////////////////////////
    // Set Message
    //
    // Set the message that is displayed when showing the
    // spinner
    /////////////////////////////////////////////////////
    function publicSetMessage(message) {
        _message = message;
        $('#' + messageId).html(message);
        repositionElements();
    }

    /////////////////////////////////////////////////////
    // Disable
    //
    // Any files that include this script will automatically
    // show the spinner when an ajax call is initiated.
    // This method will disable the spinner so it will
    // not show on any ajax calls.
    /////////////////////////////////////////////////////
    function publicDisable() {
        getSpinnerContainer().remove();
        getOverlay().remove();

        settings.enabled = false;
    }

    /////////////////////////////////////////////////////
    // Create HTML
    //
    // This method will create the HTML required to display
    // the spinner.
    /////////////////////////////////////////////////////
    function createHTML(message) {
        if (!message)
            message = 'Loading...';

        var html = '';
        html += '<div id="' + overlayId + '"></div>';
        html += '<div id="' + elemId + '">';
        html += '    <span id="' + messageId + '">' + message + '</span>';

        // If the current browser supports css animations,
        // use css animations to show the spinner so we don't
        // have to load an image.
        //
        // If this is an older browser that doesn't support
        // css animations, show an image instead.
        if (Modernizr.cssanimations) {
            html += '    <span id="' + cssSpinnerId + '"></span>';
        } else {
            html += '    <img id="' + imgSpinnerId + '" />';
        }

        html += '</div>';

        $('body').prepend(html);
    }

    /////////////////////////////////////////////////////
    // Register Ajax Events
    //
    // Register listeners for ajax events.
    //
    // The main ones are ajaxStart and ajaxStop
    /////////////////////////////////////////////////////
    function registerAjaxEvents() {
        var $document = $(document);

        // If the delay variable was set to undefined or a negative number,
        // set it to a default value of _defaultDelay (a little over a second)
        if (!_delay || _delay < 1)
            _delay = _defaultDelay;

        // Ajax start fires when the first ajax call is made
        $document.ajaxStart(function () {
            _delayTimeout = setTimeout(function () {
                if (settings.enabled) {
                    publicShow();
                }
            }, _delay);
        });

        // Ajax stop is fired when all current ajax calls 
        // are done
        $document.ajaxStop(function (e) {
            if (_delayTimeout)
                clearTimeout(_delayTimeout);

            // If the caller didn't disable the spinner
            if (settings.enabled) {
                // Double check that there are no active ajax calls
                if ($.active == 0) {
                    counter = 0;
                    hide();
                }
            }
        });
    }

    /////////////////////////////////////////////////////
    // Register Window Events
    //
    // This method registers a listener for when the window
    // is resized.  The ajax loading message will be
    // repositioned so it will stay centered on the screen.
    /////////////////////////////////////////////////////
    function registerWindowEvents() {
        $(window).on('resize', function () {
            // Use a clear/set timeout patter so that the
            // repositionElements method isn't called 
            // way too many times.
            clearTimeout(resizeTimeout);

            resizeTimeout = setTimeout(function () {
                repositionElements();
            }, 100);
        });
    }

    /////////////////////////////////////////////////////
    // Is Showing
    //
    // This method checks if the spinner is currently 
    // showing.
    /////////////////////////////////////////////////////
    function isShowing() {
        var $spinner = getSpinnerContainer();

        if ($spinner.length == 0 || !$spinner.is(':visible'))
            return false;

        return true;
    }

    /////////////////////////////////////////////////////
    // Reposition Elements
    //
    // This method repositions the elements associated 
    // with the ajax loader so that everything stays
    // centered both vertically and horizontally no matter
    // what the screen size.
    /////////////////////////////////////////////////////
    function repositionElements() {
        var $message = $('#' + messageId),
            $spinner = $('#' + cssSpinnerId);

        // If the spinner element wasnt initialized - initialize it
        if ($spinner.length == 0)
            $spinner = $('#' + imgSpinnerId);

        // Get the height of the message text and the actual spinning element
        var messageHeight = $message.outerHeight(),
            spinnerHeight = $spinner.outerHeight();

        // Check which is taller, the text of the spinner
        var taller = messageHeight > spinnerHeight ? messageHeight : spinnerHeight;

        $elem = getSpinnerContainer();

        // Set the height of the spinner container to be 32 pixels taller
        // than the tallest element within it.
        $elem.css('height', taller + 32);
        var containerHeight = $elem.outerHeight();

        // Vertically center the message.
        $message.css({
            top: (containerHeight / 2) - (messageHeight / 2)
        });

        // Vertically center the spinner.
        $spinner.css({
            top: (containerHeight / 2) - (spinnerHeight / 2)
        });
    }

    /////////////////////////////////////////////////////
    // Show
    //
    // Show the spinner and the overlay
    /////////////////////////////////////////////////////
    function show() {
        getSpinnerContainer().show();
        getOverlay().show();

        repositionElements();
    }

    /////////////////////////////////////////////////////
    // Hide
    //
    // Hide the spinner and the overlay
    /////////////////////////////////////////////////////
    function hide() {
        getSpinnerContainer().hide();
        getOverlay().hide();
    }

    /////////////////////////////////////////////////////
    // Get Spinner Container
    //
    // Gets a jQuery reference to the spinner container
    // element.
    /////////////////////////////////////////////////////
    function getSpinnerContainer() {
        if (!$elem)
            $elem = $('#' + elemId);

        return $elem;
    }

    /////////////////////////////////////////////////////
    // Get Overlay
    //
    // Gets a jQuery reference to the overlay element.
    /////////////////////////////////////////////////////
    function getOverlay() {
        if (!$overlay)
            $overlay = $('#' + overlayId);

        return $overlay;
    }

    /////////////////////////////////////////////////////
    // Set Delay
    //
    // Sets the time to wait before displaying the spinner
    // when an AJAX request is made.
    /////////////////////////////////////////////////////
    function publicSetDelay(delay) {
        _delay = delay;
    }

    return {
        init: publicInit,
        show: publicShow,
        hide: publicHide,
        setMessage: publicSetMessage,
        disable: publicDisable,
        setDelay: publicSetDelay
    }
})(jQuery, document, window);

// Automatically initialize this component so that any page that
// contains this script will show the spinner when ajax calls are
// made.
$(document).ready(function () {
    Cat.UI.Spinner.init();
});