function create() {

    $.getJSON('api/alpaca').done(function (data) {
        drawForm(data);
    });

}

function createById(id) {

    $.getJSON('api/alpaca/' + encodeURIComponent(id.toString())).done(function (data) {
        drawForm(data);
    });

}


function drawForm(data) {
    $('#form1').show();
    $("#form1").alpaca({
        "schema": data,
        "options": data,
        "view": data
    });
}

