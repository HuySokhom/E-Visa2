app.controller('check_status_ctrl', [
    '$scope',
    '$window',
    'Restful',
    'alertify',
    function ($scope, $window, Restful, $alertify) {

        $scope.check = function () {
            var data = {
                ReferenceNo: $scope.ReferenceNo,
                PrimaryEmail: $scope.PrimaryEmail
            };
            $scope.process = true;
            Restful.save("/CheckStatus/Check", data).success(function (data) {
                $scope.process = false;
                if (data.success) {
                    $alertify.success("<b>Success: </b> Your ReferenceNo and PrimaryEmail Match.");
                    // redirect to ApplicationStatus
                    return $window.location.href = '/Home/ApplicationStatus';
                } else {
                    return $alertify.error("<b>Error: </b> Invalid ReferenceNo Or PrimaryEmail.");
                }
            });
        };

    }
]);