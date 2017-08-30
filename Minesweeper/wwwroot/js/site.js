$("document").ready(function () {
    $('#diff').modal();
    $('body').removeClass();
});
$("button[name=ds]").click(function () {
    var options = {
        type: "GET",
        url: "/api/values/" + this.value + "?q=1",
        dataType: "json",
        success: function (response) {
            console.log("Success");
            CreateGrid(response["grid_length"], response["grid_width"], response["no_of_mines"]);
            console.log(response);
        },
        error: function (response) {
            console.log("Error");
        },
        faliure: function (response) {
            console.log("Faliure");
        }
    };
    //console.log(options.data);
    $.ajax(options);
});

function CreateGrid(l, b, m) {
    var d = $("<div>", { "class": "container" });
    for (var i = 0; i < b; i++) {
        var o = $("<div>", { "class": "row" });
        for (var j = 0; j < l; j++) {
            var y = $("<div>", {
                "class": "cell nopadding", "id": (i).toString() + "A" + j.toString(), "style":"width: 20px;"
            });
            y.click(function () {
                gonclick(this);
            });
            o.append(y);
        }
        d.append(o);
    }
    $("#game").append(d);
}

function gonclick(elm) {
    var options = {
        type: "GET",
        url: "/api/values/" + elm.id + "?q=2",
        dataType: "json",
        success: function (response) {
            console.log("Success");
            console.log(response);
            //opencell(response);
        },
        error: function (response) {
            console.log("Error");
        },
        faliure: function (response) {
            console.log("Faliure");
        }
    };
    $.ajax(options)
}
function opencell() {

}