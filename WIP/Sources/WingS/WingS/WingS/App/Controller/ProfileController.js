app.controller("ProfileController", function ($scope, $http, $routeParams) {
    var UserName = $routeParams.UserName;
    $http({
        url: "/api/User/GetCurrentUser",
        method: "GET",
        params: { userName: UserName },
        contentType: "application/json",
    }).success(function (response) {
        $scope.UserInfo = response.Data;
        
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
});