app.controller("EventController", function ($scope, $http) {
    //Load top 1 event to Event page
    $http.get("/api/Event/Top1View").success(function (response) {
        $scope.Top1Event = response.Data;
    });

    //Load Event follow create date
    //Default when router to Event page
    $http.get("/api/Event/NewestEvent").success(function (response) {
        $scope.EventList = response.Data;
    });

    //initial option sort
    $scope.listOfOptions = ['Ngày Tạo', 'Điểm'];
    
    $scope.LoadEventSortByPoint = function () {
        var sortOption = $scope.selectedItem;
        if (sortOption === "Điểm") {
            //Load Event follow point
            $http.get("/api/Event/EventsSortByPoint").success(function(response) {
                EventList = response.Data;
            });
        } else {
            //Load Event follow create date
            $http.get("/api/Event/NewestEvent").success(function (response) {
                $scope.EventList = response.Data;
            });
        }
        //Load Event follow number of follower
        
    };

    // Get event type is Văn hóa - Xã hội
    $scope.getEventSocial = function() {
        $http({
            url: "/api/Event/GetEventsFollowEventType",
            method: "GET",
            params: { typeEventId: 1 },
            contentType: "application/json"
        }).success(function (response) {
            $scope.EventList = response.Data;
        });
    };
    // Get event type is Giáo dục
    $scope.getEventEducation = function () {
        $http({
            url: "/api/Event/GetEventsFollowEventType",
            method: "GET",
            params: { typeEventId: 2 },
            contentType: "application/json"
        }).success(function (response) {
            $scope.EventList = response.Data;
        });
    };

    // Get event type is Y tế
    $scope.getEventMedical = function () {
        $http({
            url: "/api/Event/GetEventsFollowEventType",
            method: "GET",
            params: { typeEventId: 3 },
            contentType: "application/json"
        }).success(function (response) {
            $scope.EventList = response.Data;
        });
    };
    
    // Get event type is Khác
    $scope.getEventOther = function () {
        $http({
            url: "/api/Event/GetEventsFollowEventType",
            method: "GET",
            params: { typeEventId: 4 },
            contentType: "application/json"
        }).success(function (response) {
            $scope.EventList = response.Data;
        });
    };
  
    //Load 3 organization to Event page
    $http.get("/api/Organization/GetTopThreeOrganization").success(function (response) {
        $scope.Organization = response.Data;
    });

    //load more event
    var pageShow = 4;
    var index = 1;
    $scope.paginationLimit = function (data) {
        return pageShow * index;
    };
    $scope.hasMoreItemsToShow = function () {
        return pageShow < ($scope.Event.length / index);
    };
    $scope.showMoreItems = function () {
        index = index + 1;
    };
});