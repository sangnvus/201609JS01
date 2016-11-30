app.controller("AdminOrganizationRegisterController", function ($scope, $http, SweetAlert) {
    //Load statistic for organization tiles
    $http.get("/api/AdminOrganization/GetStatisticAboutOrganization").success(function (response) {
        $scope.OrgManageInfor = response.Data;
    });

    // Load information of organization for organization list cshtml
    $http({
        url: "/api/Organization/GetAllOrganizationWaitForAccept",
        method: "GET",
        contentType: "application/json"
    }).success(function (response) {
        $scope.allOrgWaitAccept = response.Data;
    });
    // Accept create Organization request
    $scope.acceptOrganization = function (index, orid) {
        $http({
            url: "/api/AdminOrganization/AcceptingOrganization",
            method: "GET",
            params: { OrganizationId: orid },
            contentType: "application/json"
        }).success(function (response) {
            //$scope.allOrgWaitAccept[index].IsActive = response.Data;
            //$scope.allOrgWaitAccept[index].IsVerify = response.Data;
            $scope.allOrgWaitAccept.splice(index, 1);
        });
        return true;
    };
    // Alert admin before accepting org
    //confirm 
    $scope.AcceptOrg = function (index, orid) {
        SweetAlert.swal({
            title: "Phê duyệt tổ chức",
            text: "Bạn muốn đưa tổ chức này vào hoạt động?",
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
                    $scope.acceptOrganization(index, orid);
                    SweetAlert.swal("Thông báo!", "Bạn đã đưa tổ chức vào hoạt động thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Tổ chức chưa được đưa vào hoạt động", "error");
                }
            });
    };

    // Cancel create Organization request
    $scope.cancelCreateOrganization = function (index, orid) {
        $http({
            url: "/api/AdminOrganization/CancelCreateOrganizationRequest",
            method: "GET",
            params: { OrganizationId: orid },
            contentType: "application/json"
        }).success(function (response) {
            if (response.Status === "success" && response.Data === true) {
                $scope.allOrgWaitAccept.splice(index, 1);
            }
        });
        return true;
    };

    //Cancel request to create organization
    $scope.cancelCreateOrg = function (index, orid) {
        SweetAlert.swal({
            title: "Hủy bỏ yêu cầu tạo tổ chức",
            text: "Bạn có muốn hủy bỏ yêu cầu tạo tổ chưc này?",
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
                    $scope.cancelCreateOrganization(index, orid);
                    SweetAlert.swal("Thông báo!", "Bạn đã loại bỏ yêu cầu tạo tổ chức này!", "success");
                } else {
                    SweetAlert.swal("Hủy", "Bạn chưa loại bỏ yêu cầu này!", "error");
                }
            });
    };
});