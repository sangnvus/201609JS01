app.controller("DonationController", function ($scope, $http, $routeParams, $window) {
    var eventId = $routeParams.EventId;
    $http({
        url: "/api/DonationLoad/GetDonateInfo",
        method: "GET",
        params: { eventId: eventId },
        contentType: "application/json"
    }).success(function (response) {
        if (response.Status === "success") {
            $scope.DonateInfo = response.Data;
        } else {
            $window.location.href = "/#/Error";
        }
    });
});