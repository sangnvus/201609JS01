app.controller("UserListController", function ($scope, $http, $sce) {
    $http.get("/api/UserList/GetAllUser").success(function (response) {
        $scope.UserList = response.Data;
    });
    
});
