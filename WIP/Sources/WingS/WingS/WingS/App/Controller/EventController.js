app.controller("EventController", function ($scope, $http) {
    //Load top 1 event to Event page
    $http.get("/api/Event/Top1View").success(function (response) {
        $scope.Top1Event = response.Data;
    });
    //Load Event follow create date
    $http.get("/api/Event/NewestEvent").success(function (response) {
        $scope.Event = response.Data;
        var pageShow = 4;
        var index = 1;
        $scope.paginationLimit = function (data) {
            return pageShow * index;
        };
        $scope.hasMoreItemsToShow = function () {
            return pageShow < ($scope.Event.length / index);
        };
        $scope.showMoreItems = function () {
            index = index + 1;
        };

    });
    //Load 3 organization to Event page
    $http.get("/api/Organization/GetTopThreeOrganization").success(function (response) {
        $scope.Organization = response.Data;
    });

});