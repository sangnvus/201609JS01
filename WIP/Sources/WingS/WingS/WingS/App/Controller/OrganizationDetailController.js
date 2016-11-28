app.controller("OrganizationDetailController", function ($scope, $http, $routeParams, $rootScope) {
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
    //Check current User reported this Org or not
    if ($rootScope.User_Information.IsAuthen == true && $rootScope.User_Information.UserId != organizationId) {
        $http({
            url: "/api/Report/CheckCurrentUserReportedOrNot",
            method: "GET",
            params: { Type: "Organizations", ReportTo: organizationId },
            contentType: "application/json",
        }).success(function (response) {
            $scope.checkReported = response.Data;
        });
    }
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
    //Get report content to bind to View
    $scope.getReportContent = function () {
        $http({
            url: "/api/Report/GetReportContentForOrg",
            method: "GET",
            contentType: "application/json",
        }).success(function (response) {
            $scope.ReportContent = response.Data;
        });
    };
    $scope.setValue = function (setValue) {
        $scope.radioValue = setValue;
    }
    $scope.sendReport = function () {
        $http({
            url: "/api/Report/ReportOrg",
            method: "get",
            params: { toOrgId: organizationId, Content: $scope.ReportContent[$scope.radioValue] },
            contentType: "application/json",
        }).success(function (response) {
            $(".modal-body").hide();
            $(".sendMessage").hide();
            $(".modal-message").show();
            $(".closeForm").show();
        });
    };
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