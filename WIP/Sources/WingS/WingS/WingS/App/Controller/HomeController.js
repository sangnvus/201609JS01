app.controller("HomeController", function ($scope,$http) {
    $http.get("/api/Event/GetTopViewEvent").success(function(response) {
        var listviewdEvent = response.Data;
        }
    );
});