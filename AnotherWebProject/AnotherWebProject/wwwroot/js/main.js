$(document).ready(function () {
    var template = $('#template').html();
    Mustache.parse(template);

    refresh();

    $("form").submit(function (e) {
        e.preventDefault(e);

        var payload = {};

        payload["firstname"] = $("#firstname").val();
        payload["lastname"] = $("#lastname").val();

        $.ajax({
            type: "POST",
            url: "https://localhost:44309/api/entries",
            data: JSON.stringify(payload),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).then(function (data) {
            refresh();
        });
    });

    $("#refresh").click(function () {
        refresh();
    });

    $("body").on("click", ".delete", function () {
        $.ajax({
            type: "DELETE",
            url: "https://localhost:44309/api/entries/" + this.id
        }).then(function (data) {
            refresh();
        });
    });

    function refresh() {
        $.ajax({
            url: "https://localhost:44309/api/entries"
        }).then(function (data) {
            var rendered = Mustache.render(template, data);
            $('#target').html(rendered);
        });
    }
});

