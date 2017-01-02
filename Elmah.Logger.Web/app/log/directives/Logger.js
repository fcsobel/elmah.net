(function () {
	'use strict';

	angular.module('c3o.core').directive('logLogger', logLogger);

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

			//scope.newDate = function(days) {
			//	return moment().add(days, 'd');
			//}

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


						
			////http://www.chartjs.org/docs/#scales-linear-scale
			//scope.chartOptions = {
			//	responsive: true,
			//	spanGaps: true,
			//	scales: {
			//		xAxes: [{
			//			type: 'time', display: true,
			//			time: {
			//				min: scope.newDate(-80),
			//				max: scope.newDate(-0),
			//				unitStepSize : 5,
			//				displayFormats: {
			//					'millisecond': 'MMM DD',
			//					'second': 'MMM DD',
			//					'minute': 'MMM DD',
			//					'hour': 'MMM DD',
			//					'day': 'MMM DD',
			//					'week': 'MMM DD YY',
			//					'month': 'MMM YY',
			//					'quarter': 'MMM YY',
			//					'year': 'MMM YY',
			//				}
			//			}
			//		}, ],
			//		yAxes: [{ display: false }, ]
			//	}

			//};

		    //scope.chartConfig = {
		    //	options: {
		    //		rangeSelector: {
		    //			selected: 1
		    //		},
		    //		//chart: { type: "StockChart", spacing: [0, 0, 0, 0] },
		    //		legend: {
		    //			enabled: true, itemMarginLeft: 2, itemMarginRight: 2, itemMarginBottom: 0, itemMarginTop: 0, itemStyle: { fontSize: "10px" }, margin: 0, useHTML: true, symbolHeight: 8, symbolWidth: 8, symbolRadius: 4
		    //		},
		    //	},
		    //	series: [
			//		{
			//			name: "test1", type: "areaspline", data: [10, 2, 0, 15, 12, 8, 7],

			//			fillColor: {
		    //	linearGradient: {
		    //			x1: 0,
		    //			y1: 0,
		    //			x2: 0,
		    //			y2: 1
		    //	},
		    //	stops: [
			//		[0, Highcharts.getOptions().colors[0]],
			//		[1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
		    //	]
		    //}
			//			,
			//			//name: "Traces", type: "column", zIndex: 3, showInLegend: true
			//		}
		    //	],
		    	
		    //	title: { text: '', style: { display: 'none' } },
		    //	subtitle: { text: '', style: { display: 'none' } },
		    //	loading: false,
		    //	xAxis: { type: "datetime", labels: { overflow: 'justify', align: 'left', x: 4, y: 10, format: "{value:%M:%S}" } },
		    //	yAxis: [
			//			{ title: { text: "" }, min: 0, max: 16 },
			//			//{ title: { text: "Time" }, opposite: true, min: 0, max: 1 }
		    //			],
		    //	//plotOptions: {
		    //	//	areaspline: {
		    //	//		stacking: 'normal', connectNulls: false
		    //	//	}, area: {
		    //	//		stacking: "normal", connectNulls: false
		    //	//	}, column: {
		    //	//		stacking: null, pointIntervalUnit: "second", pointWidth: 10, borderWidth: 0, borderRadiusTopLeft: 5, borderRadiusTopRight: 5, color: {
		    //	//			linearGradient: {
		    //	//				x1: 0, x2: 0, y1: 0, y2: 1
		    //	//			}, stops: [[0, 'rgba(208,208,208,1)'], [1, 'rgba(208,208,208,0.2)']]
		    //	//		}
		    //	//	}, marker: {
		    //	//		enabled: true, radius: 2
		    //	//	}
		    //	//},
		    //	//series: [{
		    //	//	name: "Traces", type: "column", zIndex: 3, showInLegend: true
		    //	//},
			//	//{
			//	//	name: "Total", type: "spline", visible: false, showInLegend: false
			//	//}]
		    //};

		}
	}
})();