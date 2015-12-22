function create() {

    $.getJSON('../api/alpaca').done(function (data) {
        drawForm(data);
    });

}

function createById(id) {

    $.getJSON('../api/alpaca/' + encodeURIComponent(id.toString())).done(function (data) {
        drawForm(data);
    });

}


function drawForm(data) {
    $('#form1').show();
    $("#form1").alpaca({
        "schema": data,
        "options": data,
        "view": data,
        "postRender": function (renderedForm) {         
            $('#btnSubmit').click(function () {

                var val = renderedForm.getValue();
                val.user = '0026607';

                $.ajax({
                    type: "POST",
                    url: "../api/FormPost",
                    data: { json: JSON.stringify(val) },
                    success: function (msg) {
                        alert("Request received: " + msg);
                    }
                });//post me

            });//btnSubmit
        }
    });
}

