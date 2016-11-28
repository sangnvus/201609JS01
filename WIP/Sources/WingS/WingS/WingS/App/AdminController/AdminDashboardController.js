app.controller("AdminDashboardController", function ($scope, $http) {
    //Get information for circle tiles
    $http.get("/api/AdminUserDashboard/GetStatisticManageBasicInfor").success(function (response) {
        $scope.dashboardManageInfor = response.Data;

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
    // Load information of organization which in top 5 rank
    $http({
        url: "/api/Organization/GetTopOrganization",
        method: "GET",
        params: { top: 5 },
        contentType: "application/json"
    }).success(function(response) {
        $scope.TopFiveRankingOrg = response.Data;
    });

    // Load information of most donated event which in top 5 rank
    $http({
        url: "/api/Event/GetTopEventSortByMoneyDonateIn",
        method: "GET",
        params: { top: 5 },
        contentType: "application/json"
    }).success(function (response) {
        $scope.TopFiveRankingRaisedMoneyEvent = response.Data;
    });

    // Load information of most liked thread which in top 5 rank
    $http({
        url: "/api/AdminThread/GetTopLikeThread",
        method: "GET",
        params: { top: 5 },
        contentType: "application/json"
    }).success(function (response) {
        $scope.TopFiveLikeThread = response.Data;
    });

    // Load information of most donated user which in top 5 rank
    $http({
        url: "/api/AdminThread/GetTopLikeThread",
        method: "GET",
        params: { top: 5 },
        contentType: "application/json"
    }).success(function (response) {
        $scope.TopFiveLikeThread = response.Data;
    });

    //Load  information for donation table
    $http.get("/api/AdminUserDashboard/GetTopMostDonatedUser").success(function (response) {
        $scope.TopFiveDonator = response.Data;

    });

    // load infor of donation recently
    $http({
        url: "/api/AdminUserDashboard/GetTopRecentDonator",
        method: "GET",
        params: { top: 5 },
        contentType: "application/json"
    }).success(function (response) {
        $scope.TopRecentDonator = response.Data;
    });
});