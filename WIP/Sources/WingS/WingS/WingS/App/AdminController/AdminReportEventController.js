app.controller("AdminReportEventController", function ($scope, $http, $sce, SweetAlert) {

    //load report statistic with type = Events
    $http({
        url: "/api/Report/GetReportStatisticWithReportType",
        method: "Get",
        params: { reportType: "Events" },
        contentType: "application/json"
    }).success(function (response) {
        $scope.reportStatistic = response.Data;
    });

    // Change status of Event
    $scope.activeEvent = function (index, eventid) {
        $http({
            url: "/api/AdminEvent/ChangeStatusEvent",
            method: "GET",
            params: { eventId: eventid },
            contentType: "application/json"
        }).success(function (response) {
            if (response.Data === "ban") {
                $scope.reportStatistic[index].IsreportedUserStatus = false;
            } else {
                $scope.reportStatistic[index].IsreportedUserStatus = true;
            }
        });
        return true;
    };
    
    //ban
    $scope.ban = function (index, eventid) {
        SweetAlert.swal({
            title: "Khóa sự kiện",
            text: "Bạn muốn khóa sự kiện này?",
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
                    $scope.activeEvent(index, eventid);
                    SweetAlert.swal("Khóa!", "Bạn đã khóa sự kiện thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Sự kiện chưa được khóa", "error");
                }
            });
    };
    //Un lock
    $scope.unban = function (index, eventid) {
        SweetAlert.swal({
            title: "Mở khóa sự kiện",
            text: "Bạn muốn mở khóa sự kiện này?",
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
                    $scope.activeEvent(index, eventid);
                    SweetAlert.swal("Khóa!", "Bạn đã mở khóa sự kiện thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Sự kiện chưa được mở khóa", "error");
                }
            });
    };
});

app.controller("AdminReportEventDetailController", function ($scope, $http, $routeParams, $sce, SweetAlert) {
    var eventId = $routeParams.EventId;
    //get information of reported Event
    $http({
        url: "/api/AdminEvent/GetEventWithId",
        method: "GET",
        params: { eventId: eventId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.isReportedEvent = response.Data;
    });

    //Load report information about reported Event
    $http({
        url: "/api/Report/GetReportDetailData",
        method: "GET",
        params: { isReportId: eventId, reportType: "Events" },
        contentType: "application/json"
    }).success(function (response) {
        $scope.EventReportDetailData = response.Data;

        // after load event's detail reports, change all status of report to false (seen - đã xem)
        if (response.Status === "success") {
            $http({
                url: "/api/Report/ChangeStatusOfAllReportWithIdAndType",
                method: "GET",
                params: { isReportId: eventId, reportType: "Events" },
                contentType: "application/json"
            }).success(function (isSuccess) {
                var isChangeStatusSucces = isSuccess.Status;
            });
        }
    });

    // Change status of Event
    $scope.activeEvent = function (index, eventid) {
        $http({
            url: "/api/AdminEvent/ChangeStatusEvent",
            method: "GET",
            params: { eventId: eventid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.reportStatistic[index].IsreportedUserStatus = response.Data;
        });
        return true;
    };

    //ban
    $scope.ban = function (eventid) {
        SweetAlert.swal({
            title: "Khóa sự kiện",
            text: "Bạn muốn khóa sự kiện này?",
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
                    $scope.activeEvent(eventid);
                    SweetAlert.swal("Khóa!", "Bạn đã khóa sự kiện thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Sự kiện chưa được khóa", "error");
                }
            });
    };
    //Un lock
    $scope.unban = function (eventid) {
        SweetAlert.swal({
            title: "Mở khóa sự kiện",
            text: "Bạn muốn mở khóa sự kiện này?",
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
                    $scope.activeEvent(eventid);
                    SweetAlert.swal("Khóa!", "Bạn đã mở khóa sự kiện thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Sự kiện chưa được mở khóa", "error");
                }
            });
    };
});