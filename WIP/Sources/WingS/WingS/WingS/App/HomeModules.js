'use strict';
var app = angular.module('ClientApp', ['ngRoute']);
app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        tittle: "Hãy đến với chúng tôi để chia sẻ từ thiện",
        templateUrl: "/Client/Home"
    })
    .when("/Discussion", {
     
    });

});

