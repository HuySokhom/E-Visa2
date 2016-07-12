app.controller(
	'application_ctrl', [
	'$scope'
	, 'Restful'
	, '$window'
	, function ($scope, Restful, $window) {
	    $scope.sex = "male";
	    var dob = '';
	    var issueDate = '';
	    $scope.delete = true;
	    var expiryDate = '';
	    $scope.init = function () {
	        Restful.get("/Apply/GetContact").success(function (data) {
	            $scope.Contact = data.Data[0];
	            $scope.Contact.CountryOfBirth = data.Data[0].Country;
	            $scope.Contact.Nationality = data.Data[0].Country;
	            console.log(data);
	        });
	    };
	    $scope.init();

	    $scope.save = function () {
	        var model = {
	            SurName: $scope.Contact.SurName,
	            GivenName: $scope.Contact.GivenName,
	            CountryOfBirth: $scope.Contact.CountryOfBirth,
	            Nationality: $scope.Contact.Nationality,
	            Sex: $scope.Contact.Sex,
	            DOB: $scope.year + '/' + $scope.month + '/' + $scope.day,
	            Photo: $scope.Contact.Photo,
	            Type: $scope.Contact.Type,
	            ResidentialAddress: $scope.Contact.ResidentialAddress,
	            PassportNo: $scope.Contact.PassportNo,
	            PassportCountry: $scope.Contact.PassportCountry,
	            PassportIssueDate: $scope.year_issue + '/' + $scope.month_issue + '/' + $scope.day_issue,
	            PassportExpiryDate: $scope.year_expiry + '/' + $scope.month_expiry + '/' + $scope.day_expiry,
	            PointOfEntry: $scope.Contact.PointOfEntry,
	            EntryDate: $scope.year_entry + '/' + $scope.month_entry + '/' + $scope.day_entry,
	            TravelMode: $scope.Contact.TravelMode,
	            ArrivalVehicleNo: $scope.Contact.ArrivalVehicleNo,
	            ArrivalTime: $scope.Contact.ArrivalTime,
	            VisitAddress: $scope.Contact.VisitAddress,
	            VisitPerson: $scope.Contact.VisitPerson,

	        };
	        $scope.disabled = true;
	        Restful.save("/Application/Save", model).success(function (data) {
	            $scope.disabled = false;
	            if (data.success) {
	                Materialize.toast("save success.", 4000);	                
	                // redirect to review 
	                $window.location.href = '/Home/Review';
	            } else {
	                Materialize.toast(
                        "Your Application has reach to the maximum of rang. We allow you to apply application only 9 application.",
                        4000
                    );
                    // redirect to review 
	                $window.location.href = '/Home/Review';
	            }
	        });
	        
	    };
	    

	}
]);
