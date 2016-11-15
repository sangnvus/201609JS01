app.controller("UserEventController", function ($scope, $http, $sce) {
    //Load user static information
    $http.get("/api/AdminEvent/GetEventManageBasicInfor").success(function (response) {
        $scope.EventManageInfor = response.Data;
    });
});