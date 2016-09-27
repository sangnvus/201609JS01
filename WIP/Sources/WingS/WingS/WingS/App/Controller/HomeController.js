app.controller("HomeController", function ($scope,$http) {
    $http.get("/api/Event/GetTopViewEvent").success(function(response) {
            var getTopViewEvent = response.Data;
            $scope.eventName = getTopViewEvent.EventName;
            $scope.eventContent = getTopViewEvent.Content;
            $scope.eventImage = getTopViewEvent.ImageUrl;
        }
    );
});