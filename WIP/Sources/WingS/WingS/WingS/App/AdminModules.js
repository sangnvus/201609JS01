'use strict';
var app = angular.module('AdminApp', ['ngRoute', 'angular-loading-bar', 'ngAnimate']);

app.config(function ($routeProvider) {
    $routeProvider
   .when("/", {
       tittle: "DashBoard",
       templateUrl: "/Admin/UserDashBoard",
       controller: "AdminDashBoardController"
   });
});


app.run(['$location', '$rootScope', '$window', function ($location, $rootScope, $window, $localStorage) {
    $rootScope.$on("$routeChangeStart", function (e, curr, prev) {
        if (curr.$$route !== undefined && curr.$$route.title != null) {
            $rootScope.title = curr.$$route.title;

        } else $rootScope.title = "Admin WingS";

    });

   
}]);
