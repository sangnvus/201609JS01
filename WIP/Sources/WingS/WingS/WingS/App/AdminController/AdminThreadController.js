app.controller("AdminThreadController", function ($scope, $http, SweetAlert) {
    //Load Thread overview information
    $http.get("/api/AdminThread/GetThreadManageBasicInfor").success(function (response) {
        $scope.threadManageInfor = response.Data;
    });
    // Load 10 new created thread
    $http.get("/api/AdminThread/Get10NewestThread").success(function (response) {
        $scope.newCreatedThread = response.Data;
    });
    // Load Top 5 Thread have Like is most
    $http.get("/api/AdminThread/GetTopLikeThread").success(function (response) {
        $scope.topLikeThread = response.Data;
    });
    // Get all Thread in Thread table
    $http.get("/api/AdminThread/GetAllThread").success(function (response) {
        $scope.allThread = response.Data;
    });
    // Change status of thread
    $scope.activeThread = function (index, threadid) {
        $http({
            url: "/api/AdminThread/ChangeStatusThread",
            method: "GET",
            params: { threadid: threadid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.allThread[index].Status = response.Data;
        });
        return true;
    };
    // Alert admin before change status
    //Lock
    $scope.ban = function (index, threadid) {
        SweetAlert.swal({
            title: "Khóa bài viết",
            text: "Bạn muốn khóa bài viết này?",
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
                    $scope.activeThread(index, threadid);
                    SweetAlert.swal("Khóa!", "Bạn đã khóa bài viết thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Bài viết chưa được khóa", "error");
                }
            });
    };
    //Un lock
    $scope.unBan = function (index, threadid) {
        SweetAlert.swal({
            title: "Mở khóa bài viết",
            text: "Bạn muốn mở khóa bài viết này?",
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
                    $scope.activeThread(index, threadid);
                    SweetAlert.swal("Khóa!", "Bạn đã mở khóa bài viết thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Bài viết chưa được mở khóa", "error");
                }
            });
    };
});