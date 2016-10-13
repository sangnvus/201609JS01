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

});