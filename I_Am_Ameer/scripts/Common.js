//$(document).ready(function () {

//    $(document).on('click', '#submit_form', function () {
//        debugger;
//        var tomail = $('#email').val();
//        var body = $('#body_email').val();
//        var subject = $('#subject').val();
//        $.ajax({
//            type: "POST",
//            url: "/Home/Email",
//            data: JSON.stringify({ tomail: tomail, body: body, subject: subject }),
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (data) {
//                debugger;
//            },
//            error: function (XMLHttpRequest, textStatus, errorThrown) {
//                location.reload();
//            }
//        });
//    });
//});
function Submit_click() {
    $('#f1').parsley().validate();
    if ($('#f1').parsley().isValid()) {
        $("#mes_email").text("Thank you");
        var tomail = $('#email').val();
        var body = $('#body_email').val();
        var subject = $('#subject').val();
        $.ajax({
            type: "POST",
            url: "/Home/Email",
            data: JSON.stringify({ tomail: tomail, body: body, subject: subject }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                debugger;
            },
            //error: function (XMLHttpRequest, textStatus, errorThrown) {
            //    location.reload();
            //}
        });
    }
    else {
        return false;
    }

}