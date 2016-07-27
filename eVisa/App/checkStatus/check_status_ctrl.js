app.controller('check_status_ctrl', [
    '$scope',
    '$window',
    'Restful',
    'alertify',
    function ($scope, $window, Restful, $alertify) {
        $scope.success = true;
        $scope.check = function () {
            var data = {
                ReferenceNo: $scope.ReferenceNo,
                PrimaryEmail: $scope.PrimaryEmail
            };
            $scope.process = true;
            Restful.save("/CheckStatus/Check", data).success(function (data) {
                $scope.process = false;
                console.log(data);
                if (data.success) {
                    $scope.data = data.data[0];
                    $scope.success = false;
                    console.log($scope.data.ApplicationDetail);
                    $alertify.success("<b>Success: </b> Your ReferenceNo and PrimaryEmail Match.");
                    // redirect to ApplicationStatus
                    //return $window.location.href = '/CheckStatus/Status';
                } else {
                    return $alertify.error("<b>Error: </b> Invalid ReferenceNo Or PrimaryEmail.");
                }
            });
        };

        $scope.initStatus = function (data) {
            Restful.get("/CheckStatus/Check", data).success(function (data) {
                console.log(data);
            });
        };

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