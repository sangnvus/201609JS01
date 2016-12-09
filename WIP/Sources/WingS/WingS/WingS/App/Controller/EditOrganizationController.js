app.controller("EditOrganizationController", function ($scope, $http, $routeParams) {
    var organizationId = $routeParams.OrgId;
    //get organization detail
    $http({
        url: "/api/Organization/GetOrganizationUsingId",
        method: "GET",
        params: { orgId: organizationId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.editOrg = response.Data;
        $scope.OrganizationName = $scope.editOrg.OrganizationName;
        $scope.Introduction = $scope.editOrg.Introduction;
    });
});