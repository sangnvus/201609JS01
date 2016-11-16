app.controller("NotificationController", function ($scope, $http, $window) {
    $http.get("/api/Notification/GetAllNotification").success(function (response) {
        $scope.Notification = response.Data;
    });
  
});
