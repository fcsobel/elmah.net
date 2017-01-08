(function () {
	'use strict';

	angular.module('c3o.core').directive('logMessageChart', exampleApiDetail);

	// dependencies
	exampleApiDetail.$inject = ['$window', '$cookies', '$timeout', 'LogService', 'usSpinnerService'];

	// Package Options Directive
	function exampleApiDetail($window, $cookies, $timeout, logService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: { },
			templateUrl: '/app/log/directives/MessageChart.html?v=1',
			link: link
		};


		// Link Function
		function link(scope, el, attrs) {
			scope.hasData = false;
			scope.model = {};
			// Load Message Chart 
			scope.init = function () {
		    	scope.model = logService.model();
			}	

			// initialize
			scope.init();

			//http://www.chartjs.org/docs/#scales-linear-scale
			scope.chartOptions = {

				responsive: true,
				spanGaps: false,
				fullWidth: true,
				maintainAspectRatio: false,
				scales: {
					xAxes: [{
						type: 'time',
						display: true,
						//minUnit: 'day',
						//round: 'day',
						time: {							
							//min: scope.newDate(-80),
							//max: scope.newDate(-0),
							//unitStepSize: 1,
							//round: 'day',
							minUnit: 'day',							
							displayFormats: {
								'millisecond': 'SSS [ms]',
								'second': 'h:mm:ss a',
								'minute': 'h:mm:ss a',
								'hour': 'MMM D, hA',
								'day': 'MMM DD',
								'week': 'MMM DD YY',
								'month': 'MMM YY',
								'quarter': '[Q]Q - YY',
								'year': 'YYYY',
							}
						}
					}, ],
					yAxes: [{ display: false }, ]
				}
			};

						
			//scope.$watch('model.model.typeCount', function (newValue, oldValue, scope) {
			//	scope.RefreshChart();
			//});
					
			scope.$watch('model.model.types', function (newValue, oldValue, scope) {
				scope.RefreshChart();
			});

			//$rootScope.$broadcast("refreshFilter");

			scope.$on("refreshFilter", function () {
				scope.RefreshChart();
			});


			//scope.$watch('messageFilter()', function (newValue, oldValue, scope) {
			//	scope.RefreshChart();
			//});


			//scope.GetData = function () {
			//	scope.hasData = false;
			//	var data = [];
			//	var selected = scope.model.model.types;
	
			//	// get selected types
			//	selected = _.filter(selected, { 'selected': true });
			//	if (selected.length <= 0) { selected = scope.model.model.types };
							
			//	_.forEach(selected, function (value, key) {
			//		data.push(value.counts);
			//		if (value.counts && value.counts.length > 0) { scope.hasData = true; }

			//		scope.series.push(value.name);
			//	});

			//	return data;
			//};

			//scope.GetSeries = function () {
			//	var series = [];
			//	var selected = scope.model.model.types;

			//	// get selected types
			//	selected = _.filter(selected, { 'selected': true });
			//	if (selected.length <= 0) { selected = scope.model.model.types };

			//	_.forEach(selected, function (value, key) {
			//		series.push(value.name);
			//	});

			//	return series;
			//};


			scope.RefreshChart = function () {

				//console.log('RefreshChart');

				//var labels = [];
				//var data4 = [];
				//var data3 = [];
				//var list = scope.model.model.typeCount;

				//_.forEach(list, function (value, key) {					
				//	//labels.push(moment(new Date(value.name)));
				//	///data1.push(value.count);
				//	//data2.push(value.count * 2);
				//	data3.push({ x: moment(new Date(value.name)), y: value.count })
				//	data4.push({ x: moment(new Date(value.name)), y: value.count * 2 })
				//	scope.hasData = true;
				//});

				// Set Labels / dates
				//scope.labels = labels;
				// Set Data / counts
				//scope.data = [];
				scope.data = [];
				//scope.labels = [];
				scope.series = [];
				//scope.datasets = [];
				//scope.data.push(data3);

				// get selected types
				var selected = _.filter(scope.model.model.types, { 'selected': true });
				if (selected.length <= 0) { selected = scope.model.model.types };
				
				scope.colors = [];

				scope.hasData = false;

				_.forEach(selected, function (value, key) {
				
					var color = '#97BBCD';
					if (value.color == undefined) {
						color = '#97BBCD'
					}
					else {
						color = value.color;
						console.log('color', color);
					}
					if (value.color == 'brown') { color = '#800000'; }
					if (value.color == 'orange') { color = '#FF4500'; }
					if (value.color == 'green') { color = '#008000'; }
					if (value.color == 'blue') { color = '#4682B4'; }

					scope.colors.push(color);

					
					scope.data.push(value.counts);

					if (value.counts && value.counts.length > 0)
					{
						scope.hasData = true;
					}

					//var obj = {
					//	label: '# of Votes',
					//	data: value.counts
					//};
					//scope.datasets.push(obj);
					scope.series.push(value.name);

				});

				//scope.chartOptions.chartColors = colors;

				//scope.chartOptions.colors = [];
				//: [ '#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'],
				
			}
		}
	}
})();