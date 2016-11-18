app.controller("ProfileController", function ($scope, $http, $routeParams) {
    var UserName = $routeParams.UserName;
    $http({
        url: "/api/User/GetCurrentUser",
        method: "GET",
        params: { userName: UserName },
        contentType: "application/json",
    }).success(function (response) {
        $scope.UserInfo = response.Data;
        $scope.FullName = $scope.UserInfo.FullName;
        $scope.Phone = $scope.UserInfo.Phone;
        $scope.Address = $scope.UserInfo.Address;
        $scope.Country = $scope.UserInfo.Country;
        $scope.Gender = $scope.UserInfo.Gender;
        //Convert DOB to suitable value with date time picker
        var formatDay = ($scope.UserInfo.DOB).split("/");
        $scope.DOB = formatDay[2]+"-"+formatDay[1]+"-"+formatDay[0];
        //End convert
        $scope.FacebookUri = $scope.UserInfo.FacebookUri;
        $scope.UserSignature = $scope.UserInfo.UserSignature;
        
    });
    $scope.sendMessage = function (titleMessage, content) {
        $http({
            url: "/api/Conservation/AddConservation",
            method: "post",
            data: { Title: titleMessage, Content: content, ReceiverName: $scope.UserInfo.UserName },
            contentType: "application/json",
        }).success(function (response) {
            $(".modal-body").hide();
            $(".sendMessage").hide();
            $(".modal-message").show();
            $(".closeForm").show();
        });
    };
    $scope.enableButton = function()
    {
        $(".form-control").prop("disabled", false);
        $("#MoreInfo").prop("disabled", true);
        $("#saveUser").css("visibility", "visible");
    }
    $scope.saveUser = function()
    {
        $scope.newUserInfo = $scope.UserInfo;
        $scope.newUserInfo.FullName = $scope.FullName;
        $scope.newUserInfo.Phone = $scope.Phone;
        $scope.newUserInfo.Address = $scope.Address;
        $scope.newUserInfo.Country = $scope.Country;
        $scope.newUserInfo.Gender = $scope.Gender;
        $scope.newUserInfo.DOB = $("#datePicker").val();
        $scope.newUserInfo.FacebookUri = $scope.FacebookUri;
        $scope.newUserInfo.UserSignature = $scope.UserSignature;
        $http({
            url: "/api/User/UpdateUserInfo",
            method: "put",
            data: $scope.newUserInfo,
            contentType: "application/json",
        }).success(function (response) {
            if (response.Status == "success") {
            Lobibox.notify('success', {
                size: 'mini',
                rounded: true,
                position: 'center bottom',
                delayIndicator: false,
                msg: "Đã cập nhật thông tin thành công"
            });
            } else Lobibox.notify('error', {
                size: 'mini',
                rounded: true,
                position: 'center bottom',
                delayIndicator: false,
                msg: "Đã xảy ra lỗi khi cập nhật thông tin"
            });
        });

        //Put new data to API

        $("#saveUser").css("visibility", "hidden");
        $(".form-control").prop("disabled", true);
       
    }
});
