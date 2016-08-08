app.controller(
	'user_change_ctrl', [
	'$scope'
	, 'Restful'
	, '$location'
    , 'alertify'
	, function ($scope, Restful, $location, $alertify) {
	    $scope.save = function () {
	        if ($scope.confirm_password != $scope.new_password) {
	            return $alertify.error('Confirmation Password Not Match!');
	        }
	        if ($scope.new_password.length < 8) {
	            return $alertify.error("New Password should be 8 character.");
	        }
	        var data = {
	            oldPassword: $scope.old_password,
	            newPassword: $scope.new_password
	        };
	        Restful.save("/User/ChangePassword", data).success(function (data) {
	            if (data.success) {
	                $location.path("#/");
	                return $alertify.success("<b>Save success</b>. Password has been change."); 
	            } else {
	                return $alertify.error('Invalid Old Password.');
	            }
	        });
	    };
	}
]);


