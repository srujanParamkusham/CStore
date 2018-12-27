/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

//////////////////////////////
//message box or alert control
//////////////////////////////
Cat.UI.MsgBox = (function ($) {
    return {
        show: function (title, message, buttons, callback) {
            if (buttons == null) {
                buttons = {
                    "Ok": function () {
                        if (callback != null) {
                            callback();
                        }
                        $(this).dialog("close");
                    }
                };
            }
            var d = new Date();
            //n will be the number of milliseconds since 1970/01/01. i.e., 1444833965003
            var n = d.getTime();
            var id = "dialog_" + n;

            $("<div id=\"" + id + "\"></div>").html(message).dialog({
                title: title,
                resizable: false,
                modal: true,
                buttons: buttons
            });
        }
    };
})(jQuery);

//////////////////////////////
//message box with yes/no buttons
//////////////////////////////
Cat.UI.MsgBoxYesNo = (function (cat) {
    return {
        show: function (title, message, yesButtonFunc, noButtonFunc) {
            var buttons = {
                "Yes": function () {
                    if (yesButtonFunc != null) {
                        yesButtonFunc();
                    }
                    $(this).dialog("close");
                },
                "No": function () {
                    if (noButtonFunc != null) {
                        noButtonFunc();
                    }
                    $(this).dialog("close");
                }
            };
            cat.show(title, message, buttons);
        }
    };
})(Cat.UI.MsgBox);
