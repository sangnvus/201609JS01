app.controller("MessagesController", function ($scope, $http) {
    //Get All conservation
    $scope.MessageName =  "Tin nhắn";
    $http.get("/api/Conservation/GetAllConservation").success(function (response) {
        $scope.Conservation = response.Data;
    });
    //Load All Message by ConservationId
    $scope.getMessage = function(userName, conservationId)
    {
        $scope.MessageName = userName;
        $http({
            url: "/api/Conservation/GetAllMessageByConservationId",
            method: "GET",
            params: { conservationId: conservationId },
            contentType: "application/json",
        }).success(function (response) {
            $scope.Message = response.Data;
        });
    }
});
