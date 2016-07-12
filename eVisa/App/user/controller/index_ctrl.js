app.controller(
	'index_ctrl', [
	'$scope'
    , 'Services'
	, 'Restful'
	, '$window'
	, function ($scope, Services, Restful, $window) {
	    $scope.service = new Services();
	    $scope.sex = "male";
	    var dob = '';
	    var issueDate = '';
	    $scope.delete = true;
	    var expiryDate = '';
	    $scope.init = function () {
	        Restful.get("/User/Profile/").success(function (data) {
	            $scope.contact = data;
	            dob = $scope.service.dateFormat(data.DOB);
	            if (dob) {
	                dob = dob.year + '/' + dob.month + '/' + dob.day;
	            }
	            issueDate = $scope.service.dateFormat(data.PassportIssueDate);
	            if (issueDate) {
	                issueDate = issueDate.year + '/' + issueDate.month + '/' + issueDate.day;
	            }
	            expiryDate = $scope.service.dateFormat(data.PassportExpiryDate);
	            if (expiryDate) {
	                expiryDate = expiryDate.year + '/' + expiryDate.month + '/' + expiryDate.day;
	            }
	        });
	    };
	    $scope.init();

	    $scope.saveApply = function () {
	        var model = {
	            Userid: $scope.contact.UserId,
	            SurName: $scope.contact.SurName,
	            GivenName: $scope.contact.GivenName,
	            CountryOfBirth: $scope.contact.Country,
	            Nationality: $scope.contact.Nationality,
	            Sex: $scope.contact.Sex,
	            DOB: dob,
	            Photo: $scope.contact.Photo,
	            
	            ResidentialAddress: $scope.contact.ResidentialAddress,
	            PassportNo: $scope.contact.PassportNo,
	            PassportCountry: $scope.contact.CountryIssue,
	            PassportIssueDate: issueDate,
	            PassportExpiryDate: expiryDate,
                PrimaryEmail: $scope.contact.PrimaryEmail,
                SecondaryEmail: $scope.contact.SecondaryEmail,
                
                PointOfEntry: $scope.PortOfEntry,
                EntryDate: $scope.year_entry + '/' + $scope.month_entry + '/' + $scope.day_entry,
                TravelMode: $scope.ModOfTravel,
                ArrivalVehicleNo: $scope.ArrivalVehicleNo,
                ArrivalTime: $scope.ArrivalTime,
                VisitAddress: $scope.AddressDuringVisit,
                VisitPerson: $scope.OrganizationPersonsTo,
                
	        };
	        $scope.disabled = true;
	        Restful.save("/Application/Save", model).success(function (data) {
	            $scope.disabled = false;
	            if (data.success) {
	                $('#apply_single_evisa').modal('hide');
	                Materialize.toast("save success.", 4000);	                
	                // redirect to review 
	                $window.location.href = '/Home/Review';
	            } else {
	                $('#apply_single_evisa').modal('hide');
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
