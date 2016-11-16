'use strict';
var app = angular.module('AdminApp', ['ngRoute', 'angular-loading-bar', 'ngAnimate', 'oitozero.ngSweetAlert']);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            tittle: "Admin Login",
            templateUrl: "/Admin/AdminLogin"

        })
        .when("/DashBoard", {
            tittle: "Thống kê chung",
            templateUrl: "/Admin/DashBoard",
            controller: "AdminDashboardController"
        })
        .when("/UserDashBoard", {
            tittle: "Thống kê chung",
            templateUrl: "/Admin/UserDashBoard",
            controller: "UserDashboardController"
        })
        .when("/UserProfile/:UserId", {
            tittle: "Thông tin cá nhân",
            templateUrl: "/Admin/UserProfile",
            controller: "UserProfileController"
        })
        .when("/UserList", {
            tittle: "Danh sách thành viên",
            templateUrl: "/Admin/UserList",
            controller: "UserListController"
        })
        .when("/ThreadDashBoard", {
            tittle: "Thống kê bài viết",
            templateUrl: "/Admin/ThreadDashBoard",
            controller: "AdminThreadController"
        })
        .when("/ThreadList", {
            tittle: "Danh sách bài viết",
            templateUrl: "/Admin/ThreadList",
            controller: "AdminThreadController"
        })
        .when("/ThreadDetail/:ThreadID", {
            tittle: "Chi tiết bài viết",
            templateUrl: "/Admin/ThreadDetail",
            controller: "AdminThreadDetailController"
        })
        .when("/EventDashBoard", {
            tittle: "Thống kê sự kiện",
            templateUrl: "/Admin/EventDashBoard",
            controller: "AdminEventController"
        })
        .when("/EventList", {
            tittle: "Danh sách sự kiện",
            templateUrl: "/Admin/EventList",
            controller: "AdminEventController"
        })
        .when("/EventDetail/:EventID", {
            tittle: "Chi tiết sự kiện",
            templateUrl: "/Admin/EventDetail",
            controller: "AdminEventController"
        });

});


app.run(['$location', '$rootScope', '$window', function ($location, $rootScope, $window, $localStorage) {
    $rootScope.$on("$routeChangeStart", function (e, curr, prev) {
        if (curr.$$route !== undefined && curr.$$route.title != null) {
            $rootScope.title = curr.$$route.title;

        } else $rootScope.title = "Admin WingS";

    });

   
}]);
