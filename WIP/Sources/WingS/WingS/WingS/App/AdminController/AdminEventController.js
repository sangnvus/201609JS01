app.controller("AdminEventController", function ($scope, $http,$sce, $routeParams) {
    //Load user static information
    $scope.parseFloat=function(val) {
    return isNaN(parseFloat(val)) ? 0: parseFloat(val);
    }
    $http.get("/api/AdminEvent/GetEventManageBasicInfor").success(function (response) {
        $scope.EventManageInfor = response.Data;
    });
    $http.get("/api/AdminEvent/GetTopNewEvent").success(function (response) {
        $scope.TopNewEvent = response.Data;
    });
    $http.get("/api/AdminEvent/GetTopDonatedEvent").success(function (response) {
        $scope.TopHotEvent = response.Data;
    });
    $http.get("/api/AdminEvent/GetAllEvent").success(function (response) {
        $scope.AllEventlist = response.Data;
    });
    function getYoutubeId(url) {
        var regExp = /^.*(youtu\.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
        var match = url.match(regExp);
        if (match && match[2].length == 11) {
            return match[2];
        } else {
            return "";
        }
    }
    var eventId = $routeParams.EventID;
    $http({
        url: "/api/AdminEvent/GetEventWithId",
        method: "GET",
        params: { eventId: eventId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.currentEvent = response.Data;
        $scope.currentEvent.VideoUrl = "http://www.youtube.com/embed/" + getYoutubeId($scope.currentEvent.VideoUrl);
        $scope.currentEvent.ExpectedMoney = $scope.currentEvent.ExpectedMoney.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        $scope.currentEvent.RaisedMoney = $scope.currentEvent.RaisedMoney.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        $scope.currentEvent.VideoUrl = $sce.trustAsResourceUrl($scope.currentEvent.VideoUrl);
        $scope.currentEvent.Content = $sce.trustAsHtml($scope.currentEvent.Content);
    });
    
});