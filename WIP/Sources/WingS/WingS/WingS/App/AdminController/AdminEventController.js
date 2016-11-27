//controller for event dashboard
app.controller("AdminEventDashBoardController", function ($scope, $http) {
    $http.get("/api/AdminEvent/GetEventManageBasicInfor").success(function (response) {
        $scope.EventManageInfor = response.Data;
    });
    $http.get("/api/AdminEvent/GetTopNewEvent").success(function (response) {
        $scope.TopNewEvent = response.Data;
    });
    $http.get("/api/AdminEvent/GetTopDonatedEvent").success(function (response) {
        $scope.TopHotEvent = response.Data;
    });
});
//controller for evetn detail
app.controller("AdminEventDetailController", function ($scope, $http, $sce, $routeParams) {
    $scope.parseFloat = function (val) {
        return isNaN(parseFloat(val)) ? 0 : parseFloat(val);
    }
    function getYoutubeId(url) {
        var regExp = /^.*(youtu\.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
        var match = url.match(regExp);
        if (match && match[2].length == 11) {
            return match[2];
        } else {
            return "";
        }
    }
    var eventId = $routeParams.EventID;
    $http({
        url: "/api/AdminEvent/GetEventWithId",
        method: "GET",
        params: { eventId: eventId },
        contentType: "application/json"
    }).success(function (response) {
        $scope.currentEvent = response.Data;
        $scope.widthEventDetail = (parseFloat($scope.currentEvent.RaisedMoney) / parseFloat($scope.currentEvent.ExpectedMoney)) + '%';
        $scope.currentEvent.VideoUrl = "http://www.youtube.com/embed/" + getYoutubeId($scope.currentEvent.VideoUrl);
        $scope.currentEvent.ExpectedMoney = $scope.currentEvent.ExpectedMoney.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        $scope.currentEvent.RaisedMoney = $scope.currentEvent.RaisedMoney.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        $scope.currentEvent.VideoUrl = $sce.trustAsResourceUrl($scope.currentEvent.VideoUrl);
        $scope.currentEvent.Content = $sce.trustAsHtml($scope.currentEvent.Content);
    });
});
//controller for event list page
app.controller("AdminEventListController", function ($scope, $http, SweetAlert) {
    $http.get("/api/AdminEvent/GetAllEvent").success(function (response) {
        $scope.AllEventlist = response.Data;
    });
    $http.get("/api/AdminEvent/GetEventManageBasicInfor").success(function (response) {
        $scope.EventManageInfor = response.Data;
    });
    //do active or ban
    $scope.activeEvent = function (index, eventid) {
        $http({
            url: "/api/AdminEvent/ChangeStatusEvent",
            method: "GET",
            params: { eventId: eventid },
            contentType: "application/json"
        }).success(function (response) {
            $scope.AllEventlist[index].TimeStatus = response.Data;
        });
        return true;
    };
    //ban
    $scope.banEvent = function (index, eventid) {
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
    $scope.unbanEvent = function (index, eventid) {
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