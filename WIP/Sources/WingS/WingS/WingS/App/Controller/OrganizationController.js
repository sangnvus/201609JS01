app.controller("OrganizationController", function ($scope, $http, $sce, $window, $compile) {
    $.getJSON("api/User/GetCurrentUserId").done(function (data) {
        if (data.Status === "success") {
            var loginUserId = data.Data;

            //append organization part
            var orgUserPartAppend = angular.element(document.querySelector("#orgUserPart"));
            orgUserPartAppend.append('<strong><h2>Tổ chức của bạn</h2></strong>');

            //get organization of current user
            $http({
                url: "/api/Organization/GetOrganizationUsingId",
                method: "GET",
                params: { orgId: loginUserId },
                contentType: "application/json"
            }).success(function (response) {
                $scope.orgBelongUser = response.Data;

                var orgUserAppend = angular.element(document.querySelector("#orgUser"));
                var breakTagAppend = angular.element(document.querySelector("#breaktag"));

                breakTagAppend.append('<hr/>');

                if (response.Status === "success") {
                    //User có tổ chức riêng
                    $scope.showOrgUser = function() {
                        return true;
                    }

                    //$("#orgUser").append(orgUserAppend);
                } else {
                    $scope.showOrgUser = function () {
                        return false;
                    }
                    //User chưa tạo tổ chức
                    orgUserAppend.append('<h4>Bạn chưa tạo tổ chức nào!&nbsp;<a href="">Hãy tạo một tổ chức</a></h4>');
                }

                
                
            });
            
        } else {
            //User chua dang nhap
        }
    });


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
