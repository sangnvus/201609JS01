'use strict';
var app = angular.module('ClientApp', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        tittle: "Hãy đến với chúng tôi để chia sẻ từ thiện",
        templateUrl: "/Client/Home",
        controller: "HomeController"
    })
    .when("/Home", {
        templateUrl: "/Client/Home",
        title: "Hãy đến với chúng tôi để chia sẻ từ thiện",
        controller: "HomeController"
    });

});
app.run(['$location', '$rootScope', function ($location, $rootScope) {
    $rootScope.$on("$routeChangeStart", function (e, curr, prev) {
        if (curr.$$route !== undefined && curr.$$route.title != null) {
            $rootScope.title = curr.$$route.title;

        } else $rootScope.title = "WingS";

    });
}]);
