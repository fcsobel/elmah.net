(function () {
	'use strict';

	angular.module('c3o.logger').directive('logMessageDetail', logMessageDetail);

	// dependencies
	logMessageDetail.$inject = ['$window', '$cookies', '$timeout', 'LogService', 'usSpinnerService'];

	// Package Options Directive
	function logMessageDetail($window, $cookies, $timeout, logService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: {
				id: '@'				
			},
			templateUrl: '/app/log/directives/MessageDetail.html',
			link: link
		};
		
		// Link Function
		function link(scope, el, attrs) {

			scope.context = {};

			scope.tab = 'code';

			scope.Delete = function () {
			    usSpinnerService.spin('spinner-1');
			    logService.deleteMessage(scope.context.model.message)
					.then(function (response) { // sucess
					    usSpinnerService.stop('spinner-1');
					});
			}

			// Load Message Detail 
			scope.init = function () {

				if (scope.id) {
					logService.detail(scope.id).then(function (response) { // sucess
						//scope.model = response.model;
						//scope.context = response;						
						scope.context = response;
					});
				}
				else {
					scope.context = logService.model();
				}
			}

			// initialize
			scope.init();
		}
	}
})();