var currentCultureCode; //global variable, created in _Layout.cshtml - et, en, etc

$.when(
    $.get("/bower_components/cldr-data/supplemental/likelySubtags.json"),
    $.get("/bower_components/cldr-data/main/" + currentCultureCode + "/numbers.json"),
    $.get("/bower_components/cldr-data/supplemental/numberingSystems.json"),
    $.get("/bower_components/cldr-data/main/" + currentCultureCode + "/ca-gregorian.json"),
    $.get("/bower_components/cldr-data/main/" + currentCultureCode + "/ca-generic.json"),
    $.get("/bower_components/cldr-data/main/" + currentCultureCode + "/timeZoneNames.json"),
    $.get("/bower_components/cldr-data/supplemental/timeData.json"),
    $.get("/bower_components/cldr-data/supplemental/weekData.json")
).then(function () {
    // Normalize $.get results, we only need the JSON, not the request statuses.
    return [].slice.apply(arguments, [0]).map(function (result) {
        return result[0];
    });
}).then(Globalize.load).then(function () {
    // Initialise Globalize to the current UI culture
    Globalize.locale(currentCultureCode);
    moment.locale(currentCultureCode);
    //moment.localeData("et")._longDateFormat.LT = "HH:mm";
});

$(function () {
    // attach bootstrap datetimepicker spinner
    $('input[data-val-datetime]').datetimepicker({ locale: currentCultureCode, format: 'L LT' });
    $('input[data-val-date]').datetimepicker({ locale: currentCultureCode, format: 'L' });
    $('input[data-val-time]').datetimepicker({ locale: currentCultureCode, format: 'LT' });

    //add placeholders, use moment.js formats - since datetimepicker uses it
    $('input[data-val-datetime]').attr('placeholder',
        moment.localeData(currentCultureCode)._longDateFormat.L + " " +
        moment.localeData(currentCultureCode)._longDateFormat.LT);
    $('input[data-val-date]').attr('placeholder', moment.localeData(currentCultureCode)._longDateFormat.L);
    $('input[data-val-time]').attr('placeholder', moment.localeData(currentCultureCode)._longDateFormat.LT);
});
