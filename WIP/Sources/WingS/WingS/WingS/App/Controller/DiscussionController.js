﻿app.controller("DiscussionController", function ($scope, $http) {
    //Load 8 Thread mới nhất to Discussion page
    $http.get("/api/Thread/NewestThread").success(function (response) {
        $scope.Thread = response.Data;
        var pageShow = 4;
        var index = 2;
        $scope.paginationLimit = function (data) {
            return pageShow * index;
        };
        $scope.hasMoreItemsToShow = function () {
            return pageShow < ($scope.Thread.length / index);
        };
        $scope.showMoreItems = function () {
            index = index + 1;
        };

    });
});
