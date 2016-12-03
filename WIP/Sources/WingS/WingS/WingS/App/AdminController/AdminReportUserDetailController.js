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
        url: "/api/Report/GetUserReportDetailData",
        method: "GET",
        params: { userId: userDetailId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.UserReportDetailData = response.Data;
    });

    // Alert admin before change status
    //Lock
    $scope.ban = function (index, userid) {
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
                    $scope.activeUser(index, userid);
                    SweetAlert.swal("Khóa!", "Bạn đã khóa tài khoản thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Tài khoản chưa được khóa", "error");
                }
            });
    };
    //Un lock
    $scope.unBan = function (index, userid) {
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
                    $scope.activeUser(index, userid);
                    SweetAlert.swal("Khóa!", "Bạn đã mở khóa tài khoản thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Tài khoản chưa được mở khóa", "error");
                }
            });
    };
});