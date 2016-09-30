$(function () {
    $('#registerBtn').click(function () {
        $.getJSON("/Login/CheckExistedUserNameOrEmail", { UserName: $("#UserName").val(), Email: $("#Email").val() }, function (data) {
            if (data == "ExistedUser") {
                $('#errorExistUser').html("Tên đăng nhập đã được sử dụng");
                $('#errorExistEmail').html("");
                $('#UserName').focus();
                return false;

            } else if (data == "ExistedEmail") {
                $('#errorExistEmail').html("Địa chỉ Email đã được sử dụng");
                $('#errorExistUser').html("");
                $('#Email').focus();
                return false;
            } else {
                $('#errorExistEmail').html("");
                $('#errorExistUser').html("Đang đăng ký...");
                $('#RegisterForm').submit();
                return true;
            }
        });
    });
});
//$(function () {
//        $('#registerBtn').click(function () {
//            if ($("#UserName").val() == "" || $("#PassWord").val() == "" || $("#RePassWord").val() == "" || $("#FullName").val() == "" || $("#Email").val() == "") {
//                $('#errorExistUser').html("Bạn phải nhập thông tin đầy đủ cho các mục");
//                return false;
//            } else {
//                Dosubmit();
//            }
//        });
//    });
//function Dosubmit() {
//    $.getJSON("/Login/CheckExistedUserNameOrEmail", { UserName: $("#UserName").val(), Email: $("#Email").val() }, function (data) {
//        if (data == "ExistedUser") {
//            $('#errorExistUser').html("Tên đăng nhập đã được sử dụng");
//            $('#UserName').focus();
//            return false;

//        } else if (data == "ExistedEmail") {
//            $('#errorExistEmail').html("Địa chỉ Email đã được sử dụng");
//            $('#Email').focus();
//            return false;
//        } else {
//            $('#errorExistUser').html("Đang đăng ký...");
//            $('#RegisterForm').submit();
//            return true;
//        }
//    });
//}
