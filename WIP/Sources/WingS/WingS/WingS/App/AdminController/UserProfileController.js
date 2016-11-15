app.controller("UserProfileController", function ($scope, $http,$routeParams, $sce) {
    var UserId = $routeParams.UserId;

    //Get user profile 
    $http({
        url: "/api/AdminUserProfile/GetUserProfileWithId",
        method: "GET",
        params: { userId: UserId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.currentUserProfile = response.Data;
    });

    //Get user donation information
    $http({
        url: "/api/AdminUserProfile/GeUsertDonationInformation",
        method: "GET",
        params: { userId: UserId },
        contentType: "application/json"
    }).success(function(response) {
        $scope.currentUserDonation = response.Data;
    });

    //Get created thread information
    $http({
        url: "/api/AdminUserProfile/GetCreatedThreadOfUser",
        method: "GET",
        params: { userId: UserId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.currentUserThread = response.Data;
    });
});