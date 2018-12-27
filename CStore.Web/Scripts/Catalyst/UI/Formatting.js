/////////////////
//setup namespace
/////////////////
namespace("Cat.UI");

/////////////////////////////////////////////////////
//
//Formatting Methods
//
/////////////////////////////////////////////////////
Cat.UI.Formatting = (function ($) {
    /////////////////////////////////////////////////////
    //
    //formatDate
    //
    //Use Moment.js to take a date string, which is in the format of "2015-07-10T16:07:54.243",
    //and convert it to the displayed Date/Time value
    //If formatMask is not specified, then the date is converted to MM/DD/YYYY format.
    //
    //See documentation for moment.js for appropriate format masks.
    //http://momentjs.com/docs/#/displaying/format/
    //
    //Common ones are:
    //MM/DD/YYYY
    //MM/DD/YYYY HH:mm
    //
    /////////////////////////////////////////////////////
    var formatDate = function (value, formatMask) {
        if (value == null) {
            return null;
        }
        var dt = new Date(value);
        if (formatMask != null) {
            return moment(value).format(formatMask);
        } else {
            return moment(value).format('MM/DD/YYYY');
        }
    };

    /////////////////////////////////////////////////////
    //
    //dotNetJsonDateToFormattedString
    //
    //Use Moment.js to take a .NET serialized date, which is in the format of /Date(1436112169790)/,
    //and convert it to the displayed Date/Time value
    //If formatMask is not specified, then the date is converted to MM/DD/YYYY format.
    //
    //See documentation for moment.js for appropriate format masks.
    //http://momentjs.com/docs/#/displaying/format/
    //
    //Common ones are:
    //MM/DD/YYYY
    //MM/DD/YYYY HH:mm
    //
    /////////////////////////////////////////////////////
    var dotNetJsonDateToFormattedString = function (value, formatMask) {
        if (value == null) {
            return null;
        }

        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        if (results == null || results[1] == null) {
            return null;
        }
        var dt = new Date(parseFloat(results[1]));
        if (formatMask != null) {
            return moment(value).format(formatMask);
        } else {
            return moment(value).format('MM/DD/YYYY');
        }
    };

    /////////////////////////////////////////////////////
    //
    //formatBooleanAsYesNo
    //Takes a boolean and if true returns a Yes, and if false returns a No
    //
    /////////////////////////////////////////////////////
    var formatBooleanAsYesNo = function (value) {
        if (value == null) {
            return null;
        }
        var value = new Boolean(value);
        if (value == true) {
            return "Yes";
        }
        else {
            return "No";
        }
    };

    /////////////////////////////////////////////////////
    //
    //formatNumeric
    //Takes a number and a format type.  Formats accordingly
    //
    //Common ones are:
    //C, C2 -   Currency
    //F, F2 -   Fixed Point
    //N, N2 -   Number
    //PHONE -   Phone number
    //SSN   -   Social Security Number
    //
    /////////////////////////////////////////////////////
    var formatNumeric = function (value, format, ext) {
        if (value == null) {
            return null;
        }
        if (format == null) {
            return value;
        }

        var numVal = new Number(value);
        var hasExt = new Boolean(ext);

        switch (format) {
            case "C":
                return '$' + addCommas(value);
                break;
            case "C2":
                return '$' + addCommas(numVal.toFixed(2));
                break;
            case "F":
                return numVal.toFixed();
                break;
            case "F2":
                return numVal.toFixed(2);
                break;
            case "N":
                return addCommas(value);
                break;
            case "N2":
                return addCommas(numVal.toFixed(2));
                break;
            case "PHONE":
                if (value.length != 10 && !hasExt) {
                    return value;
                }
                if (hasExt) {
                    return "(" + value.substr(0, 3) + ") " + value.substr(3, 3) + "-" + value.substr(6, 4) + " " + value.substr(10);
                }
                return "(" + value.substr(0, 3) + ") " + value.substr(3, 3) + "-" + value.substr(6, 4);
                break;
            case "SSN":
                if (value.length != 9)
                    return value;
                return value.substr(0, 3) + "-" + value.substr(3, 2) + "-" + value.substr(5, 4);
                break;
            default:
                return value;
                break;
        }
    };

    ///
    ///Add commas to a number
    ///
    function addCommas(x) {
        var parts = x.toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return parts.join(".");
    }

    /////////////////////////////////////////////////////
    //
    //Return the methods to be exposed using the revealing module pattern
    //
    /////////////////////////////////////////////////////
    return {
        dotNetJsonDateToFormattedString: dotNetJsonDateToFormattedString,
        formatDate: formatDate,
        formatBooleanAsYesNo: formatBooleanAsYesNo,
        formatNumeric: formatNumeric
    };
})(jQuery);
