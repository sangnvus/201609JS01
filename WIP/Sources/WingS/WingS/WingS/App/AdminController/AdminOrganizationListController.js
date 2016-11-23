app.controller("AdminOrganizationListController", function ($scope, $http, SweetAlert) {
    //Load statistic for organization tiles
    $http.get("/api/AdminOrganization/GetStatisticAboutOrganization").success(function (response) {
        $scope.OrgManageInfor = response.Data;
    });

    // Load information of organization for organization list cshtml
    $http({
        url: "/api/Organization/GetAllOrganization",
        method: "GET",
        contentType: "application/json"
    }).success(function (response) {
        $scope.allOrg = response.Data;
    });
    // Change status of Organization
    $scope.activeOrganization = function (index, orid) {
        $http({
            url: "/api/AdminOrganization/ChangeStatusOrganization",
            method: "GET",
            params: { OrganizationId: orid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.allOrg[index].IsActive = response.Data;
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