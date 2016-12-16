app.controller("AdminDonationInfoController", function ($scope, $http) {
    $http.get("/api/DonationLoad/GetAllDonationBasicInformation").success(function (response) {
        $scope.AllDonationRecord = response.Data;
    });
});