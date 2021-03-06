﻿app.controller("StatisticsController", function ($scope, $http) {
    //load event donation information
    $http.get("/api/AdminEvent/GetAllEvent").success(function (response) {
        $scope.AllEventList = response.Data;
    });
    // Load information of user who in top 10 rank
    $http({
        url: "/api/AdminUserDashboard/GetTopRankUser",
        method: "GET",
        params: { top: 10 },
        contentType: "application/json"
    }).success(function (response) {
        $scope.TopTenRankingUser = response.Data;
    });
    // Load information of organization which in top 5 rank
    $http({
        url: "/api/Organization/GetTopOrganization",
        method: "GET",
        params: { top: 10 },
        contentType: "application/json"
    }).success(function (response) {
        $scope.TopTenRankingOrg = response.Data;
    });
});