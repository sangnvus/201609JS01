app.controller("AdminOrganizationDetailController", function ($scope, $http, $sce, SweetAlert, $routeParams) {
    var organizationId = $routeParams.OrganizationID;
    //get organization detail
    $http({
        url: "/api/Organization/GetOrganizationUsingId",
        method: "GET",
        params: { orgId: organizationId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.currentOrg = response.Data;
        $scope.currentOrg.Introduction = $sce.trustAsHtml($scope.currentOrg.Introduction);
    });

    

   

    //Get event belong to Organization
    $http({
        url: "/api/Event/GetEventListOfOrganization",
        method: "GET",
        params: { orgId: organizationId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.eventsOfOrganization = response.Data;
        var pageShow = 4;
        var index = 1;
        $scope.paginationLimit = function (data) {
            return pageShow * index;
        };
        $scope.hasMoreItemsToShow = function () {
            return pageShow < ($scope.eventsOfOrganization.length / index);
        };
        $scope.showMoreItems = function () {
            index = index + 1;
        };
    });
});