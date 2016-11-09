app.controller("UserListController", function ($scope, $http, $sce) {
    $http.get("/api/UserList/GetAllUser").success(function (response) {
        $scope.UserList = response.Data;
    });
    $scope.DoChangeStatus = function (userid) {
        $.ajax({
            url: "/api/UserList/ChangeStatusUser",
            type: "get",
            data: userid,
            timeout: 10000, // 10 seconds for getting result, otherwise error.
            error:function() { alert("Temporary error. Please try again...");},
            success: function(data) { alert('yo'); }
        });
    };
});
