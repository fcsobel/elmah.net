(function () {
	
	angular.module('elmah.net.logger').directive('logMessageChart', exampleApiDetail);

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
			    //pointHitDetectionRadius : 1,
			    //pointDotRadius: 1,			    
			    tooltips: {
			        mode: 'index',
			        intersect: true,
			        //itemSirt: '',
			        //filter: '',
			        //position: 'nearest',
			        position: 'myCustomPosition',			        
			        callbacks: {
			            label: function (tooltipItem, data) {
			                var dataset = data.datasets[tooltipItem.datasetIndex]; //.label;
			                var item = dataset.data[tooltipItem.index];
			                if (item.y > 0) {
			                    var label = data.labels[tooltipItem.index];
			                    return dataset.label + ': ' + item.y;
			                }
			                else {
			                    return null;
			                }
			            }
			        }
			    },
				responsive: true,
				spanGaps: true,
				fullWidth: true,
				maintainAspectRatio: false,
				scales: {
					xAxes: [{
						type: 'time',
						display: true,						
						spanGaps: true,
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

			Chart.Tooltip.positioners.myCustomPosition = function (unused, position) {
			    return { x: position.x, y: 6 };
			}
			
			//Chart.Tooltip.positioners.myCustomPosition = function (unused, position) {
			//    return { x: 10, y: 10 };
			//}

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

				scope.data = [];
				scope.series = [];
				scope.colors = [];
				scope.hasData = false;

				// get selected types
				var selected = scope.model.model.types;
				var selected = _.filter(selected, { 'selected': true });

				//console.log('selected1', selected);

				if (selected.length <= 0) { selected = scope.model.model.types };
				
				//console.log('selected', selected);

				_.forEach(selected, function (value, key) {
				
					// set flag 
					if (value.counts && value.counts.length > 0)
					{
						//console.log('add', value.name);

						var color = '#97BBCD';
						if (value.color == undefined) {
							color = '#97BBCD'
						}
						else {
							color = value.color;
							//console.log('color', color);
						}
						if (value.color == 'brown') { color = '#800000'; }
						if (value.color == 'orange') { color = '#FF4500'; }
						if (value.color == 'green') { color = '#008000'; }
						if (value.color == 'red') { color = '#FF4500'; }
						if (value.color == 'blue') { color = '#4682B4'; }

						scope.colors.push(color);  // Add color
						scope.series.push(value.name); // Add series names
						scope.data.push(value.counts); // Add counts				
						scope.hasData = true;
					}
				});

				//scope.chartOptions.chartColors = colors;

				//scope.chartOptions.colors = [];
				//: [ '#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'],
				
			}
        };
    };

})();