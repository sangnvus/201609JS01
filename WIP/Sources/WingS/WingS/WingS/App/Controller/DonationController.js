app.controller("DonationController", function ($scope, $http, $routeParams, $sce) {
    var eventId = $routeParams.EventId;
    $http({
        url: "/api/Event/",
        method: "Get",
        params: { id: eventId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.DonateInfo = response.Data;
    });
});

