app.directive('validFile', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ngModel) {
            //change event is fired when file is selected
            el.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(el.val());
                    ngModel.$render();
                });
            });
        }
    }
});
//check validate to startdate and enddate
app.directive('validateFinishDate', function($log) {
    return {
        restrict: 'A',
        link: function($scope, $element, $attr) {
            $scope.$watch('FinishDate', function() {
                var toDate = Date.parse($scope.FinishDate);
                var fromDate = Date.parse($scope.StartDate);
                if ($scope.CreateEventForm.FinishDate.$dirty) {
                    if (toDate < fromDate) {
                        $scope.CreateEventForm.FinishDate.$setValidity("validateFinishDate", false);
                        $scope.CreateEventForm.StartDate.$setValidity("validateStartDate", true);
                    } else {
                        $scope.CreateEventForm.FinishDate.$setValidity("validateFinishDate", true);
                        $scope.CreateEventForm.StartDate.$setValidity("validateStartDate", true);
                    }
                }
            })
        }
    };
});
app.directive('validateStartDate', function($log) {
    return {
        restrict: 'A',
        link: function($scope, $element, $attr) {
            $scope.$watch('StartDate', function() {
                var toDate = Date.parse($scope.FinishDate);
                var fromDate = Date.parse($scope.StartDate);
                if ($scope.CreateEventForm.StartDate.$dirty) {
                    if (toDate < fromDate) {
                        $scope.CreateEventForm.StartDate.$setValidity("validateStartDate", false);
                        $scope.CreateEventForm.FinishDate.$setValidity("validateFinishDate", true);
                    } else {
                        $scope.CreateEventForm.StartDate.$setValidity("validateStartDate", true);
                        $scope.CreateEventForm.FinishDate.$setValidity("validateFinishDate", true);
                    }
                }
            })
        }
    };
});