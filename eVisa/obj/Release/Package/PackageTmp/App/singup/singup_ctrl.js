app.controller('singup_ctrl', [
    '$scope',
    '$window',
    '$http',
    'alertify',
    function ($scope, $window, $http, $alertify) {

        $scope.userFormat = /^[a-zA-Z\d\-\_]+$/;
        $scope.save = function () {
            if ($scope.password != $scope.confirmPassword) {
                return $alertify.error("<b>Password not match</b>.");
            }
            var str = $("#UserId").val(); 
            var res = str.match($scope.userFormat);
            if ( !res ) {
                return $alertify.error("<b>Invalid UserId.</b>.");
            }
            if ($scope.password.length < 8) {
                return $alertify.error("<b>Password should be 8 character.</b>.");
            }
            var Json = {
                UserId: $scope.userId,
                Password: $scope.password
            };
            $scope.process = true;
            //console.log(Json); 
            $http.post("/User/SignUp", Json).success(function (data) {
                //console.log(data);
                $scope.process = false;
                if (data.success) {
                    $alertify.success("Save Success.");
                    $window.location.href = '/User/#/Profile';
                } else {
                    $alertify.error(data.message);
                }
                
            });
        };
        

    }
]);

/* Directives check match elements */
app.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function ($scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                $scope.$apply(function () {
                    //console.info(elem.val() === $(firstPassword).val());
                    //console.log(elem.val());console.log($(firstPassword).val());
                    ctrl.$setValidity('pwmatch', elem.val() === $(firstPassword).val());
                });
            });
        }
    }
}]);