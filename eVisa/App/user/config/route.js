app.config([
	'$stateProvider',
	'$urlRouterProvider',
	function($stateProvider, $urlRouterProvider) {
		$stateProvider.
			state('/', {
				url: '/',
				templateUrl: '/App/user/partials/index.html',
				controller: 'index_ctrl'
			})
			.state('/Profile', {
				url: '/Profile',
				templateUrl: '/App/user/partials/user_profile.html',
				controller: 'user_profile_ctrl'
			})
            .state('/Change', {
                url: '/Change',
                templateUrl: '/App/user/partials/user_change.html',
                controller: 'user_change_ctrl'
            })
            .state('/History', {
                url: '/History',
                templateUrl: '/App/user/partials/user_history.html',
                controller: 'user_history_ctrl'
            })
            .state('/Application', {
                url: '/Application',
                templateUrl: '/App/user/partials/user_application.html',
                controller: 'user_application_ctrl'
            })
		;
		$urlRouterProvider.otherwise('/');
	}
]);
