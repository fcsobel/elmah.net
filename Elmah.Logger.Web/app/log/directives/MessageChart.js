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
			scope: {
				log: '@',
				severity: '@',
				limit: '@',
				span: '@'
			},
			templateUrl: '/app/log/directives/MessageChart.html?v=1',
			link: link
		};


		// Link Function
		function link(scope, el, attrs) {
			scope.hasData = false;

			scope.model = {};

			// Load Message Chart 
			scope.init = function () {
			    if (scope.log) {
			        scope.Find(scope.log);
				}
				else {
			    	scope.model = logService.model();
				}
			}


			scope.newDate = function (days) {
				return moment().add(days, 'd');
			}

			// initialize
			scope.init();


			//scope.labels = ["January", "February", "March", "April", "May", "June", "July", "test"];
			scope.labels = [scope.newDate(-90), scope.newDate(-6), scope.newDate(-5), scope.newDate(-4), scope.newDate(-3), scope.newDate(-1), scope.newDate(0)];
			scope.series = ['Errors', 'Series B'];
			scope.data = [
			  [65, 59, 80, 81, 56, 55, 88],
			  [28, 48, 40, 19, 27, 90, 34, ]
			];
			scope.onClick = function (points, evt) {
				//console.log(points, evt);
			};
			//scope.chartOptions = { type: 'line', fill: true};

			scope.test1 = function () {
				scope.data = [
				  [65, 59, 80, 81, 56, 155, 120],
				  [28, 48, 40, 19, 27, 90, 24, ]
				];
			};

			//http://www.chartjs.org/docs/#scales-linear-scale
			scope.chartOptions = {
				responsive: true,
				spanGaps: true,
				fullWidth: true,
				maintainAspectRatio: false,
				scales: {
					xAxes: [{
						type: 'time',
						display: true,
						//unit: 'day',
						//round: 'day',
						time: {
							//min: scope.newDate(-80),
							//max: scope.newDate(-0),
							//unitStepSize: 2,
							//round: 'day',
							//unit: 'day',
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


			//scope.labels = [scope.newDate(-90), scope.newDate(-6), scope.newDate(-5), scope.newDate(-4), scope.newDate(-3), scope.newDate(-1), scope.newDate(0)];
			//scope.series = ['Series A', 'Series B'];
			//scope.data = [
			//  [65, 59, 80, 81, 56, 55, 88],
			//  [28, 48, 40, 19, 27, 90, 34, ]
			//];

			scope.$watch('model.model.typeCount', function (newValue, oldValue, scope) {
				scope.RefreshChart();
			});

			scope.counter = 0;

			scope.RefreshChart = function () {
				
				scope.counter = scope.counter  +1;
				//console.log('refresh', scope.counter)

				//scope.chartOptions.responsive = false;

				
				var labels = [];
				//var data1 = [];
				//var data2 = [];
				var data4 = [];
				var data3 = [];
				var list = scope.model.model.typeCount;

				//console.log('list', list);

				_.forEach(list, function (value, key) {					
					//console.log(value, key);
					labels.push(moment(new Date(value.name)));
					///data1.push(value.count);
					//data2.push(value.count * 2);
					data3.push({ x: moment(new Date(value.name)), y: value.count })
					data4.push({ x: moment(new Date(value.name)), y: value.count * 2 })
					scope.hasData = true;
				});

				//data3 = data4.splice(20, 40);
				//data4 = data4.splice(1, 20);

				//console.log(labels);
				scope.data = [];
				scope.labels = [];				
				scope.labels = labels;
				//scope.data = [data1, data2];
				//scope.data = [data3, data4];
				scope.data = [data3];

				//scope.chartOptions.responsive = false;


				//var ctx = document.getElementById("myChart");
				//var myChart = new Chart(ctx, {
				//	type: 'line',
				//	data: {
				//		labels: labels,
				//		series : ['Series A', 'Series B'],
				//		datasets: [{
				//			label: '# of Votes',
				//			data: data1,							
				//			borderWidth: 1
				//		}]
				//	},
				//	options: {
				//		responsive: true,
				//		spanGaps: true,
				//		fullWidth: true,
				//		maintainAspectRatio: false,
				//		scales: {
				//			xAxes: [{
				//				type: 'time', display: true,
				//				time: {
				//					//min: scope.newDate(-80),
				//					//max: scope.newDate(-0),
				//					unitStepSize: 5,
				//					displayFormats: {
				//						'millisecond': 'MMM DD',
				//						'second': 'MMM DD',
				//						'minute': 'MMM DD',
				//						'hour': 'MMM DD',
				//						'day': 'MMM DD',
				//						'week': 'MMM DD YY',
				//						'month': 'MMM YY',
				//						'quarter': 'MMM YY',
				//						'year': 'MMM YY',
				//					}
				//				}
				//			}, ],
				//			yAxes: [{ display: false }, ]
				//		}
				//	}
				//});

				//// another chart
				//var chart = new CanvasJS.Chart("chartContainer",
				//{
				//	axisX:{
				//		title: "timeline",
				//		gridThickness: 2
				//	},
				//});

				//var dates = [];

				//_.forEach(list, function (value, key) {
				//	var item = { x: new Date(value.name), y: value.count };
				//	dates.push(item);
				//});
				//console.log('dates', dates);
				//chart.options.data = dates;
				//chart.options.data = [
				//	{
				//		type: "splineArea",
				//		dataPoints: dates
				//	}
				//];

				//chart.render();

			}
									

		}
	}
})();