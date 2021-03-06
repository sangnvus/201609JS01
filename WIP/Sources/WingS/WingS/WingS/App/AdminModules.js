﻿'use strict';
var app = angular.module('AdminApp', ['ngRoute','datatables','datatables.bootstrap',  'angular-loading-bar', 'ngAnimate', 'oitozero.ngSweetAlert']);

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
            controller: "AdminEventDashBoardController"
        })
        .when("/EventList", {
            tittle: "Danh sách sự kiện",
            templateUrl: "/Admin/EventList",
            controller: "AdminEventListController"
        })
        .when("/EventType", {
            tittle: "Danh sách thể loại sự kiện",
            templateUrl: "/Admin/EventType",
            controller: "AdminEventTypeController"
        })
        .when("/EventDetail/:EventID", {
            tittle: "Chi tiết sự kiện",
            templateUrl: "/Admin/EventDetail",
            controller: "AdminEventDetailController"
        })
        .when("/OrganizationDashBoard", {
            tittle: "Thống kê tổ chức",
            templateUrl: "/Admin/OrganizationDashBoard",
            controller: "AdminOrganizationController"
        })
        .when("/OrganizationList", {
            tittle: "Danh sách sự kiện",
            templateUrl: "/Admin/OrganizationList",
            controller: "AdminOrganizationListController"
        })
        .when("/RegisterOrganization", {
            tittle: "Danh sách đăng ký",
            templateUrl: "/Admin/RegisterOrganization",
            controller: "AdminOrganizationRegisterController"
        })
        .when("/OrganizationDetail/:OrganizationID", {
            tittle: "Chi tiết sự kiện",
            templateUrl: "/Admin/OrganizationDetail",
            controller: "AdminOrganizationDetailController"
        })
        .when("/DonationInfo", {
            tittle: "Thông tin quyên góp",
            templateUrl: "/Admin/DonationInfo",
            controller: "AdminDonationInfoController"
        })
        .when("/ReportUser", {
            tittle: "Báo cáo người dùng",
            templateUrl: "/Admin/ReportUser",
            controller: "AdminReportUserController"
        })
        .when("/ReportUserDetail/:UserId", {
            tittle: "Báo cáo người dùng",
            templateUrl: "/Admin/ReportUserDetail",
            controller: "AdminReportUserDetailController"
        })
        .when("/ReportOrganization", {
            tittle: "Báo cáo tổ chức",
            templateUrl: "/Admin/ReportOrganization",
            controller: "AdminReportOrganizationController"
        })
        .when("/ReportOrganizationDetail/:OrganizationId", {
            tittle: "Báo cáo tổ chức",
            templateUrl: "/Admin/ReportOrganizationDetail",
            controller: "AdminReportOrganizationDetailController"
        })
        .when("/ReportThread", {
            tittle: "Báo cáo bài viết",
            templateUrl: "/Admin/ReportThread",
            controller: "AdminReportThreadController"
        })
        .when("/ReportThreadDetail/:ThreadId", {
            tittle: "Báo cáo bài viết",
            templateUrl: "/Admin/ReportThreadDetail",
            controller: "AdminReportThreadDetailController"
        })
        .when("/ReportEvent", {
            tittle: "Báo cáo sự kiện",
            templateUrl: "/Admin/ReportEvent",
            controller: "AdminReportEventController"
        })
        .when("/ReportEventDetail/:EventId", {
            tittle: "Báo cáo sự kiện",
            templateUrl: "/Admin/ReportEventDetail",
            controller: "AdminReportEventDetailController"
        });
});


app.run(['$location', '$rootScope', '$window', function ($location, $rootScope, $window, $localStorage) {
    $rootScope.$on("$routeChangeStart", function (e, curr, prev) {
        if (curr.$$route !== undefined && curr.$$route.title != null) {
            $rootScope.title = curr.$$route.title;

        } else $rootScope.title = "Admin WingS";

    });

   
}]);
