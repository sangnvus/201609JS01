app.controller("DonationController", function ($scope, $http, $routeParams, $sce) {
    var eventId = $routeParams.EventId;
    $http({
        url: "/api/DonationLoad/GetDonateInfo",
        method: "GET",
        params: { eventId: eventId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.DonateInfo = response.Data;
    });
});

app.controller("DonationDoneController", function ($scope, $http, $sce) {
    $http({
        url: "/Donation/AddNewDonation",
        method: "GET",
        params: {},
        contentType: "application/json"
    }).success(function (response) {
        
    });
});
