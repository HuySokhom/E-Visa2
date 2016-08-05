// directive name
app.directive('croppedView', function () {
    return {
        restrict: 'EA',
        templateUrl: '/App/imageCropped/partials/cropped_image.html',
        controller: 'cropped_ctrl'
    };
});

// controller cropped
app.controller(
	'cropped_ctrl', [
	'$scope'
	, '$http'
	, function ($scope, $http) {

	    $scope.myImage = '';
	    $scope.myCroppedImage = '';

	    var handleFileSelect = function (evt) {
	        var file = evt.currentTarget.files[0];
	        var reader = new FileReader();
	        reader.onload = function (evt) {
	            $scope.$apply(function ($scope) {
	                $scope.myImage = evt.target.result;
	            });
	        };
	        reader.readAsDataURL(file);
	    };
	    angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);

	    $scope.crop = function () {
	        $('#cropped_image').modal('hide');
	        var data = { base64String: $scope.myCroppedImage };
	        //console.log(data);
	        $scope.disabled = true;
	        $http({
	            url: '/User/BaseToImage',
	            method: 'POST',
	            data: data,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: false,
	        }).success(function (data) {
	            $scope.disabled = false;
	            if (angular.isDefined($scope.Contact)) {
	                $scope.Contact.Photo = data.image_name;
	            }
	            if (angular.isDefined($scope.Profile)) {
	                $scope.Profile.Photo = data.image_name;
	            }
	            if (angular.isDefined($scope.Child)) {
	                $scope.Child.ChildPhoto = data.image_name;
	            }
	            //console.log(data);
	        });

	    };
	}
]);