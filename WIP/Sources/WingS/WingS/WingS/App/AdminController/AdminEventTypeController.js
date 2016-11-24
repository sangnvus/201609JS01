app.controller("AdminEventTypeController", function ($scope, $http, SweetAlert) {
    $http.get("/api/AdminEvent/GetAllEventType").success(function (response) {
        $scope.AllEventType = response.Data;
    });

    // function change status of event type
    $scope.activeEventType = function (index, eventTypeId) {
        $http({
            url: "/api/AdminEvent/ChangeSatusOfEventType",
            method: "GET",
            params: { eventTypeId: eventTypeId },
            contentType: "application/json"
        }).success(function (response) {
            $scope.AllEventType[index].IsActive = response.Data;
        });
        return true;
    };

    // alert admin before DEACTIVE event type
    $scope.ban = function (index, eventTypeId) {
        SweetAlert.swal({
            title: "Ngưng sử dụng loại sự kiện này",
            text: "Bạn ngưng sử dụng loại sự kiện này?",
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
                    $scope.activeEventType(index, eventTypeId);
                    SweetAlert.swal("Ngưng sử dụng!", "Bạn ngưng sử dụng loại sự kiện thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Loại sự kiện này chưa ngưng sử dụng", "error");
                }
            });
    };

    // alert admin before ACTIVE event type
    $scope.unBan = function (index, eventTypeId) {
        SweetAlert.swal({
            title: "Sử dụng loại sự kiện này",
            text: "Bạn muốn đưa loại sự kiện này vào sử dụng?",
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
                    $scope.activeEventType(index, eventTypeId);
                    SweetAlert.swal("Sử dụng!", "Bạn đã đưa loại sự kiện này vào sử dụng thành công", "success");
                } else {
                    SweetAlert.swal("Hủy", "Loại sự kiện này đã ngưng sử dụng", "error");
                }
            });
    };
});