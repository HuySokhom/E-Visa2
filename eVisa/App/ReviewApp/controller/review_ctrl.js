app.controller(
	'review_ctrl', [
	'$scope'
	, 'Restful'
	, '$stateParams'
	, '$window'
    , 'alertify'
	, function ($scope, Restful, $stateParams, $window, $alertify) {
	    $scope.Child = {};
	    $scope.Contact = {};
		var url = '/Application/';
		$scope.init = function (params) {
            //************* get user profile info *******************//
		    Restful.get('/User/Profile/').success(function (data) {
		        $scope.Contacts = data;
		        if (data.success) {
		            var dob = $scope.dateFormat(data.DOB)
		            if (dob) {
		                $scope.year = dob.year;
		                $scope.month = dob.month;
		                $scope.day = dob.day;
		            }
		            var issueDate = $scope.dateFormat(data.PassportIssueDate);
		            if (issueDate) {
		                $scope.day_issue = issueDate.day;
		                $scope.month_issue = issueDate.month;
		                $scope.year_issue = issueDate.year;
		            }

		            var expiryDate = $scope.dateFormat(data.PassportExpiryDate);
		            if (expiryDate) {
		                $scope.day_expiry = expiryDate.day;
		                $scope.month_expiry = expiryDate.month;
		                $scope.year_expiry = expiryDate.year;
		            }
		            $(".apply_popup").modal('show');
		        }
		        //console.log($scope.Contacts);
		    });
		    //************** get application info ***************//
		    Restful.get(url).success(function (data) {
		        $scope.applicationReviews = '';
		        if (data.success) {
		            $scope.applicationReviews = data.Data;
		            console.log($scope.applicationReviews);
		        }
		    });
		};
		$scope.init();

        //************* funcationality for calculate age  **********************//
		$scope.calculateAge = function calculateAge(birthday) {
		    birthday = new Date(birthday);
		    // date of birth
		    var ageDifMs = Date.now() - birthday.getTime();
		    var ageDate = new Date(ageDifMs); // miliseconds from epoch		    
		    return Math.abs(ageDate.getUTCFullYear() - 1970);
		}

		$scope.dateFormat = function (value) {
		    if(value){
		        var pattern = /Date\(([^)]+)\)/;
		        var results = pattern.exec(value);
		        var dt = new Date(parseFloat(results[1]));
		        var date = {
		            day: dt.getDate(),
		            month: dt.getMonth() + 1,
		            year: dt.getFullYear()
		        };
		        return date;
		    }
		}
        // funcationality convert json Date for show date format 
		$scope.dateFormatHTML = function (value) {
		    if (value) {
		        var pattern = /Date\(([^)]+)\)/;
		        var results = pattern.exec(value);
		        if (results) {
		            var dt = new Date(parseFloat(results[1]));
		            var monthNames = [
                        "January", "February", "March", "April", "May", "June",
                        "July", "August", "September", "October", "November", "December"
		            ];
		            var date = dt.getDate() + '-' + monthNames[parseFloat(dt.getMonth())] + '-' + dt.getFullYear()
		            return date;
		        } else {
		            return value;
		        }
		    }
		}

		$scope.save = function () {
		    console.log($scope.id);
		    var model = {
		        SurName: $scope.Contact.SurName,
		        GivenName: $scope.Contact.GivenName,
		        CountryOfBirth: $scope.Contact.CountryOfBirth,
		        Nationality: $scope.Contact.Nationality,
		        Sex: $scope.Contact.Sex,
		        DOB:  $scope.year + '/' + $scope.month + '/' + $scope.day,
		        Photo: $scope.Contact.Photo,
		        Type: $scope.Contact.Type,
		        ResidentialAddress: $scope.Contact.ResidentialAddress,
		        PassportNo: $scope.Contact.PassportNo,
		        PassportCountry: $scope.Contact.PassportCountry,
		        PassportIssueDate: $scope.year_issue + '/' + $scope.month_issue + '/' + $scope.day_issue,
		        PassportExpiryDate: $scope.year_expiry + '/' + $scope.month_expiry + '/' + $scope.day_expiry,
		        PrimaryEmail: $scope.Contact.PrimaryEmail,
		        SecondaryEmail: $scope.Contact.SecondaryEmail,
                id: $scope.id,
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
		        $(".apply_popup").modal('hide');
		        $scope.init();
		        if (data.success) {
		            $alertify.logPosition("top right");
		            return $alertify.success("<b>Complete: </b> Save success.");
		        } else {
		            $alertify.logPosition("top right");
		            return $alertify.error("<b>Warning: </b> Sorry you can apply only 9 application.");
		        }
		    });
		};

	    //***************************************************//
	    //******Delete Application Funcationality ***********//
	    //***************************************************//
		$scope.remove = function ($index, id) {
		    console.log(id);
		    $alertify.okBtn("Ok")
				.cancelBtn("Cancel")
				.confirm("<b>Waring: </b>" +
					"Are you sure want to delete this application?", function (ev) {
						// The click event is in the
						// event variable, so you can use
						// it here.
						ev.preventDefault();
						Restful.save("/Application/Delete/" + id).success(function (data) {
						    $alertify.logPosition("top right");
						    $alertify.error("<b>Complete: </b> Delete Success.");
						    $scope.applicationReviews.splice($index, 1);
						});
					}, function (ev) {
						// The click event is in the
						// event variable, so you can use
						// it here.
						ev.preventDefault();
					});
		    
		};

		$scope.editReview = function (params) {
		    $scope.Contact = angular.copy(params);
		    $scope.id = $scope.Contact.id;
		    $(".apply_popup").modal('show');
		    var dob = $scope.dateFormat(params.DOB); console.log(dob);
		    var entry_date = $scope.dateFormat(params.EntryDate);
		    var issue_date = $scope.dateFormat(params.PassportIssueDate);
		    var expire_date = $scope.dateFormat(params.PassportExpiryDate);
		    $scope.day = dob.day;
		    $scope.day_entry = entry_date.day;
		    $scope.day_expiry = expire_date.day;
		    $scope.day_issue = issue_date.day;
		    $scope.month = dob.month;
		    $scope.month_entry = entry_date.month;
		    $scope.month_expiry = expire_date.month;
		    $scope.month_issue = issue_date.month;
		    $scope.year = dob.year;
		    $scope.year_entry = entry_date.year;
		    $scope.year_expiry = expire_date.year;
		    $scope.year_issue = issue_date.year;
		};

		$scope.delete = true;
		
		$scope.clear = function () {
		    $scope.Contact = {};
            // set default value sex male 
		    $scope.Contact.Sex = "Male";
		    $scope.id = '';
		    $scope.day = '';
		    $scope.day_entry = '';
		    $scope.day_expiry = '';
		    $scope.day_issue = '';
		    $scope.month = '';
		    $scope.month_entry = '';
		    $scope.month_expiry = '';
		    $scope.month_issue = '';
		    $scope.year = '';
		    $scope.year_entry = '';
		    $scope.year_expiry = '';
		    $scope.year_issue = '';
		    $("#message span").text("");
		};
		
	    //************* functionality add child popup ****************//
		$scope.addChild = function (params) {
		    $scope.Child = {};
		    $scope.Child.ChildSex = "Male";
		    $('#addChild').modal('show');
		    $scope.ReferenceNo = params.ReferenceNo;
		    $scope.Line1 = params.Line;
		    $scope.day = '';
		    $scope.month = '';
		    $scope.year = '';
		    $("#message span").text("");
		    $scope.childId = '';
		};

	    //******** Edit Funcationality for Change children ********//
		$scope.editChild = function (params) {
		    $scope.childId = params.id;
		    $scope.Child = params;
		    //$scope.Child.ChildGivenName = params.ChildGivenName;
		    //$scope.Child.ChildSurName = params.ChildSurName;
		    //$scope.Child.ChildSex = params.ChildSex;
		    //$scope.Child.ChildPhoto = params.ChildPhoto;
		    var dob = $scope.dateFormat(params.ChildDob);
		    console.log(params);
		    $scope.year = dob.year;
		    $scope.day = dob.day;
		    $scope.month = dob.month;
		    $('#addChild').modal('show');
		    $("#message span").text("");
		};

        //*********** funcationality save child ***************//
		$scope.saveChild = function () {
		    var model = {
		        ChildSurName: $scope.Child.ChildSurName,
		        ChildGivenName: $scope.Child.ChildGivenName,
		        ChildSex: $scope.Child.ChildSex,
		        ChildDob: $scope.year + '/' + $scope.month + '/' + $scope.day,
		        ChildPhoto: $scope.Child.ChildPhoto,
		        ReferenceNo: $scope.ReferenceNo,
		        Line1: $scope.Line1,
		        id: $scope.childId
		    };
		    $scope.disabled = true;
		    Restful.save("/Children/Save", model).success(function (data) {
		        $scope.disabled = false;
		        $("#addChild").modal('hide');
		        $scope.init();
		        if (data.success) {
		            $alertify.logPosition("top right");
		            return $alertify.success("<b>Complete: </b> Save success.");
		        } else {
		            $alertify.logPosition("top right");
		            return $alertify.error("<b>Warning: </b> Sorry you can apply your child only 3 application.");
		        }
		    });
		};
	    
        // remove child ************************/
		$scope.deleteChild = function () {
		    console.log($scope.Child);
		    $alertify.okBtn("Ok")
				.cancelBtn("Cancel")
				.confirm("<b>Waring: </b>" +
					"Are you sure want to delete this child?", function (ev) {
					    // The click event is in the
					    // event variable, so you can use
					    // it here.
					    ev.preventDefault();
					    var model = {
					        id: $scope.Child.id, 
					        ReferenceNo: $scope.Child.ReferenceNo,
					        Line1: $scope.Child.Line1
					    };
					    
					    Restful.save("/Children/Delete/", model).success(function (data) {
					        
					        $("#addChild").modal('hide');
					        $alertify.logPosition("top right");
					        $alertify.error("<b>Complete: </b> Delete Success.");
					        $scope.init();
					    });
					}, function (ev) {
					    // The click event is in the
					    // event variable, so you can use
					    // it here.
					    ev.preventDefault();
					});

		};

	    //*** upload functionality *****//
	    //$scope.Profile.Photo = '';
		function readURL(input) {
		    if (input.files && input.files[0]) {
		        var data = new FormData();
		        var files = $("#upload_image").get(0).files;
		        if (files.length > 0) {
		            data.append("MyImages", files[0]);
		        }

		        $.ajax({
		            url: "/User/UploadImage",
		            type: "POST",
		            processData: false,
		            contentType: false,
		            data: data,
		            success: function (response) {
		                if (response.success) {
		                    $scope.Contact.Photo = '/Uploads/Users/' + response.image;
		                    $scope.Child.ChildPhoto = '/Uploads/Users/' + response.image;
		                    $("#message span").text(response.message);
		                    $("#img").attr('src', '/Uploads/Users/' + response.image);
		                } else {
		                    $("#message span").text(response.message);
		                }
		            },
		            error: function (er) {
		                $("#message span").text("Invalid or file error");
		            }
		        });
		    }
		}

		$("#upload_image").change(function () {
		    readURL(this); console.log(this);
		});

	    /**************************************
        ***  functionality for save next 
        ***  step payment conditional 
        ***************************************/
		$scope.saveAppPayment = function () {
		    var model = {
		        ReferenceNo: $scope.applicationReviews[0].ReferenceNo,
		    }; console.log(model);
		    $scope.disabled = true;
		    Restful.save("/Payment/SaveAppPayment", model).success(function (data) {
		        $scope.disabled = false;
		        console.log(data);
		        if (data.success) {
		            Materialize.toast("Save Success.", 4000);
		            // redirect to review 
		            $window.location.href = '/Home/PaymentCon';
		        }
		    });
		};

	}
]);