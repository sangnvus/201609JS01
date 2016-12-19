'use strict';
var app = angular.module('ClientApp', ['ngRoute', 'datatables', 'datatables.bootstrap', 'angular-loading-bar', 'ngAnimate', 'oitozero.ngSweetAlert']);
    
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
        .when("/AboutUs", {
            tittle: "Về chúng tôi",
            templateUrl: "/Client/AboutUs"

        })
        .when("/Register", {
            tittle: "Đăng ký",
            templateUrl: "/Client/Register"
        })
        .when("/Home", {
            templateUrl: "/Client/Home",
            title: "Hãy đến với chúng tôi để chia sẻ từ thiện",
            controller: "HomeController"
        })
        .when("/ForgotPassword", {
            templateUrl: "/Client/ForgotPassword",
            title: "Quên mật khẩu"
        })
        .when("/Error", {
            templateUrl: "/Client/Error",
            title: "Đã xảy ra lỗi"
        })
        .when("/Search", {
            templateUrl: "/Client/Search",
            title: "Tìm kiếm"
        })
        .when("/Discussion", {
            templateUrl: "/Client/Discussion",
            title: "Xem thảo luận",
            controller: "DiscussionController"
        })
        .when("/CreateDiscussion", {
            templateUrl: "/Client/CreateDiscussion",
            resolve: {
                'check': function($location, $window) {
                    if ($window.sessionStorage.isAuthen == "false") {
                        $location.path('/Login');
                    }
                }
            },
            title: "Tạo thảo luận"

        })
        .when("/RegisterSuccess", {
            templateUrl: "/Client/RegisterSuccess",
            title: "Đăng ký thành công",
            controller: "RegisterSuccessController"
        })
        .when("/VerifyAccount", {
            templateUrl: "/Client/VerifyAccount",
            title: "Xác nhận tài khoản",
            controller: "VerifyAccountController"
        })
        .when("/ThreadDetail/:Id", {
            templateUrl: "/Client/ThreadDetail",
            title: "Chi tiết thảo luận",
            controller: "ThreadDetailController"
        })
        .when("/Event", {
            templateUrl: "/Client/Event",
            title: "Sự kiện",
            controller: "EventController"
        })
        .when("/EventDetail/:Id", {
            templateUrl: "/Client/EventDetail",
            title: "Chi tiết sự kiện",
            controller: "EventDetailController"
        })
        .when("/Profile/:UserName", {
            templateUrl: "/Client/Profile",
            title: "Thông tin cá nhân",
            controller: "ProfileController"
        })
        .when("/Messages", {
            templateUrl: "/Client/Messages",
            resolve: {
                'check': function($location, $window) {
                    if ($window.sessionStorage.isAuthen == "false") {
                        $location.path('/Login');
                    }
                }
            },
            title: "Tin nhắn",
            controller: "MessagesController"
        })
        .when("/CreateEvent", {
            templateUrl: "/Client/CreateEvent",
            resolve: {
                'check': function($location, $window) {
                    if ($window.sessionStorage.isAuthen == "false") {
                        $location.path('/Login');
                    }
                }
            },
            title: "Tạo sự kiện"
        })
        .when("/Organization", {
            templateUrl: "/Client/Organization",
            title: "Tổ chức",
            controller: "OrganizationController"
        })
        .when("/Donate/:EventId", {
            templateUrl: "/Client/Donate",
            title: "Quyên góp",
            resolve: {
                'check': function($location, $window) {
                    if ($window.sessionStorage.isAuthen == "false") {
                        $location.path('/Login');
                    }
                }
            },
            controller: "DonationController"
        })
        .when("/DonationComplete", {
            templateUrl: "/Client/DonationComplete",
            title: "Xác nhận giao dịch hoàn tất",
            resolve: {
                'check': function($location, $window) {
                    if ($window.sessionStorage.isAuthen == "false") {
                        $location.path('/Login');
                    }
                }
            }
        })
        .when("/DonationFailed", {
            templateUrl: "/Client/DonationFailed",
            title: "Giao dịch thất bại"
        })
        .when("/CreateOrganization", {
            templateUrl: "/Client/CreateOrganization",
            resolve: {
                'check': function($location, $window) {
                    if ($window.sessionStorage.isAuthen === "false") {
                        $location.path('/Login');
                    }
                    else if($window.sessionStorage.IsVerifyOrg ==="false")
                    {
                        $location.path('/RegistedNotify');
                    }
                }
            },
            title: "Tạo tổ chức"

        })
        .when("/OrganizationDetail/:OrgId", {
            templateUrl: "/Client/OrganizationDetail",
            title: "Chi tiết Tổ chức",
            controller: "OrganizationDetailController"
        })
        .when("/RegistedNotify", {
                templateUrl: "/Client/RegistedOrg",
                title: "Đã tạo tổ chức rồi",
        })
        .when("/EditOrganization/:OrgId", {
            templateUrl: "/Client/EditOrganization",
            title: "Chi tiết Tổ chức",
            resolve: {
                'check': function ($location, $window) {
                    if ($window.sessionStorage.isAuthen == "false") {
                        $location.path('/Login');
                    }
                }
            },
            controller: "EditOrganizationController"
        })
        .when("/Statistics", {
            templateUrl: "/Client/Statistics",
            title: "Thống kê",
            controller: "StatisticsController"
        })
        .when("/EditThread/:Id", {
            templateUrl: "/Client/EditThread",
            title: "Sửa thông tin của bài viết",
            resolve: {
                'check': function ($location, $window) {
                    if ($window.sessionStorage.isAuthen == "false") {
                        $location.path('/Login');
                    }
                }
            },
            controller: "EditThreadController",

        });
});


app.run(['$location', '$rootScope', '$window', function ($location, $rootScope, $window, $localStorage) {
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
                $window.sessionStorage.setItem("isAuthen", true);
                $window.sessionStorage.setItem("IsVerifyOrg", $rootScope.User_Information.IsOrganazationVerify);
            } else {
                $rootScope.User_Information = {
                    IsAuthen: false,
                    IsOrganazation: false
                };
                $window.sessionStorage.setItem("isAuthen", false);
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
