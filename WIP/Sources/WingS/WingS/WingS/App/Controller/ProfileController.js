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
        $scope.DOB = $scope.UserInfo.DOB;
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
        $scope.UserInfo.FullName = $scope.FullName;
        $scope.UserInfo.Phone = $scope.Phone;
        $scope.UserInfo.Address = $scope.Address;
        $scope.UserInfo.Country = $scope.Country;
        $scope.UserInfo.Gender = $scope.Gender;
        $scope.UserInfo.DOB = $scope.DOB;
        $scope.UserInfo.FacebookUri = $scope.FacebookUri;
        $scope.UserInfo.UserSignature = $scope.UserSignature;
        $http({
            url: "/api/User/UpdateUserInfo",
            method: "put",
            data: $scope.UserInfo ,
            contentType: "application/json",
        }).success(function (response) {
            
        });
        //Put new data to API

        $("#saveUser").css("visibility", "hidden");
        $(".form-control").prop("disabled", true);
        Lobibox.notify('success', {
            size: 'mini',
            rounded: true,
            position: 'center bottom',
            delayIndicator: false,
            msg: "Đã cập nhật thông tin thành công"
        });
    }
});