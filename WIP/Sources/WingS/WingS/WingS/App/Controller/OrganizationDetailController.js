app.controller("OrganizationDetailController", function ($scope, $http, $routeParams) {
    var organizationId = $routeParams.OrgId;
    //get organization detail
    $http({
        url: "/api/Organization/GetOrganizationUsingId",
        method: "GET",
        params: { orgId: organizationId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.currentOrg = response.Data;
    });

    $.getJSON("api/User/GetCurrentUserId").done(function (data) {
        var currentUserId ="";
        if (data.Data != null) {
            currentUserId = data.Data.toString();
        }
        
        if (data.Status === "success" && currentUserId === organizationId) {
            $scope.isOwnerOrg = { "visibility": "visible" };
        } else {
            $scope.isOwnerOrg = { "visibility": "hidden" };
        }
    });

    $http({
        url: "/api/Organization/GetRankOfOrganization",
        method: "GET",
        params: { orgId: organizationId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.orgRank = response.Data;
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
   
    //Get top event of Organization
    
    $http({
        url: "/api/Event/GetTopOneViewedEventOfOrganization",
        method: "GET",
        params: { orgId: organizationId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.topEventsOfOrganization = response.Data;
    });

});