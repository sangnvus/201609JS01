app.controller("UserListController", function ($scope, $http) {
    $http.get("/api/AdminUserList/GetAllUser").success(function (response) {
        $scope.UserList = response.Data;
    });
    $scope.DoChangeStatus = function (index, userid) {
        $http({
            url: "/api/AdminUserList/ChangeStatusUser",
            method: "GET",
            params: { userid: userid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.UserList[index].IsActive = response.Data;
        });
        return true;
    };
});
