app.controller("AdminDashboardController", function ($scope, $http) {
    //Get information for circle tiles
    $http.get("/api/AdminUserDashboard/GetStatisticManageBasicInfor").success(function (response) {
        $scope.dashboardManageInfor = response.Data;

    });

    // Load information of user who in top 5 rank
    $http.get("/api/AdminUserDashboard/GetTopFiveRankUser").success(function (response) {
        $scope.TopFiveRankingUser = response.Data;

    });

    // Load information of organization which in top 5 rank
   
    $http({
        url: "/api/Organization/GetTopOrganization",
        method: "GET",
        params: { top: 5 },
        contentType: "application/json"
    }).success(function(response) {
        $scope.TopFiveRankingOrg = response.Data;
    });

});