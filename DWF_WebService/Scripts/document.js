function create() {

    $.getJSON('api/alpaca').done(function (data) {
        drawForm(data);
    });

}

function drawForm(data) {
    $("#form1").alpaca({
        "schema": data
    });
}
