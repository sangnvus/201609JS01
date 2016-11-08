'use strict';
var app = angular.module('AdminApp', ['ngRoute', 'angular-loading-bar', 'ngAnimate']);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            tittle: "Admin Login",
            templateUrl: "/Admin/AdminLogin"

        })
        .when("/DashBoard", {
            tittle: "Thống kê chung",
            templateUrl: "/Admin/DashBoard"

        })
        .when("/UserDashBoard", {
                    tittle: "Thống kê chung",
                    templateUrl: "/Admin/UserDashBoard"

        })
        .when("/UserProfile", {
            tittle: "Thông tin cá nhân",
            templateUrl: "/Admin/UserProfile"

        })
        .when("/UserList", {
            tittle: "Danh sách thành viên",
            templateUrl: "/Admin/UserList",
            controller: "UserListController"
        });

});


app.run(['$location', '$rootScope', '$window', function ($location, $rootScope, $window, $localStorage) {
    $rootScope.$on("$routeChangeStart", function (e, curr, prev) {
        if (curr.$$route !== undefined && curr.$$route.title != null) {
            $rootScope.title = curr.$$route.title;

        } else $rootScope.title = "Admin WingS";

    });

   
}]);
