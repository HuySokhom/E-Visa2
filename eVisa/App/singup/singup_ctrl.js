app.controller('singup_ctrl', [
    '$scope',
    '$window',
    '$http',
    function ($scope, $window, $http) {

        $scope.userFormat = /^[a-zA-Z\d\-\_]+$/;
        $scope.save = function () {
            if ($scope.password != $scope.confirmPassword) {
                return Materialize.toast('Password not match.', 4000);
            }
            var str = $("#UserId").val(); 
            var res = str.match($scope.userFormat);
            if ( !res ) {
                return Materialize.toast('Invalid UserId.', 4000);
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
                    $window.location.href = '/User/#/Profile';
                } else {
                    Materialize.toast(data.message, 4000);
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