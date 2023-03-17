$.ajaxSetup();

function getFilteredPumpstationObjectIds() {
    $.when(
        $.get(""),
        $.get(""),
        $.get("")
    ).done(function (response1, response2, response3) {



    });
}