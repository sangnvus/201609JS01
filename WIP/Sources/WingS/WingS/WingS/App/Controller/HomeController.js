app.controller("HomeController", function ($scope,$http) {
    //Load top 4 event to Home page
    $http.get("/api/Event/GetTopFourViewedEvent").success(function (response) {
        $scope.Event = response.Data;
        //$scope.abc = "nghia";
    });

    //Load top 4 Thread to Home page
    $http.get("/api/Thread/GetTopFourThread").success(function (response) {
        $scope.Thread = response.Data;
    });

    //Load 3 organization to Home page
    $http.get("/api/Organization/GetTopThreeOrganization").success(function (response) {
        $scope.Organization = response.Data;
    });
});