app.controller("MessagesController", function ($scope, $http,$window) {
    //Get All conservation
    $scope.MessageName =  "Tin nhắn";
    $http.get("/api/Conservation/GetAllConservation").success(function (response) {
        $scope.Conservation = response.Data;
    });
    //Load All Message by ConservationId
    $scope.getMessage = function(userName, conservationId)
    {
        $("#textMessage").remove();
        $window.conservationId = conservationId;
        $scope.replyShow = true;
        $scope.CurrentConservationId = conservationId;
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
    $scope.addMessage = function(message, conservationId)
    {
        $http({
            url: "/api/Conservation/AddMessage",
            method: "GET",
            params: { ConservationId: conservationId, newMessage: message },
            contentType: "application/json",
        }).success(function (response) {
            $(".newMesage").val("");
        });
    }
    $scope.listOfOptions = ['Tất cả', 'Đã đọc', 'Chưa đọc'];
    $scope.selectedItem = "Tất cả";
    $scope.GetConversation = function () {
        var sortOption = $scope.selectedItem;
        if (sortOption === "Tất cả") {
            $http.get("/api/Conservation/GetAllConservation").success(function (response) {
                $scope.Conservation = response.Data;
            });
        }
        if (sortOption === "Đã đọc") {
            $http({
                url: "/api/Conservation/GetConservationByStatus",
                method: "GET",
                params: { status: true },
            }).success(function (response) {
                $scope.Conservation = response.Data;
            });
        }
         if (sortOption === "Chưa đọc") {
            $http({
                url: "/api/Conservation/GetConservationByStatus",
                method: "GET",
                params: { status: false },
            }).success(function (response) {
                $scope.Conservation = response.Data;
            });
        }
    };
});
