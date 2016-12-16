app.controller("AdminReportThreadController", function ($scope, $http, $sce, SweetAlert) {

    //load report statistic with type = organization
    $http({
        url: "/api/Report/GetReportStatisticWithReportType",
        method: "Get",
        params: { reportType: "Threads" },
        contentType: "application/json"
    }).success(function (response) {
        $scope.reportStatistic = response.Data;
    });

    // Change status of thread
    $scope.activeThread = function (index, threadid) {
        $http({
            url: "/api/AdminThread/ChangeStatusThread",
            method: "GET",
            params: { threadid: threadid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.reportStatistic[index].IsreportedUserStatus = response.Data;
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


app.controller("AdminReportThreadDetailController", function ($scope, $http, $routeParams, $sce, SweetAlert) {
    var threadId = $routeParams.ThreadId;
    //get information of reported Thread
    $http({
        url: "/api/AdminThread/GetThreadWithId",
        method: "GET",
        params: { threadId: threadId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.isReportedThread = response.Data;
    });

    //Load report information about reported Thread
    $http({
        url: "/api/Report/GetReportDetailData",
        method: "GET",
        params: { isReportId: threadId, reportType: "Threads" },
        contentType: "application/json"
    }).success(function (response) {
        $scope.ThreadReportDetailData = response.Data;

        // after load thread's detail reports, change all status of report to false (seen - đã xem)
        if (response.Status === "success") {
            $http({
                url: "/api/Report/ChangeStatusOfAllReportWithIdAndType",
                method: "GET",
                params: { isReportId: threadId, reportType: "Threads" },
                contentType: "application/json"
            }).success(function (isSuccess) {
                var isChangeStatusSucces = isSuccess.Status;
            });
        }
    });

    $scope.activeThread = function (threadid) {
        $http({
            url: "/api/AdminThread/ChangeStatusThread",
            method: "GET",
            params: { threadid: threadid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.isReportedThread.IsActive = response.Data;
        });
        return true;
    };
    // Alert admin before change status
    //Lock
    $scope.ban = function (threadid) {
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
                    $scope.activeThread(threadid);
                    SweetAlert.swal("Khóa!", "Bạn đã khóa bài viết thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Bài viết chưa được khóa", "error");
                }
            });
    };
    //Un lock
    $scope.unBan = function (threadid) {
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
                    $scope.activeThread(threadid);
                    SweetAlert.swal("Khóa!", "Bạn đã mở khóa bài viết thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Bài viết chưa được mở khóa", "error");
                }
            });
    };
});