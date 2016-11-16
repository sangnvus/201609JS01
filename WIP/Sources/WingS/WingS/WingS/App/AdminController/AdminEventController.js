app.controller("AdminEventController", function ($scope, $http, $sce) {
    //Load user static information
    $http.get("/api/AdminEvent/GetEventManageBasicInfor").success(function (response) {
        $scope.EventManageInfor = response.Data;
    });
    $http.get("/api/AdminEvent/GetTopNewEvent").success(function (response) {
        $scope.TopNewEvent = response.Data;
    });
    $http.get("/api/AdminEvent/GetTopHotEvent").success(function (response) {
        $scope.TopHotEvent = response.Data;
    });
    $http.get("/api/AdminEvent/GetAllEvent").success(function (response) {
        $scope.AllEventlist = response.Data;
    });
});