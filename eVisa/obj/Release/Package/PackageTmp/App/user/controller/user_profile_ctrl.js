app.controller(
	'user_profile_ctrl', [
	'$scope'
	, 'Restful'
	, '$stateParams'
	, '$location'

	, function ($scope, Restful, $stateParams, $location) {

		var url = '/User/Profile/';
		$scope.init = function(params){
			Restful.get(url).success(function(data){
			    $scope.Profile = data;
			    var dob = dateFormat(data.DOB)
			    if (dob) {
			        $scope.year = dob.year;
			        $scope.month = dob.month;
			        $scope.day = dob.day;
			    }
			    var issueDate = dateFormat(data.PassportIssueDate);
			    if (issueDate) {
			        $scope.day_issue = issueDate.day;
			        $scope.month_issue = issueDate.month;
			        $scope.year_issue = issueDate.year;
			    }

			    var expiryDate = dateFormat(data.PassportExpiryDate);
			    if (expiryDate) {
			        $scope.day_expiry = expiryDate.day;
			        $scope.month_expiry = expiryDate.month;
			        $scope.year_expiry = expiryDate.year;
			    }
			});
		};
		$scope.init();

        // functionality for format date json from db
		function dateFormat(value) {
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
        //functional for save contact info
		$scope.save = function () {
		    var model = {
		        SurName: $scope.Profile.SurName,
		        GivenName: $scope.Profile.GivenName,
		        PrimaryEmail: $scope.Profile.PrimaryEmail,
		        SecondaryEmail: $scope.Profile.SecondaryEmail,
		        Country: $scope.Profile.Country,
		        PhoneNo: $scope.Profile.PhoneNo,
		        HeardFrom: $scope.Profile.HeardFrom,

		        Photo: $scope.Profile.Photo,
		        Sex: $scope.Profile.Sex,
		        PassportNo: $scope.Profile.PassportNo,
		        DOB: $scope.year + '/' + $scope.month + '/' + $scope.day,
		        CountryIssue: $scope.Profile.CountryIssue,
		        PassportIssueDate: $scope.year_issue + '/' + $scope.month_issue + '/' + $scope.day_issue,
		        PassportExpiryDate: $scope.year_expiry + '/' + $scope.month_expiry + '/' + $scope.day_expiry,
		        Nationality: $scope.Profile.Nationality,
		        ResidentialAddress: $scope.Profile.ResidentialAddress,
		    };
		    //console.log(model); 
		    $scope.process = true;
		    Restful.save("/User/Profile", model).success(function (data) {
		        //console.log(data);
		        $scope.process = false;
		        if (data.success) {
		            $location.path("#/");
		            return Materialize.toast('Save success. Data has been change', 4000);
		        } else {
		            return Materialize.toast('Something went wrong.', 4000);
		        }
		    });
		};
        // upload functionality
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
		                    console.log(response);
		                    $scope.Profile.Photo = '/Uploads/Users/' + response.image;
		                    $(".img-profile").attr('src', $scope.Profile.Photo);
		                    console.log($scope.Profile.Photo);
		                    $("#message span").text(response.message);
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
		    readURL(this);
		});

	}
]);