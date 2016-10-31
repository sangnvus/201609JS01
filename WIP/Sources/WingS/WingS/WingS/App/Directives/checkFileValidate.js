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
app.directive("compareWithStartDate", function () {
    return {
        restrict: "A",
        require: "?ngModel",
        link: function (scope, element, attributes, ngModel) {
            validateEndDate = function (endDate, startDate) {
                if (endDate && startDate) {
                    startDate = new Date(startDate);
                    startDate.setHours(0, 0, 0, 0);
                    endDate = new Date(endDate);
                    endDate.setHours(0, 0, 0, 0);
                    return endDate >= startDate;
                }
                else {
                    return true;
                }
            }

            // use $validators.validation_name to do the validation
            ngModel.$validators.checkEndDate = function (modelValue) {
                var startdate = Date.parse(attributes.startDate);
                var enddate = modelValue;
                return validateEndDate(enddate, startdate);
            };

            // use $observe if we need to keep an eye for changes on a passed value
            attributes.$observe('startDate', function (value) {
                var startdate = Date.parse(value);
                var enddate = Date.parse(ngModel.$viewValue);

                // use $setValidity method to determine the validation result 
                // the first parameter is the validation name, this name is the same in ng-message template as well
                // the second parameter sets the validity (true or false), we can pass a function returning a boolean
                ngModel.$setValidity("checkEndDate", validateEndDate(enddate, startdate));
            });
        }
    };
});


// function to parse date time object into yyyy-mm-dd format string
Date.prototype.yyyymmdd = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based         
    var dd = this.getDate().toString();

    return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
};