$(function () {
    $('#VerifyBtn').click(function () {
        $.getJSON("/Login/CheckVerifyCode", { UserName: $("#UserName").val(), VerifyCode: $("#VerifyCode").val() }, function (data) {
            if (data == "NotExistUser") {
                $('#ErrorNotExistUser').html("Tên đăng nhập không đúng");
                $('#ErrorCode').html("");
                return false;
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
                $('#VerifyForm').submit();
                return true;
            }
        });
    });
});
