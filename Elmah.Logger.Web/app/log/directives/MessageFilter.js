(function () {
	'use strict';

	angular.module('elmah.net.logger').directive('logMessageFilter', logMessageFilter);

	// dependencies
	logMessageFilter.$inject = ['$window', '$cookies', '$timeout', '$rootScope', 'LogService', 'usSpinnerService'];

	// Package Options Directive
	function logMessageFilter($window, $cookies, $timeout, $rootScope, logService, usSpinnerService) {

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

			scope.model = { types: [], sources: [], users: [], logs: [], applications: [], severities: [] };

			// setup query
			scope.model.query = {
				log: scope.log,
				severity: scope.severity,
				limit: scope.limit,
				span: scope.span,
				type: scope.type,
				source: scope.source,
				//startMoment: moment().subtract(7, 'days').startOf('day'), //utc().
				//endMoment: moment().endOf('day'), //utc()
			};

			scope.filter = {};

			scope.Clear = function () {
				scope.filter = {};
				scope.model.query = {
					limit: 100,
					span: 60 * 24,
					//startMoment: moment().subtract(7, 'days').startOf('day'), //utc()
					//endMoment: moment().endOf('day'), //utc()
				};
			}

			scope.Showhide = function (list) {
				_.forEach(list, function (value, key) {
					value.visible = !value.visible;
				});
			}

			scope.SetFilter = function (item) {
				// set current filter
				scope.filter = item;

				scope.model.query = item.query;

				//scope.query.name = item.name;

                if (scope.model.query.span == 0) {
                    if (item.query.start && item.query.end) {
                        scope.model.query.startMoment = moment(item.query.start);
                        scope.model.query.endMoment = moment(item.query.end);

                        var drp = $('#demo').data('daterangepicker');
                        drp.setStartDate(scope.model.query.startMoment.format('MM/DD/YYYY'));
                        drp.setEndDate(scope.model.query.endMoment.format('MM/DD/YYYY'));
                    }
                    else
                    {
                        $('#demo').val('');
                    }
				}

				scope.Find(scope.filter.name);
			}


			scope.RefreshQuery = function () {
				// find all selected objects
				var types = _.filter(scope.model.types, { 'selected': true });
				var sources = _.filter(scope.model.sources, { 'selected': true });
				var users = _.filter(scope.model.users, { 'selected': true });
				var logs = _.filter(scope.model.logs, { 'selected': true });
				var applications = _.filter(scope.model.applications, { 'selected': true });
				var severities = _.filter(scope.model.severities, { 'selected': true });

				// get ids for query
				scope.model.query.types = _.map(types, 'id');
				scope.model.query.sources = _.map(sources, 'id');
				scope.model.query.users = _.map(users, 'id');
				scope.model.query.logs = _.map(logs, 'id');
				scope.model.query.applications = _.map(applications, 'id');
				scope.model.query.severities = _.map(severities, 'name');
			}

			scope.Delete = function (item) {

				usSpinnerService.spin('spinner-1');

				// clear current filter selection
				scope.filter = {};

				// get query data
				scope.RefreshQuery();

				// find by name
				logService.deleteByName(item.name)
					.then(function (response) { // sucess
						scope.model = response.model;
						usSpinnerService.stop('spinner-1');

						scope.Refresh();
					});
			}

			scope.Find = function (name) {

				usSpinnerService.spin('spinner-1');

				// get query data
				scope.RefreshQuery();


				// find by name
				logService.find(name)
					.then(function (response) { // sucess
						scope.model = response.model;
						usSpinnerService.stop('spinner-1');

						scope.Refresh();
					});
			}

			scope.SearchAndUpdate = function () {

				usSpinnerService.spin('spinner-1');

				// get query data
				scope.RefreshQuery();

				// Get dates 
				var startDate = null;
				var endDate = null;
				if (scope.model.query.span === 0) {
					if (scope.model.query.startMoment && scope.model.query.endMoment) {
						// gets the native Date object that Moment.js wraps
						startDate = scope.model.query.startMoment.toDate();
						endDate = scope.model.query.endMoment.toDate();
					}
				}

				logService.searchAndUpdate(scope.filter.name, scope.filter,
                    scope.model.query.limit, scope.model.query.span, scope.model.query.logs, scope.model.query.applications, scope.model.query.severities, scope.model.query.types, scope.model.query.sources, scope.model.query.users, startDate, endDate, scope.model.query.search)
					.then(function (response) { // sucess
						scope.model = response.model;
						usSpinnerService.stop('spinner-1');
						scope.Refresh();
					});
			}


            // change span dropdown event
            scope.ChangeSpan = function () {

                scope.CheckDates();

                if (scope.model.query.span > 0) {
                    scope.Search();
                }
            };
            
            scope.CheckDates = function () {
                // refresh moment dates from uimodel

                if (scope.model.query.span > 0 && scope.model.query.span < 1440000) {
                    var days = scope.model.query.span / (60 * 24);
                    scope.model.query.startMoment = moment().subtract(days, 'days').startOf('day');
                    scope.model.query.endMoment = moment().endOf('day');
                }

                if (scope.model.query.span == 0) {
                    if (scope.model.query.start && scope.model.query.end) {
                        scope.model.query.startMoment = moment(scope.model.query.start);
                        scope.model.query.endMoment = moment(scope.model.query.end);
                    }
                }
            }


			// Search
			scope.Search = function () {

				usSpinnerService.spin('spinner-1');

				// get query data
				scope.RefreshQuery();

				// Get dates 
				var startDate = null;
				var endDate = null;

                if (scope.model.query.startMoment && scope.model.query.endMoment) {
                    // gets the native Date object that Moment.js wraps
                    startDate = scope.model.query.startMoment.toDate();
                    endDate = scope.model.query.endMoment.toDate();
                }

			    logService.search(scope.model.query.limit, scope.model.query.span, scope.model.query.logs, scope.model.query.applications, scope.model.query.severities, scope.model.query.types, scope.model.query.sources, scope.model.query.users, startDate, endDate, scope.model.query.search)
					.then(function (response) { // sucess
						scope.model = response.model;

						//// refresh UI date from response
						//if (scope.model.query.span === 0) {
						//	if (scope.model.query.start && scope.model.query.end) {
						//		scope.model.query.startMoment = moment(scope.model.query.start);
						//		scope.model.query.endMoment = moment(scope.model.query.end);
						//	}
						//}

						usSpinnerService.stop('spinner-1');

						scope.Refresh();
					});
			};

			// Load Intital values
			scope.Init = function () {

				usSpinnerService.spin('spinner-1');

				logService.init()
					.then(function (response) { // sucess
						scope.model = response.model;

						usSpinnerService.stop('spinner-1');

						// auto select matching query items
						scope.Refresh();
					});
			};



			// Update UI based on response
			scope.Refresh = function () {

                scope.CheckDates();

				if (scope.model.query.startMoment && scope.model.query.endMoment) {
					var drp = $('#demo').data('daterangepicker');
					//scope.model.query.start = moment(scope.model.query.start);
					//scope.model.query.end = moment(scope.model.query.end);
					drp.setStartDate(scope.model.query.startMoment.format('MM/DD/YYYY'));
					drp.setEndDate(scope.model.query.endMoment.format('MM/DD/YYYY'));
				}

				scope.Select(scope.model.query.types, scope.model.types);
				scope.Select(scope.model.query.sources, scope.model.sources);
				scope.Select(scope.model.query.users, scope.model.users);
				scope.Select(scope.model.query.logs, scope.model.logs);
				scope.Select(scope.model.query.applications, scope.model.applications);
				scope.Select(scope.model.query.severities, scope.model.severities);

				if (scope.model.messages.length > 0) {
					// get first message
					var message = scope.model.messages[0];

					usSpinnerService.spin('spinner-1');

					// auto select 1st message
					logService.detail(message.id).then(function (response) {
						usSpinnerService.stop('spinner-1');
					});
				}
			}

			scope.Select = function (idList, objList) {
				_.forEach(idList, function (value) {
					var obj = _.find(objList, { 'id': value });
					if (obj) { obj.selected = true; }
				});
			}

			scope.SelectName = function (idList, objList) {
				_.forEach(idList, function (value) {
					var obj = _.find(objList, { 'name': value });
					if (obj) { obj.selected = true; }
				});
			}

			scope.Filter = function ()	{
				//broadcast to refresh ui, chart...etc
				$rootScope.$broadcast("refreshFilter");
			}

			// Load Message Filter 
			scope.init = function () {

				//var start = scope.query.start.format('MM/DD/YYYY');
				//var end = scope.query.end.format('MM/DD/YYYY');

                if (scope.model.query.startMoment && scope.model.query.endMoment) {
                    $('#demo').daterangepicker({
                        "autoApply": true,
                        //"timePicker": true,
                        "startDate": scope.model.query.startMoment.format('MM/DD/YYYY'),
                        "endDate": scope.model.query.endMoment.format('MM/DD/YYYY'),
                        "maxDate": moment().format('MM/DD/YYYY')
                    }, function (start, end, label) {
                        //scope.query.start = start;
                        //scope.query.end = end;
                        scope.model.query.span = 0;
                        scope.model.query.startMoment = moment(start).startOf('day');
                        scope.model.query.endMoment = moment(end).endOf('day');
                        scope.Search();
                    });
                }
                else {
                    $('#demo').daterangepicker({
                        "autoApply": true,
                        //"timePicker": true,
                        //"startDate": scope.model.query.startMoment.format('MM/DD/YYYY'),
                        //"endDate": scope.model.query.endMoment.format('MM/DD/YYYY'),
                        "maxDate": moment().format('MM/DD/YYYY')
                    }, function (start, end, label) {
                        //scope.query.start = start;
                        //scope.query.end = end;
                        scope.model.query.span = 0;
                        scope.model.query.startMoment = moment(start).startOf('day');
                        scope.model.query.endMoment = moment(end).endOf('day');
                        scope.Search();
                    });
                }

				if (scope.log) {
					scope.Find(scope.log);
				}
				else {
					scope.Search();
				}
			}

			// initialize
			scope.init();
		}
	}
})();