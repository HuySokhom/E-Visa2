app.controller(
	'user_history_ctrl', [
	'$scope'
	, 'Restful'
	, '$location'
	, function ($scope, Restful, $location) {

	    $scope.init = function () {
	        Restful.get("/Application/History/").success(function (data) {
	            $scope.history = data.data;
	            //console.log(data);
	        });
	    };
	    $scope.init();

	    // functionality for format date json from db
	    $scope.dateFormat = function (value) {
	        if (value) {
	            var pattern = /Date\(([^)]+)\)/;
	            var results = pattern.exec(value);
	            var dt = new Date(parseFloat(results[1]));
	            return dt;
	        }
	    }

	}
]);
