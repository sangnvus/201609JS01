app.controller("UserDashboardController", function ($scope, $http, $sce) {
    //Load user static information
    $http.get("/api/AdminUserDashboard/GetUserManageBasicInfor").success(function (response) {
        $scope.userManageInfor = response.Data;
        
    });
    // Load new created user
    $http.get("/api/AdminUserDashboard/GetNewCreatedUser").success(function (response) {
        $scope.newCreatedUser = response.Data;

    });

    //Load  information for donation table
    $http.get("/api/AdminUserDashboard/GetTopTenMostDonatedUser").success(function (response) {
        $scope.TopFiveDonator = response.Data;

    });

});