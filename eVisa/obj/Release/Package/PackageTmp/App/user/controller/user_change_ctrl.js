app.controller(
	'user_change_ctrl', [
	'$scope'
	, 'Restful'
	, '$location'
	, function ($scope, Restful, $location){
	    $scope.save = function () {
	        if ($scope.confirm_password != $scope.new_password) {
	            return Materialize.toast('Confirmation Password Not Match!', 4000);
	        }
	        var data = {
	            oldPassword: $scope.old_password,
	            newPassword: $scope.new_password
	        };
	        Restful.save("/User/ChangePassword", data).success(function (data) {
	            if (data.success) {
	                $location.path("#/");
	                return Materialize.toast('Save success. Password has been change', 4000);
	            } else {
	                return Materialize.toast('Invalid old password.', 4000);
	            }
	        });
	    };
	}
]);


