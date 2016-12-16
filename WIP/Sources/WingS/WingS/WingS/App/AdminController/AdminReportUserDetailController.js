app.controller("AdminReportUserDetailController", function ($scope, $http, $routeParams, $sce, SweetAlert) {
    var userDetailId = $routeParams.UserId;

    //Get user profile 
    $http({
        url: "/api/AdminUserProfile/GetUserProfileWithId",
        method: "GET",
        params: { userId: userDetailId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.currentUserProfile = response.Data;
    });

    $http({
        url: "/api/Report/GetReportDetailData",
        method: "GET",
        params: { isReportId: userDetailId, reportType: "Ws_User" },
        contentType: "application/json"
    }).success(function (response) {
        $scope.UserReportDetailData = response.Data;

        // after load user's detail reports, change all status of report to false (seen - đã xem)
        if (response.Status === "success") {
            $http({
                url: "/api/Report/ChangeStatusOfAllReportWithIdAndType",
                method: "GET",
                params: { isReportId: userDetailId, reportType: "Ws_User" },
                contentType: "application/json"
            }).success(function (isSuccess) {
                var isChangeStatusSucces = isSuccess.Status;
            });
        }
    });


    $scope.activeUser = function (userid) {
        $http({
            url: "/api/AdminUserList/ChangeStatusUser",
            method: "GET",
            params: { userid: userid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.currentUserProfile.IsActive = response.Data;
        });
        return true;
    };
    // Alert admin before change status
    //Lock
    $scope.ban = function (userid) {
        SweetAlert.swal({
            title: "Khóa tài khoản",
            text: "Bạn muốn khóa tài khoản này?",
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
                    $scope.activeUser(userid);
                    SweetAlert.swal("Khóa!", "Bạn đã khóa tài khoản thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Tài khoản chưa được khóa", "error");
                }
            });
    };
    //Un lock
    $scope.unBan = function (userid) {
        SweetAlert.swal({
            title: "Mở khóa tài khoản",
            text: "Bạn muốn mở khóa tài khoản này?",
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
                    $scope.activeUser(userid);
                    SweetAlert.swal("Khóa!", "Bạn đã mở khóa tài khoản thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Tài khoản chưa được mở khóa", "error");
                }
            });
    };
});