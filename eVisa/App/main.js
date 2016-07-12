var app = angular.module(
	'main',
	[
		, 'ngSanitize'
        , 'ui.router'
        , 'ngCookies'
        , 'ngAlertify'
	]
);


app.controller('app_ctrl',[
    '$scope',
    '$window',
    '$http',
    function ($scope, $window, $http) {
    
    // logoff functionality
    $scope.logoff = function () {
        $http.post("/User/LogOff/").success(function (data, status) {
            console.log(data);
            console.log(status);
            location.reload();
        });
    };

    // declear variable for apply form in dropdown box
    $scope.apply_items = [
        {
            id: 1,
            name: 'Airport',
        },
        {
            id: 2,
            name: 'Embassy',
        },
        {
            id: 3,
            name: 'Forum',
        },
        {
            id: 4,
            name: 'Friend',
        },
        {
            id: 5,
            name: 'Google',
        },
        {
            id: 6,
            name: 'Government Organization',
        },
        {
            id: 7,
            name: 'Guide Book',
        },
        {
            id: 8,
            name: 'MSN',
        },
        {
            id: 9,
            name: 'Newsletter',
        },
        {
            id: 10,
            name: 'Others',
        },
        {
            id: 11,
            name: 'Travel Agent',
        },
        {
            id: 12,
            name: 'Travel Magazine',
        },
        {
            id: 13,
            name: 'Travel Web Page',
        },
        {
            id: 14,
            name: 'Yahoo',
        }

    ];
    // sing up show prompt
    $scope.signUp = function () {
        $window.location.href = '/User/FQSignup';
    };
    
    // functionality for get countries
    $scope.getCountry = function () {
        $http.get("/Country/").success(function (data) {
            $scope.countries = data;
        });
    };

    //print page in payment page
    $scope.printPayment = function () {
        var printContents = document.getElementById("print");
        var popupWin = window.open('', '', '');
        popupWin.document.open();
        popupWin.document.write(
            '<html><head>'
            + '<link href="/Content/print.css" rel="stylesheet">'
            + '</head><body onload="window.print()">'
            + printContents.innerHTML
            + '</body></html>'
        );
        popupWin.document.close();
    };

    /******** Funcationality for change language *************/
    $scope.changeLanguage = function (name, url, code) {
        var data = {
            name: name,
            url: url,
            language: code
        };
        $http.post("/Base/LanguageEven/", JSON.stringify(data)).success(function (data, status) {
            console.log(data);
            console.log(status);
            location.reload();
        });
    };
    $scope.isActive = '1';
    $scope.setActive = function (text) {
        $scope.isActive = text;
    };

}]);
// filter with range of year in the future
app.filter('rangeYearFuture', function () {
    return function (input, max) {
        max = parseInt(max); //Make string input int
        var currentYear = (new Date()).getFullYear();
        for (var i = 0; i <= max; i++) {
            var y = parseInt(currentYear + i);
            input.push(y);            
        }
        return input;
    };
});
// filter with range of year
app.filter('rangeYear', function () {
    return function (input, min) {
        min = parseInt(min); //Make string input int
        var currentYear = (new Date()).getFullYear();
        for (var i = min; i <= currentYear; i++)
            input.push(i);
        return input;
    };
});
// range with number 
app.filter('rangeNumber', function () {
    return function (input, total) {
        total = parseInt(total);
        for (var i = 1; i <= total; i++) {
            //if(i <= 9 ){
                
            //}
            input.push(i);
        }
        return input;
    };
});