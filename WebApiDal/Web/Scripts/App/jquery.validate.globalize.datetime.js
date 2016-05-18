/*!
** An extension to the jQuery Validation Plugin which makes it use Globalize.js for number and date parsing
** Copyright (c) 2016 Andres Käver, based on work by John Reilly
*/

(function ($, Globalize) {

    // Clone original methods we want to call into
    var originalMethods = {
        min: $.validator.methods.min,
        max: $.validator.methods.max,
        range: $.validator.methods.range
    };

    // Globalize options
    // Users can customise this to suit them
    // https://github.com/jquery/globalize/blob/master/doc/api/date/date-formatter.md
    $.validator.methods.dateGlobalizeOptions = { dateParseFormat: [{ skeleton: "yMd" }, { skeleton: "yMMMd" }, { date: "short" }, { date: "medium" }, { date: "long" }, { date: "full" }] };
    $.validator.methods.timeGlobalizeOptions = { dateParseFormat: [{ skeleton: "Hm" }, { skeleton: "hm" }, { time: "short" }, { time: "medium" }, { time: "long" }, { time: "full" }] };
    $.validator.methods.datetimeGlobalizeOptions = { dateParseFormat: [{ skeleton: "yMdHm" }, { skeleton: "yMdhm" }, { datetime: "short" }, { datetime: "medium" }, { datetime: "long" }, { datetime: "full" }, {raw: "d.M.y H:m"}] };


    // Tell the validator that we want dates parsed using Globalize
    $.validator.methods.date = function (value, element) {
        // is it optional
        if (this.optional(element) === true) return true;

        // remove spaces just in case
        value = value.trim();
        var res = false;
        var val;

        for (var i = 0; i < $.validator.methods.dateGlobalizeOptions.dateParseFormat.length; i++) {
            val = Globalize.parseDate(value, $.validator.methods.dateGlobalizeOptions.dateParseFormat[i]);
            console.log($.validator.methods.dateGlobalizeOptions.dateParseFormat[i], val, Globalize.dateFormatter($.validator.methods.dateGlobalizeOptions.dateParseFormat[i])(new Date(2016, 1, 1, 0, 0, 0)));
            res = res || (val instanceof Date);
            if (res === true) return res;
        }
        return res;
    };

    // additional method
    $.validator.methods.time = function (value, element) {
        // is it optional
        if (this.optional(element) === true) return true;

        // remove spaces just in case
        value = value.trim();
        var res = false;
        var val;

        for (var i = 0; i < $.validator.methods.timeGlobalizeOptions.dateParseFormat.length; i++) {
            val = Globalize.parseDate(value, $.validator.methods.timeGlobalizeOptions.dateParseFormat[i]);
            console.log($.validator.methods.timeGlobalizeOptions.dateParseFormat[i], val, Globalize.dateFormatter($.validator.methods.timeGlobalizeOptions.dateParseFormat[i])(new Date(2016, 1, 1, 0, 0, 0)));
            res = res || (val instanceof Date);
            if (res === true) return res;
        }
        return res;
    };

    // additional method
    $.validator.methods.datetime = function (value, element) {
        // is it optional
        if (this.optional(element) === true) return true;

        // remove spaces just in case
        value = value.trim();
        var res = false;
        var val;

        for (var i = 0; i < $.validator.methods.datetimeGlobalizeOptions.dateParseFormat.length; i++) {
            val = Globalize.parseDate(value, $.validator.methods.datetimeGlobalizeOptions.dateParseFormat[i]);
            console.log($.validator.methods.datetimeGlobalizeOptions.dateParseFormat[i], val, Globalize.dateFormatter($.validator.methods.datetimeGlobalizeOptions.dateParseFormat[i])(new Date(2016, 1, 1, 1, 1, 1)));
            res = res || (val instanceof Date);
            if (res === true) return res;
        }
        return res;
    };

    // Tell the validator that we want numbers parsed using Globalize
    $.validator.methods.number = function (value, element) {
        var val = Globalize.parseNumber(value);
        return this.optional(element) || ($.isNumeric(val));
    };

    // Tell the validator that we want numbers parsed using Globalize,
    // then call into original implementation with parsed value

    $.validator.methods.min = function (value, element, param) {
        var val = Globalize.parseNumber(value);
        return originalMethods.min.call(this, val, element, param);
    };

    $.validator.methods.max = function (value, element, param) {
        var val = Globalize.parseNumber(value);
        return originalMethods.max.call(this, val, element, param);
    };

    $.validator.methods.range = function (value, element, param) {
        var val = Globalize.parseNumber(value);
        return originalMethods.range.call(this, val, element, param);
    };

    //create adapters for new type - so they will be attached automatically
    //this depends on attribute data-val-time, data-val-datetime
    //asp.net 5 needs some js help
    $.validator.unobtrusive.adapters.addBool('time');
    $.validator.unobtrusive.adapters.addBool('datetime');

}(jQuery, Globalize));
