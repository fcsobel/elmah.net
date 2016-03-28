(function () {
	'use strict';

	angular.module('c3o.core').directive('logMessageDetail', logMessageDetail);

	// dependencies
	logMessageDetail.$inject = ['$window', '$cookies', '$timeout', 'LogService'];

	// Package Options Directive
	function logMessageDetail($window, $cookies, $timeout, logService) {

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