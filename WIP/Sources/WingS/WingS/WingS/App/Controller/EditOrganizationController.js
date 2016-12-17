app.controller("EditOrganizationController", function ($scope,$sce, $http, $routeParams) {
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
        $scope.editOrg.Introduction = $sce.trustAsHtml($scope.editOrg.Introduction);
    });
});