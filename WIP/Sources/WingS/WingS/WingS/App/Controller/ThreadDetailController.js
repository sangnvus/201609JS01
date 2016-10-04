app.controller("ThreadDetailController", function ($scope, $http, $routeParams, $sce) {
    var threadId = $routeParams.Id;
    $http({
        url: "/api/Thread/GetThreadById",
        method: "GET",
        params: { id: threadId },
        contentType: "application/json",
    }).success(function(response) {
        $scope.Thread = response.Data;
        $scope.imgUrl = $scope.Thread.ImageUrl;
    });

    $scope.trustSrc = function (src) {
        return $sce.trustAsResourceUrl(src);
    }
});