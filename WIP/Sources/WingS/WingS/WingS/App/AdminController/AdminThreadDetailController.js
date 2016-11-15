app.controller("AdminThreadDetailController", function ($scope, $http, $routeParams) {
    var threadID = $routeParams.ThreadID;

    //Get Thread Detail
    $http({
        url: "/api/AdminThread/GetThreadWithId",
        method: "GET",
        params: { ThreadID: threadID },
        contentType: "application/json"
    }).success(function (response) {
        $scope.currentThread = response.Data;
    });
});