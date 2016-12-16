app.controller("OrganizationDetailController", function ($scope, $http, $sce, $routeParams, $rootScope) {
    var organizationId = $routeParams.OrgId;
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
        var currentUserId = "";
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
    function setLoadMore() {
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
    }
    //Get event belong to Organization
    $http({
        url: "/api/Event/GetEventListOfOrganization",
        method: "GET",
        params: { orgId: organizationId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.eventsOfOrganization = response.Data;
        setLoadMore();
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
    //Send message 
    $scope.sendMessage = function (titleMessage, content) {
        $http({
            url: "/api/Conservation/AddConservation",
            method: "post",
            data: { Title: titleMessage, Content: content, ReceiverName: $scope.currentOrg.CreatorName },
            contentType: "application/json",
        }).success(function (response) {
            $(".modal-message-body").hide();
            $(".message-sendMessage").hide();
            $(".modal-message-message").show();
            $(".message-closeForm").show();
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

    //Sort when change dropdownlist
    $scope.listOfOptions = ['Tất cả', 'Điểm', 'Đang diễn ra', 'Đã diễn ra'];
    $scope.selectedItem = "Tất cả";
    $scope.SortEvent = function () {
        var sortOption = $scope.selectedItem;
        if (sortOption === "Tất cả") {
            $http({
                url: "/api/Event/GetEventListOfOrganization",
                method: "GET",
                params: { orgId: organizationId },
                contentType: "application/json"
            }).success(function (response) {
                $scope.eventsOfOrganization = response.Data;
                setLoadMore();
            });
        }
        else if (sortOption === "Điểm") {
            $http({
                url: "/api/Event/OrderEventListOfOrganizationByPoint",
                method: "GET",
                params: { orgId: organizationId },
                contentType: "application/json"
            }).success(function (response) {
                $scope.eventsOfOrganization = response.Data;
                setLoadMore();
            });
        }
        else if (sortOption === "Đang diễn ra") {
            $http({
                url: "/api/Event/OrderEventListOfOrganizationByStatus",
                method: "GET",
                params: { orgId: organizationId, isStatus: true },
                contentType: "application/json"
            }).success(function (response) {
                $scope.eventsOfOrganization = response.Data;
                setLoadMore();
            });
        }
        else (sortOption === "Đã diễn ra")
        {
            $http({
                url: "/api/Event/OrderEventListOfOrganizationByStatus",
                method: "GET",
                params: { orgId: organizationId, isStatus: false },
                contentType: "application/json"
            }).success(function (response) {
                $scope.eventsOfOrganization = response.Data;
                setLoadMore();
            });

        }
        //Load Event follow number of follower

    };


});