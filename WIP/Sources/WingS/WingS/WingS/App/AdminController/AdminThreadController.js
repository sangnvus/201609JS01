app.controller("AdminThreadController", function ($scope, $http, $sce) {
    //Load Thread static information
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
});