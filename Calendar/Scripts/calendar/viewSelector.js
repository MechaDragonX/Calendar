$(function () {
    $("#selector").change(function () {
        var selected;
        selected = $(this).val(); // Selected value will be in this format: <Selection Label>:<ViewBag.Year>:<ViewBag.Month>
        var splitSelection = selected.split(":");
        if (selected == "Month") {
            window.location = "/Calendar/" + splitSelection[0] + "/?year=" + splitSelection[1] + "&month=" + splitSelection[2];
        }
        else {
            window.location = "/Calendar/" + splitSelection[0] + "/?year=" + splitSelection[1] + "&month=" + splitSelection[2] + "&day=" + "1";
        }
    });
});