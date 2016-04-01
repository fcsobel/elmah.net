(function () {
	'use strict';

	angular.module('c3o.core').directive('logMessageFilter', logMessageFilter);

	// dependencies
	logMessageFilter.$inject = ['$window', '$cookies', '$timeout', 'LogService', 'usSpinnerService'];

	// Package Options Directive
	function logMessageFilter($window, $cookies, $timeout, logService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: {
				log: '@',
				severity: '@',
				limit: '@',
				span: '@',
				type: '@',
				source: '@'
			},
			templateUrl: '/app/log/directives/MessageFilter.html',
			link: link
		};

		

		// Link Function
		function link(scope, el, attrs) {

			scope.model = { types: [], sources : []};

			// setup query
			scope.query = {
				log: scope.log,
				severity: scope.severity,
				limit: scope.limit,
				span: scope.span,
				type: scope.type,
				source: scope.source,
				start: moment().subtract(7, 'days').startOf('day'), //utc().
				end : moment().endOf('day'), //utc()
			};

			// Search
			scope.Search = function () {

				usSpinnerService.spin('spinner-1');

				//var types = scope.model.types;
				//var sources = scope.model.sources;

				var types = _.filter(scope.model.types, { 'selected': true });
				var sources = _.filter(scope.model.sources, { 'selected': true });
				var users = _.filter(scope.model.users, { 'selected': true });

				types = _.map(types, 'id');
				sources = _.map(sources, 'id');
				users  = _.map(users, 'id');
				//sources = _.map(sources, 'id');

				var start = null;
				var end = null;
				if (scope.query.span === 'All') {				
					if (scope.query.start) { start = scope.query.start.toDate(); }
					if (scope.query.end) { end = scope.query.end.toDate(); }
				}

				logService.search(scope.query.log, scope.query.severity, scope.query.limit, scope.query.span, types, sources, users, start, end)
					.then(function (response) { // sucess
						scope.model = response.model;

						if (response.model.messages.length > 0) {
						    var message = response.model.messages[0];

							// auto select 1st one
							logService.detail(message.id).then(function (response) {
								usSpinnerService.stop('spinner-1');
							});
						}
						else {
							usSpinnerService.stop('spinner-1');
						}
					});
			};
					
			// Load Message Filter 
			scope.init = function () {
				
				//var start = scope.query.start.format('MM/DD/YYYY');
				//var end = scope.query.end.format('MM/DD/YYYY');

				$('#demo').daterangepicker({
						"autoApply": true,
						//"timePicker": true,
						"startDate": scope.query.start.format('MM/DD/YYYY'),
						"endDate": scope.query.end.format('MM/DD/YYYY'),
						"maxDate": moment().format('MM/DD/YYYY')
				}, function (start, end, label) {
					//scope.query.start = start;
					//scope.query.end = end;

					scope.query.start = start.startOf('day');
					scope.query.end = end.endOf('day');
					
					scope.Search();
				});

				scope.Search();
			}

			// initialize
			scope.init();
		}
	}
})();