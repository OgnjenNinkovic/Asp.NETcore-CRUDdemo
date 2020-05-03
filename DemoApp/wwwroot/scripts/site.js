$(document).ready(function () {
    $('main').html();
    $("#SearchBtn").click(function () {
        var SearchValue = $("#EmpSearch").val();
        var SetData = $("#DataSearching");

        SetData.html("");
        $.ajax({
            type: "post",
            url: "/Employee/Index?SearchValue=" + SearchValue,
            contentType: "html",
            success: function (response) {
                $('main').html(response);
            }

        });
    });

});