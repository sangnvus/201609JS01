app.controller("EventDetailController", function ($scope, $http, $routeParams, $sce, $location) {
    var eventId = $routeParams.Id;
    //Load event Detail
    $http({
        url: "/api/Event/GetEventDetailById",
        method: "Get",
        params: { id: eventId },
        contentType: "application/json",
    }).success(function (response) {
        $scope.Event = response.Data;
        $scope.Event.VideoUrl = "http://www.youtube.com/embed/" + getYoutubeId($scope.Event.VideoUrl);
        $scope.Event.ExpectedMoney = $scope.Event.ExpectedMoney.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        $scope.Event.VideoUrl = $sce.trustAsResourceUrl($scope.Event.VideoUrl);
        $scope.Event.Content = $sce.trustAsHtml($scope.Event.Content);
    });
    function getYoutubeId(url)
    {
        var regExp = /^.*(youtu\.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
        var match = url.match(regExp);
        if (match && match[2].length == 11) {
            return match[2];
        } else {
            return "";
        }
    }
});