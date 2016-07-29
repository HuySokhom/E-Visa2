app.controller('search_reference_ctrl', [
    '$scope',
    '$window',
    'Restful',
    'alertify',
    function ($scope, $window, Restful, $alertify) {
        $scope.success = true;
        $scope.search = function () {
            var data = {
                PassportNo: $scope.PassportNo,
                PrimaryEmail: $scope.PrimaryEmail
            };
            $scope.process = true;
            Restful.save("/CheckStatus/SearchRefernceNo", data).success(function (data) {
                $scope.process = false;
                console.log(data);
                if (data.success) {
                    $scope.data = data.data;
                    $scope.success = false;
                    $alertify.success("<b>Successful</b>.");
                } else {
                    return $alertify.error("<b>Error: </b> Invalid Passport Or PrimaryEmail.");
                }
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