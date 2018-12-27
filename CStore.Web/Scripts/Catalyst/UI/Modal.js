/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////////////////////////////////////////
//
//Modal Window Rendering/Helper Methods
//This library relies on the base bootstrap modal window functionality.
//
/////////////////////////////////////////////////////
Cat.UI.Modal = (function ($) {

    //
    //Special array used to hold the data to pass to the callback function
    //On a save and exit, it'll be the JSON result from the form post.
    //On any other operation it'll be the serialized data in the first form on the modal window
    //
    var callbackDataArray = new Array();

    /////////////////////////////////////////////////////
    //
    //initializeModalWindowSupport
    //This method is needed to initialize support for having links render 
    //their content as modal windows
    //
    /////////////////////////////////////////////////////
    var initializeModalWindowSupport = function () {
        //
        //attach modal-container bootstrap attributes to links with .modal-link class.
        //when a link is clicked with these attributes, bootstrap will display the href content in a modal dialog.
        //
        //Use $(body) with the selector in the on event to attach events to, in order to allow it to attach to dynamic content
        $('body').on('click', 'a.modal-link', function (e) {
            e.preventDefault();
            var callback = $(this).data('callback');
            var options = {
                urlToOpen: $(this).attr('href'),
                callback: callback
            };
            showModalWindow(options);
        });

        //
        //When a modal window is SHOWN
        //Assign the appropriate z-index to the new  modal window being displayed so that it appears on top of any other open modal windows
        //Original code for the stackable modal windows adapted from http://miles-by-motorcycle.com/static/bootstrap-modal/index.html
        //
        $('body').on('shown.bs.modal', '.modal', function (event) {
            if (typeof ($('body').data('fv_open_modals')) == 'undefined') {
                $('body').data('fv_open_modals', 0);
            }

            // if the z-index of this modal has been set, ignore.                        
            if ($(this).hasClass('fv-modal-stack')) {
                return;
            }

            $(this).addClass('fv-modal-stack');

            $('body').data('fv_open_modals', $('body').data('fv_open_modals') + 1);

            var newZIndex = 2050 + (10 * $('body').data('fv_open_modals'));
            //console.log("New Z Index:" + newZIndex);
            $(this).css('z-index', newZIndex);

            $('.modal-backdrop').not('.fv-modal-stack').css('z-index', 2049 + (10 * $('body').data('fv_open_modals')));

            $('.modal-backdrop').not('fv-modal-stack').addClass('fv-modal-stack');


            /* 
            TODO TEA 10/16/15 - get this working someday. It is support for the browser back button closing the modal

            //Browser back button handling for the modal            
            var urlLocationHash = "#" + $(this).attr("id"); // make the hash the id of the modal shown
            //This will push state values onto the window that is opening. The container ID of the modal window is
            //pushed onto the state, so in a scenario of multiple modal windows we can close them in the appropriate order.
            var stateObj = { modalContainerId: urlLocationHash };
            history.pushState(stateObj, null, urlLocationHash); // push state that hash into the url
            */
        });

        // If a pushstate has previously happened and the back button is clicked, hide any modals.
        /* 
        TODO TEA 10/16/15 - get this working someday. It is support for the browser back button closing the modal
        $(window).on('popstate', function (event) {
            //If the state is null, hide all .modal items
            //Otherwise, get the current state item, and hide all #modal_XXX with timestamps greater than the state value one.
            var currentState = event.originalEvent.state;
            
            console.log("Hiding modal container ");
            $(".modal").modal('hide');
        });
        */

        //
        //When a modal window is HIDDEN
        //Remove it from the DOM and call the optional callback
        //
        $('body').on('hidden.bs.modal', '.modal', function (event) {
            //console.log("hidden.bs.modal called");
            $(this).removeClass('fv-modal-stack');
            $('body').data('fv_open_modals', $('body').data('fv_open_modals') - 1);

            //Get the id of the modal container div
            var modalContainerId = $(this).attr("id");

            //
            //Store the ajax response so it can be passed to the callback function
            //
            if (callbackDataArray[modalContainerId] == null) {
                callbackDataArray[modalContainerId] = new Object();
            }
            //
            //Get all forms on the modal window and serialize them to add to the return data
            //
            callbackDataArray[modalContainerId].formData = new Array();
            $('#' + modalContainerId).find('form').each(function () {
                var formData = $(this).serializeArray();
                callbackDataArray[modalContainerId].formData.push(formData);
            });

            callbackDataArray[modalContainerId].modalWindowContent = $(this);

            //
            //In a stacked modal environment, closing the second modal windows, will not reapply 
            //the scrollbar back to the first modal window. This code does that.
            //
            $('.modal.fv-modal-stack').css('overflow-y', 'auto');

            //Browser back button handling for the modal
            /*            
                        var hash = this.id;
                        history.pushState('', document.title, window.location.pathname);
            */
            //Remove the modal content from the DOM
            $(this).remove();

            //
            //call the callback function if necessary
            //Be sure to do this after the modal window is removed from the DOM to avoid 
            //collisions on element IDs, where the parent form may have properties with the
            //same name as the child.
            //
            var callback = $(this).data('callback');
            if (callback && callback != "undefined") {
                //console.log("Calling callback: " + callback);

                var func = (typeof callback == 'function') ? callback : eval(callback);
                if (func) {
                    func(callbackDataArray[modalContainerId]);
                }
            }

            //
            //Remove the callback data from our array to conserve memory
            //
            delete callbackDataArray[modalContainerId];
        });
    };


    /////////////////////////////////////////////////////
    //
    //Show a modal window.
    //The options element has these properties:
    //options.urlToOpen - the href of the url whose content should be put in the modal window. This is required.
    //modalContainerId - the ID that will be assigned to the modal window div tag or the div id of an existing div to use. This is optional. If not specified, an ID will be generated.
    //
    /////////////////////////////////////////////////////
    var showModalWindow = function (options) {
        //
        //Ensure we have a valid options param for showing the window
        //
        if (!options) {
            console.log("Unable to show modal window. The options parameter is null.");
            return;
        }

        //
        //Ensure we have a url to load into the modal window
        //
        if (!options.urlToOpen) {
            console.log("Unable to show modal window. The options.urlToOpen was not specified.");
            return;
        }

        //
        //Ensure we have a container to put the modal content into.
        //If one doesnt already exist, then create it. 
        //
        if (!options.modalContainerId) {
            options.modalContainerId = Cat.generateUniqueElementId("modal");
        }

        createModalDivHtml(options);

        //
        //Setup the modal window and show it
        //
        var modalOptions = {
            //This can be either true or false. This defines whether or not you want the user to be able to close the modal by clicking the background.
            backdrop: true,
            //if set to true then the modal will close via the ESC key.
            keyboard: true,
            //Used for opening and closing the modal. It can be either true or false.
            show: true
            //Load remote content using jQuery’s load() method. You need to specify an external page in this option.
            //3/16/16 TEA, This has been deprecated as of bootstrap 3.3 and removed in v4.
            //Need to call the ajax method ourselves going forward.
            //remote: options.urlToOpen
        };

        //Call the ajax method ourselves and inject the modal content
        $.ajax({
            url: options.urlToOpen,
            data: null,
            cache: false,
            success: function (data, status, xhr) {
                //console.log("Data returned is:");
                //console.log(data);
                var modal = $('#' + options.modalContainerId).modal(modalOptions);
                modal.find('.modal-content').html(data);
                modal.show();
                Cat.UI.Modal.onModalWindowLoaded(options.modalContainerId);
            },
            error: function (xhr, status, errorThrown) {
                alert("Error getting content for modal window. " + xhr.status + " " + xhr.statusText);
            },
            complete: function (xhr, status) {
            }
        });
    };

    /////////////////////////////////////////////////////
    //
    // Create the div for the modal window to be injected into
    // This is a private method, not revealed below
    //
    /////////////////////////////////////////////////////
    function createModalDivHtml(options) {
        //If an element already exists with that ID, then dont recreate it.
        if ($("#" + options.modalContainerId).length) {
            return;
        }

        var html = '';
        //Warning, do not put tabindex="-1" in the element below. Doing so breaks search functionality in a select 2 drop down
        html += '<div id="' + options.modalContainerId + '" class="modal"  data-keyboard="true" role="dialog" data-callback="' + options.callback + '">';
        html += '    <div class="modal-content"></div>';
        html += '</div>';
        $('body').prepend(html);
    }

    /////////////////////////////////////////////////////
    //
    //onAjaxFormPostSuccess
    //Callback for when an ajax form post is done successfully
    //
    //Parameters:
    //data - The response from the form post, can be HTML or JSON. If its JSON, it must have an IsSuccessful property. If it was successful then 
    //       and has a SubmitAction of SaveAndExit or Cancel, then the modal window will be closed/hidden.
    //       If the data is not json, it is then assumed to be HTML. The method will look for an element that is closest to the form as a parent with
    //       a class of ajax-form-wrapper. That element will be replaced with whatever comes back in the data parameter.
    //form - A reference to the form object that was submitted.
    //
    /////////////////////////////////////////////////////
    var onAjaxFormPostSuccess = function (data, form) {
        //console.log("onFormPostSuccess");
        //console.log(data);
        //console.log(form);

        //Validation, ensure we have good parameters
        if (!data || !form) {
            console.log("The onAjaxFormPostSuccess handler could not successfully process the request. Either the data or form was null.");
            console.log(data);
            console.log(form);
            return;
        }

        //
        //Show any toast messages that may have occurred.
        //
        Cat.UI.ToastMessage.showAllToastMessagesUsingAjax();

        //
        //Get the parent form wrapping div
        //The form wrapper is the html that is replaced on ajax posts returning html content.
        //
        var ajaxFormWrapper = $(form).closest(".ajax-form-wrapper");
        if (!ajaxFormWrapper) {
            console.log("The onAjaxFormPostSuccess handler could not successfully process the request. There must be a parent div to the form with css class ajax-form-wrapper on it.");
        }
        var formWrapperId = "#" + ajaxFormWrapper.attr("id");

        //
        //And get the modal div wrapper
        //
        var modalContainer = $(ajaxFormWrapper).closest(".modal");
        var modalContainerId = modalContainer.attr("id");

        //
        //If the response came back in a JSON object, and was successful, then process it
        //
        if (data != null && data.IsSuccessful != null) {
            if (data.IsSuccessful) {
                //
                //Close the modal window if we got a successful json response
                //telling us to close the window
                //
                if (data.SubmitAction && (data.SubmitAction.toLowerCase() == "saveandexit" || data.SubmitAction.toLowerCase() == "cancel")) {
                    //
                    //Store the ajax response so it can be passed to the callback function
                    //
                    if (callbackDataArray[modalContainerId] == null) {
                        callbackDataArray[modalContainerId] = new Object();
                    }
                    callbackDataArray[modalContainerId].ajaxData = data;

                    if (data.IsModalDialog) {
                        modalContainer.modal("hide");
                    } else {
                        location.href = data.ReturnUrl;
                    }
                }
            } else {
                console.log("The onAjaxFormPostSuccess handler could not successfully process the request. Either the data or form was null.");
                Cat.UI.MsgBox.show("Error", data.Message);
            }
        }
            //
            //The ajax response didnt come back as valid JSON we understand, so we assume its
            //html and replace the form wrapper div with the contents
            //
        else {
            ajaxFormWrapper.replaceWith(data);
            //
            //Initialize form controls
            //
            if (Cat) {
                //console.log("Wiring up controls on modal window: " + modalContainerId);
                //Warning if the parent modal window was never given an ID.
                if (!modalContainerId || modalContainerId == "#") {
                    console.log("Warning, the modal window does not have an ID. Wiring up form controls may not work properly or may be slower than normal.");
                }
                Cat.wireUpFormControls("#" + modalContainerId);
            }
        }
    };

    /////////////////////////////////////////////////////
    //
    //onModalWindowLoaded
    //When a modal windows content is loaded, initialize all of the catalyst controls.
    //
    //Parameters:
    //modalContainerId - The html id of the modal window container div. Do not include the # sign in front of it.
    //
    /////////////////////////////////////////////////////
    var onModalWindowLoaded = function (modalContainerId) {
        //console.log("onModalWindowLoaded. Container ID: " + modalContainerId);

        var modalId = "#" + modalContainerId;
        //
        //Initialize form controls
        //
        if (Cat) {
            Cat.wireUpFormControls(modalId);
        }
    };

    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        initializeModalWindowSupport: initializeModalWindowSupport,
        showModalWindow: showModalWindow,
        onModalWindowLoaded: onModalWindowLoaded,
        onAjaxFormPostSuccess: onAjaxFormPostSuccess
    };

})(jQuery);
