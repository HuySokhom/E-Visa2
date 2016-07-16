app.controller('payment_ctrl', [
    '$scope',
    '$window',
    'Restful',
    'alertify',
    function ($scope, $window, Restful, $alertify) {
        // start init user information
        Restful.get('/User/Profile/').success(function (data) {
            $scope.Contacts = data;
            //console.log(data);
        });

        $(window).bind('beforeunload', function () {
            return 'Are you sure you want to leave?';
        });


    }
]);