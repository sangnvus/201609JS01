app.controller("AdminReportUserController", function ($scope, $http, $sce, SweetAlert) {
    // Load user report data
    $http({
        url: "/api/Report/GetReportStatisticWithReportType",
        method: "Get",
        params: { reportType: "Ws_User" },
        contentType: "application/json"
    }).success(function (response) {
        $scope.reportStatistic = response.Data;
    });

    $scope.activeUser = function (index, userid) {
        $http({
            url: "/api/AdminUserList/ChangeStatusUser",
            method: "GET",
            params: { userid: userid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.reportStatistic[index].IsreportedUserStatus = response.Data;
        });
        return true;
    };
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