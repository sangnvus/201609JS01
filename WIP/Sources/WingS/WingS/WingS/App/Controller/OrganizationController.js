app.controller("OrganizationController", function ($scope, $http, $sce, $window, $compile) {


    $http.get("/api/Organization/GetOrganizationSortByPoint").success(function(response) {
        $scope.OrgList = response.Data;
        var pageShow = 4;
        var index = 2;
        $scope.paginationLimit = function () {
            var limit = pageShow * index;
            return limit;
        };

        $scope.hasMoreItemsToShow = function () {
            if (pageShow < ($scope.OrgList.length / index)) {
                return true;
            } else {
                return false;
            }
            
        };

        $scope.showMoreItems = function () {
            index = index + 1;
        };
    });
    
    $scope.SkipValidation = function (value) {
        return $sce.trustAsHtml(value);
    };
});
