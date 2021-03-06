app.controller(
	'apply_ctrl', [
	'$scope'
	, 'Restful'
	, '$window'
    , 'alertify'
	, function ($scope, Restful, $window, $alertify) {

	    $scope.init = function () {
	        Restful.get("/Apply/GetContact").success(function (data) {
	            //console.log(data);
	            if(data.Data.length > 0){
	                $scope.id = data.Data[0].id;
	                $scope.Surname = data.Data[0].SurName;
	                $scope.GivenName = data.Data[0].GivenName;

	                $scope.Country = data.Data[0].Country;
	                $scope.heardFrom = data.Data[0].HeardFrom;
	                $scope.PhoneNumber = data.Data[0].PhoneNo;
	                $scope.PrimaryEmail = data.Data[0].PrimaryEmail;
	                $scope.RetypePrimaryEmail = data.Data[0].PrimaryEmail;
	                $scope.SecondaryEmail = data.Data[0].SecondaryEmail;
	            }
	            
	        });
	    };
	    $scope.init();


	    $scope.save = function () {
	        var data = {
	            SurName: $scope.Surname,
	            GivenName: $scope.GivenName,
	            Country: $scope.Country,
	            HeardFrom: $scope.heardFrom,
	            PhoneNo: $scope.PhoneNumber,
	            PrimaryEmail: $scope.PrimaryEmail,
	            SecondaryEmail: $scope.SecondaryEmail,
                id: $scope.id
	        };
	        if ($scope.PrimaryEmail != $scope.RetypePrimaryEmail) {
	            //$alertify.logPosition("top right");
	            return $alertify.log("<b>Warning: </b>Retype Primary Email Not Match.");
	        }
	        $scope.disabled = true;
	        Restful.save("/Apply/SaveContact", data).success(function (data) {
	            //$scope.disabled = false;
	            if (data.success) {
	                $alertify.success("<b>Complete: </b> Save Success.");
	                // redirect to Application
	                $window.location.href = '/Application';
	            } else {
	                return $alertify.log("<b>Warning: </b> Something went wrong. Please contact admin.");
	            }
	        });
	        
	    };
	    

	}
]);
