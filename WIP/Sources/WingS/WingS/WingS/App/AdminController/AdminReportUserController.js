app.controller("AdminReportUserController", function ($scope, $http, $sce) {
    // Load user report data
    $http.get("/api/Report/GetUserReportData").success(function (response) {
        $scope.reportStatistic = response.Data;
    });
});