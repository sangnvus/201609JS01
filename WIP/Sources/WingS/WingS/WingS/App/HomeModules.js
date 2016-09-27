'use strict';
var app = angular.module('ClientApp', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        tittle: "Hãy đến với chúng tôi để chia sẻ từ thiện",
        templateUrl: "/Client/Home",
        controller: "HomeController"

    })
   .when("/Login", {
       tittle: "Đăng nhập",
       templateUrl: "/Client/Login"

   })
    .when("/Register", {
             tittle: "Đăng kí",
             templateUrl: "/Client/Register",
             controller: "RegisterController"
         })
    .when("/Home", {
        templateUrl: "/Client/Home",
        title: "Hãy đến với chúng tôi để chia sẻ từ thiện",
        controller: "HomeController"
    });
 

});

app.run(['$location', '$rootScope', '$window', function ($location, $rootScope, $window) {
    $rootScope.$on("$routeChangeStart", function (e, curr, prev) {
        if (curr.$$route !== undefined && curr.$$route.title != null) {
            $rootScope.title = curr.$$route.title;

        } else $rootScope.title = "WingS";

    });
  
    function checkLoginStatus() {
        $.getJSON("api/User/CheckLoginStatus").done(function (data) {
            if (data.Status === "success") {
                // Save authen info into $rootScope
                $rootScope.User_Information = data.Data;
                $rootScope.User_Information.IsAuthen = true;
            } else {
                $rootScope.User_Information = {
                    IsAuthen: false
                };
            }
        })
    }
    ////Call function checkLoginStatus
    checkLoginStatus();
    $window.fbAsyncInit = function () {
        FB.init({
            appId: '167432310370225',
            status: false,
            cookie: true,
            xfbml: true,
            version: 'v2.4'
        });
    };

    (function (d) {
        // load the Facebook javascript SDK

        var js,
            id = 'facebook-jssdk',
            ref = d.getElementsByTagName('script')[0];

        if (d.getElementById(id)) {
            return;
        }

        js = d.createElement('script');
        js.id = id;
        js.async = true;
        js.src = "//connect.facebook.net/en_US/sdk.js";

        ref.parentNode.insertBefore(js, ref);

    }(document));
}]);
