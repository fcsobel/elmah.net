(function () {
	'use strict';

	angular.module('c3o.core').directive('logMessageList', exampleApiDetail);

	// dependencies
	exampleApiDetail.$inject = ['$window', '$cookies', '$timeout', 'LogService', 'usSpinnerService'];

	// Package Options Directive
	function exampleApiDetail($window, $cookies, $timeout, logService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: {
				log: '@',
				severity: '@',
				limit: '@',
				span: '@'
			},
			templateUrl: '/app/log/directives/MessageList.html',
			link: link
		};


		// Link Function
		function link(scope, el, attrs) {

			scope.model = {};

			scope.messageFilter = function (value, index, array) {
				return logService.filter(value, index, array);
			}

			scope.Select = function (message)
			{
				usSpinnerService.spin('spinner-1');

				// select message
				logService.detail(message.id).then(function (response) {
					usSpinnerService.stop('spinner-1');
				});
			}

			// Load Message List 
			scope.init = function () {

				if (scope.log) {
					logService.search(scope.log, scope.severity, scope.limit, scope.span).then(function (response) { // sucess
						scope.model.model = response.model;
					});
				}
				else {
					scope.model = logService.model();
				}
			}

			// initialize
			scope.init();
		}
	}
})();