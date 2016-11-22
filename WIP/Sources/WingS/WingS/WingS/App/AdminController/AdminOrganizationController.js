app.controller("AdminOrganizationController", function($scope, $http, $sce, $routeParams) {
    //Load statistic for organization tiles
    $http.get("/api/AdminOrganization/GetStatisticAboutOrganization").success(function (response) {
        $scope.OrgManageInfor = response.Data;
    });

    // Load new created organization
    $http.get("/api/AdminOrganization/GetNewesCreatedOrganization").success(function (response) {
        $scope.newCreatedOrg = response.Data;

    });

    // Load information of organization for organization dashboard cshtml: table ranking organization
    $http({
        url: "/api/Organization/GetOrganizationSortByPoint",
        method: "GET",
        contentType: "application/json"
    }).success(function (response) {
        $scope.RankingOrg = response.Data;
    });

    // Load information of organization for organization list cshtml
    $http({
        url: "/api/Organization/GetAllOrganization",
        method: "GET",
        contentType: "application/json"
    }).success(function (response) {
        $scope.allOrg = response.Data;
    });
});