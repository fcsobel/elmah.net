(function () {
	'use strict';

	angular.module('elmah.net.logger').directive('logLogger', logLogger);

	// dependencies
	logLogger.$inject = ['$window', '$cookies', '$timeout', 'SiteService', 'usSpinnerService'];

	// Package Options Directive
	function logLogger($window, $cookies, $timeout, siteService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: {},
			templateUrl: '/app/log/directives/Logger.html',
			link: link
		};
		
		// Link Function
		function link(scope, el, attrs) {

		    scope.context = {};

		    scope.Create = function () {
		        usSpinnerService.spin('spinner-1');

                // create site
		        siteService.create()
					.then(function (response) { // sucess
					    //console.log(response);
                        
					    scope.context = response;
					    usSpinnerService.stop('spinner-1');
					});
		    };

		    scope.Check = function () {
		        usSpinnerService.spin('spinner-1');
		        siteService.check()
					.then(function (response) { // sucess

					    //console.log(response);

					    scope.context = response;
					    usSpinnerService.stop('spinner-1');
					});
		    };

		    scope.Check();

		}
	}

})();