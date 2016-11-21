app.controller("AdminOrganizationController", function($scope, $http, $sce, $routeParams) {
    //Load statistic for organization tiles
    $http.get("/api/AdminOrganization/GetStatisticAboutOrganization").success(function (response) {
        $scope.OrgManageInfor = response.Data;
    });

    // Load new created organization
    $http.get("/api/AdminOrganization/GetNewesCreatedOrganization").success(function (response) {
        $scope.newCreatedOrg = response.Data;

    });
});