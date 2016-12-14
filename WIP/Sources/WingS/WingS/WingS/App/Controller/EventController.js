app.controller("EventController", function ($scope, $http) {
    var pageShow = 4;
    var index = 1;
    //Default when router to Event page
    $http.get("/api/Event/NewestEvent").success(function (response) {
        $scope.EventList = response.Data;
        //load more event
        
        $scope.hasMoreItemsToShow = function () {
            return pageShow < ($scope.EventList.length / index);
        };
        
    });

    //initial option sort
    $scope.listOfOptions = ['Ngày Tạo', 'Số người quyên góp','Điểm tích lũy'];
    $scope.selectedItem = "Ngày Tạo";
    $scope.LoadEventSortByOpion = function () {
        var sortOption = $scope.selectedItem;
        if (sortOption === "Điểm tích lũy") {
            //Load Event follow point
            $http.get("/api/Event/EventsSortByPoint").success(function(response) {
                $scope.EventList = response.Data;
            });
        }
        if (sortOption === "Ngày Tạo") {
            //Load Event follow create date
            $http.get("/api/Event/NewestEvent").success(function (response) {
                $scope.EventList = response.Data;
            });
        }
        if (sortOption === "Số người quyên góp") {
            //Load Event follow create date
            $http.get("/api/Event/EventsSortByNumberUserDonatedIn").success(function (response) {
                $scope.EventList = response.Data;
            });
        }

        //Load Event follow number of follower
        
    };
    //Load All Event Type
    $http({
        url: "/api/Event/GetEventsType",
        method: "GET",
        contentType: "application/json"
    }).success(function (response) {
        $scope.EventType = response.Data;
    });
    // Get events using specify type
    $scope.getEventByType = function(typeId) {
        $http({
            url: "/api/Event/GetEventsFollowEventType",
            method: "GET",
            params: { typeEventId: typeId },
            contentType: "application/json"
        }).success(function (response) {
            $scope.EventList = response.Data;
        });
    };
    
  
    //Load 3 organization to Event page
    $http.get("/api/Organization/GetTopThreeOrganization").success(function (response) {
        $scope.Organization = response.Data;
    });

    $scope.paginationLimit = function (data) {
        return pageShow * index;
    };

    $scope.showMoreItems = function () {
        index = index + 1;
    };
});