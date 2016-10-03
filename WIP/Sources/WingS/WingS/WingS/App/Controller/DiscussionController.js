app.controller("DiscussionController", function ($scope, $http) {
    //Load 8 Thread mới nhất to Discussion page
    $http.get("/api/Thread/NewestThread").success(function (response) {
        $scope.Thread = response.Data;
    });
});
