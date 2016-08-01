app.controller('payment_ctrl', [
    '$scope',
    '$window',
    'Restful',
    'alertify',
    function ($scope, $window, Restful, $alertify) {
        
        $(window).bind('beforeunload', function () {
            return 'Are you sure you want to leave?';
        });


    }
]);