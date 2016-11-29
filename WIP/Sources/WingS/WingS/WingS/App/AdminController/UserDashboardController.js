app.controller("UserDashboardController", function ($scope, $http, $sce) {
    //Load user static information for user circle tiles
    $http.get("/api/AdminUserDashboard/GetStatisticManageBasicInfor").success(function (response) {
        $scope.userManageInfor = response.Data;
        
    });
    // Load new created user
    $http.get("/api/AdminUserDashboard/GetNewCreatedUser").success(function (response) {
        $scope.newCreatedUser = response.Data;

    });

    //Load  information for donation table
    $http.get("/api/AdminUserDashboard/GetTopMostDonatedUser").success(function (response) {
        $scope.TopFiveDonator = response.Data;

    });

    // Load information of user who created most thread
    $http.get("/api/AdminUserDashboard/GetTopUserCreateThread").success(function (response) {
        $scope.TopFiveUserCreateThread = response.Data;

    });

    // Load information of user who in top 5 rank
    $http({
        url: "/api/AdminUserDashboard/GetTopRankUser",
        method: "GET",
        params: { top: 5 },
        contentType: "application/json"
    }).success(function (response) {
        $scope.TopFiveRankingUser = response.Data;
    });
});