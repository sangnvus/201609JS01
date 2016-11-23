app.controller("AdminEventTypeController", function ($scope, $http, SweetAlert) {
    $http.get("/api/AdminEvent/GetAllEventType").success(function (response) {
        $scope.AllEventType = response.Data;
    });
});