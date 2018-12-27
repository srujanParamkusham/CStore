/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////////////////////////////////////////
//
//Toast Message Display and Management Helper Methods
//This library contains helpers for displaying toast messages in the system.
//
/////////////////////////////////////////////////////
Cat.UI.ToastMessage = (function ($) {
    var showAllToastMessagesUsingAjax = function () {
        //Make an ajax call to get all the toast messages and display them.
        var url = "/ToastMessage/Messages";
        $.ajax({
            url: url,
            data: null,
            cache: false,
            success: function (data, status, xhr) {
                if (!data || data.length <= 0) {
                    return;
                }
                //Loop through each toast message returned and display it.
                for (var i = 0; i < data.length; i++) {
                    var toastMessage = data[i];
                    //console.log(toastMessage);
                    Cat.UI.ToastMessage.show(toastMessage);
                }
            },
            error: function (xhr, status, errorThrown) {
                alert("Error checking for new Toast Messages. " + xhr.status + " " + xhr.statusText);
            },
            complete: function (xhr, status) {
            }
        });          
    };
    
    /////////////////////////////////////////////////////
    //
    //show
    //Shows a specific toast message. Must be a JSON object. For example:
    //{"Title":"Record Saved","Message":"The record has been successfully saved.","AutoHide":true,"CloseButton":false,"Type":"Success","Position":"TopCenter"}
    //
    /////////////////////////////////////////////////////
    var show = function (toastMessage) {
        if (!toastMessage) {
            return;
        }
        
        toastr.options = {
            closeButton: toastMessage.CloseButton,
            debug: false,
            progressBar: false,
            positionClass: getPositionClass(toastMessage.Position),
            onclick: null
        };

        if (toastMessage.AutoHide) {
            toastr.options.timeOut = 3000;
            toastr.options.extendedTimeOut = 3000;
        } else {
            toastr.options.timeOut = 0;
            toastr.options.extendedTimeOut = 0;
        }

        var shortCutFunction = toastMessage.Type.toLowerCase();
        toastr[shortCutFunction](toastMessage.Message, toastMessage.Title);
    };
    
    /////////////////////////////////////////////////////
    //
    //getPositionClass
    //Takes a toast message position and returns the correct CSS class to use to
    //display the message.
    //
    /////////////////////////////////////////////////////
    var getPositionClass = function (position) {
        switch (position) {
            case "TopRight":
                return "toast-top-right";
            case "BottomRight":
                return "toast-bottom-right";
            case "BottomLeft":
                return "toast-bottom-left";
            case "TopLeft":
                return "toast-top-left";
            case "TopFullWidth":
                return "toast-top-full-width";
            case "BottomFullWidth":
                return "toast-bottom-full-width";
            case "TopCenter":
                return "toast-top-center";
            case "BottomCenter":
                return "toast-bottom-center";
        }

        return "toast-top-right";
    };

    
    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        showAllToastMessagesUsingAjax: showAllToastMessagesUsingAjax,
        show: show,
        getPositionClass: getPositionClass
    };
})(jQuery);
