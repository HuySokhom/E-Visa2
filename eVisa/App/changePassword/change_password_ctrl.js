app.controller('change_password_ctrl', [
    '$scope',
    '$window',
    '$http',
    function ($scope, $window, $http) {

        $scope.save = function () {
            if ($scope.confirm_password != $scope.new_password) {
                return Materialize.toast('Confirmation Password Not Match!', 4000);
            }
            var data = {
                oldPassword: $scope.old_password,
                newPassword: $scope.new_password
            };            
            $http.post("/User/ChangePassword", data).success(function (data) {
                if (data.success) {
                    return Materialize.toast('Save success. Password has been change', 4000);
                } else {
                    return Materialize.toast('Invalid old password.', 4000);
                }
            });
        };

    }
]);