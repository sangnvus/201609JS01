$(function () {
    $('#VerifyBtn').click(function () {
        $.getJSON("/Login/CheckVerifyCode", { UserName: $("#UserName").val(), VerifyCode: $("#VerifyCode").val() }, function (data) {
            if (data == "NotExistUser") {
                $('#ErrorNotExistUser').html("Tên đăng nhập không đúng");
                $('#ErrorCode').html("");
                return false;
            }
            else if (data == "IsVerify") {
                //$('#ErrorNotExistUser').html("Tài khoản của bạn đã được xác nhận rồi! Vui lòng Đăng nhập để sử dụng trang web.");
                //$('#ErrorCode').html("");
                //return false;
                alert("Tài khoản của bạn đã được xác nhận rồi! Vui lòng Đăng nhập để sử dụng trang web.");
                window.location.href = '/#/Login';
            }
            else if (data == "ErrorCode")
            {
                $('#ErrorNotExistUser').html("");
                $('#ErrorCode').html("Mã xác nhận không đúng");
                return false;
            }
            else if (data == "Exception") {
                $('#ErrorCode').html("");
                $('#ErrorNotExistUser').html("");
                $('#ErrorNotExistUser').html("Xảy ra lỗi Exception!");
                return false;
            }
            else {
                alert("Bạn đã đăng ký thành công, hãy đăng nhập!");
                $('#VerifyForm').submit().delay(1000);
                return true;
            }
        });
    });
});
