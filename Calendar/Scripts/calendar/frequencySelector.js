$(function () {
    $("#repeating").change(function () {
        var isHidden = $("#frequencySelector").is(":hidden");
        if (isHidden) {
            $("#frequencySelector").show();
        }
        else {
            $("#frequencySelector").hide();
        }
    });
});