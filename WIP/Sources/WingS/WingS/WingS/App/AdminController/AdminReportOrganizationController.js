app.controller("AdminReportOrganizationController", function ($scope, $http, $sce, SweetAlert) {

    //load report statistic with type = organization
    $http({
        url: "/api/Report/GetReportStatisticWithReportType",
        method: "Get",
        params: { reportType: "Organizations" },
        contentType: "application/json"
    }).success(function (response) {
        $scope.reportStatistic = response.Data;
    });

    // Change status of Organization (Mean lock or unlock organization)
    $scope.activeOrganization = function (index, orid) {
        $http({
            url: "/api/AdminOrganization/ChangeStatusOrganization",
            method: "GET",
            params: { OrganizationId: orid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.reportStatistic[index].IsreportedUserStatus = response.Data;
        });
        return true;
    };
    
    // Alert admin before change status
    //Lock
    $scope.ban = function (index, orid) {
        SweetAlert.swal({
            title: "Khóa tổ chức",
            text: "Bạn muốn khóa tổ chức này?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#16a085",
            confirmButtonText: "Có",
            cancelButtonText: "Không",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $scope.activeOrganization(index, orid);
                    SweetAlert.swal("Khóa!", "Bạn đã khóa tổ chức thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Tổ chức chưa được khóa", "error");
                }
            });
    };
    //Un lock
    $scope.unBan = function (index, orid) {
        SweetAlert.swal({
            title: "Mở khóa tổ chức",
            text: "Bạn muốn mở khóa tổ chức này?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#16a085",
            confirmButtonText: "Có",
            cancelButtonText: "Không",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $scope.activeOrganization(index, orid);
                    SweetAlert.swal("Khóa!", "Bạn đã mở khóa tổ chức thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Tổ chức chưa được mở khóa", "error");
                }
            });
    };
});

app.controller("AdminReportOrganizationDetailController", function($scope, $http, $routeParams, $sce, SweetAlert) {
    var orgId = $routeParams.OrganizationId;
    //get information of reported Organization
    $http({
        url: "/api/Organization/GetOrganizationUsingId",
        method: "GET",
        params: { orgId: orgId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.isReportedOrganization = response.Data;
    });

    //Load report information about reported organization
    $http({
        url: "/api/Report/GetReportDetailData",
        method: "GET",
        params: { isReportId: orgId, reportType: "Organizations" },
        contentType: "application/json"
    }).success(function(response) {
        $scope.OrganizationReportDetailData = response.Data;
        
        // after load organziation's detail reports, change all status of report to false (seen - đã xem)
        if (response.Status === "success") {
            $http({
                url: "/api/Report/ChangeStatusOfAllReportWithIdAndType",
                method: "GET",
                params: { isReportId: orgId, reportType: "Organizations" },
                contentType: "application/json"
            }).success(function (isSuccess) {
                var isChangeStatusSucces = isSuccess.Status;
            });
        }
    });

    $scope.activeOrganization = function (orid) {
        $http({
            url: "/api/AdminOrganization/ChangeStatusOrganization",
            method: "GET",
            params: { OrganizationId: orid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.isReportedOrganization.IsActive = response.Data;
        });
        return true;
    };
    // Alert admin before change status
    //Lock
    $scope.ban = function (orid) {
        SweetAlert.swal({
            title: "Khóa tổ chức",
            text: "Bạn muốn khóa tổ chức này?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#16a085",
            confirmButtonText: "Có",
            cancelButtonText: "Không",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $scope.activeOrganization(orid);
                    SweetAlert.swal("Khóa!", "Bạn đã khóa tổ chức thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Tổ chức chưa được khóa", "error");
                }
            });
    };
    //Un lock
    $scope.unBan = function (orid) {
        SweetAlert.swal({
            title: "Mở khóa tổ chức",
            text: "Bạn muốn mở khóa tổ chức này?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#16a085",
            confirmButtonText: "Có",
            cancelButtonText: "Không",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $scope.activeOrganization(orid);
                    SweetAlert.swal("Khóa!", "Bạn đã mở khóa tổ chức thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Tổ chức chưa được mở khóa", "error");
                }
            });
    };
});