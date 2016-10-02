app.controller("DiscussionController", function ($scope, $http) {
    //Load 8 Thread mới nhất to Discussion page
    $http.get("/api/Thread1/GetEightNewestThread").success(function (response) {
        $scope.Thread = response.Data;
    });
});
