app.controller("UserListController", function ($scope, $http, SweetAlert) {
    //Load user static information for user circle tiles
    $http.get("/api/AdminUserDashboard/GetStatisticManageBasicInfor").success(function (response) {
        $scope.userManageInfor = response.Data;

    });

    $http.get("/api/AdminUserList/GetAllUser").success(function (response) {
        $scope.UserList = response.Data;
    });
    $scope.activeUser = function (index, userid) {
        $http({
            url: "/api/AdminUserList/ChangeStatusUser",
            method: "GET",
            params: { userid: userid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.UserList[index].IsActive = response.Data;
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
