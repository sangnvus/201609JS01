﻿app.controller("HomeController", function ($scope,$http) {
    $http.get("/api/Event/GetTopFourViewedEvent").success(function (response) {
        $scope.Event = response.Data;
        //$scope.abc = "nghia";
    });
});