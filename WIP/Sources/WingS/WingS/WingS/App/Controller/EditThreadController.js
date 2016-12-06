app.controller("EditThreadController", function ($scope, $http, $routeParams, $window,$sce) {
    var threadId = $routeParams.Id;
    //Load ThreadDetail
    $http({
        url: "/api/Thread/GetThreadById",
        method: "GET",
        params: { id: threadId },
        contentType: "application/json",
    }).success(function (response) {
        $scope.Thread = response.Data;
        $scope.Thread.Content = $sce.trustAsHtml($scope.Thread.Content);
        $scope.Title = $scope.Thread.ThreadName;
        $scope.ShortDescription = $scope.Thread.ShortDescription;
        $scope.ThreadId = $scope.Thread.ThreadID;
    });
    //Get ThreadDetail to update

});