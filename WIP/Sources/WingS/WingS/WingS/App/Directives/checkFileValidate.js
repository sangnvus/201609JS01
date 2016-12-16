app.directive('validFile', function () {
var validFormats = ['image/jpg', 'image/gif','image/jpeg','image/png','image/bmp'];
    return {
        require: 'ngModel',
        link: function ($scope, el, attrs, ngModel) {
            //change event is fired when file is selected
            el.bind('change', function () {
                var thisFile = this.files;
                $scope.CreateEventForm.FileUpload.$setValidity("validFileSize", true);
                $scope.CreateEventForm.FileUpload.$setValidity("validFileType", true);
                for (var i = 0; i < thisFile.length; i++) {
                    if (thisFile[i].size > 20000000) {
                        $scope.CreateEventForm.FileUpload.$setValidity("validFileSize", false);
                        break;
                    }
                    if (validFormats.indexOf(thisFile[i].type.toLowerCase()) === -1) {
                        $scope.CreateEventForm.FileUpload.$setValidity("validFileType", false);
                        break;
                    }
                }
                $scope.$apply(function () {
                    ngModel.$setViewValue(el.val());
                    ngModel.$render();
                });
            });
        }
    }
});

app.directive('validFileThread', function () {
    var validFormats = ['image/jpg', 'image/gif', 'image/jpeg', 'image/png', 'image/bmp'];
    return {
        require: 'ngModel',
        link: function ($scope, el, attrs, ngModel) {
            //change event is fired when file is selected
            el.bind('change', function () {
                var thisFile = this.files;
                $scope.CreateThread.Images.$setValidity("validFileSize", true);
                $scope.CreateThread.Images.$setValidity("validFileType", true);
                for (var i = 0; i < thisFile.length; i++) {
                    if (thisFile[i].size > 20000000) {
                        $scope.CreateThread.Images.$setValidity("validFileSize", false);
                        break;
                    }
                    if (validFormats.indexOf(thisFile[i].type.toLowerCase()) === -1) {
                        $scope.CreateThread.Images.$setValidity("validFileType", false);
                        break;
                    }
                }
                $scope.$apply(function () {
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