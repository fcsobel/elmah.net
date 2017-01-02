(function () {

	// Declare app level module which depends on filters, and services
	angular.module('c3o.logger', [
	  'ngSanitize',
	  'ngCookies',
	  'ngRoute',
	  'ngAnimate',
	  'angularSpinner',
	  'c3o.core',
	  //'highcharts-ng',
	  'chart.js'
	])
			// Optional configuration
		  .config(['ChartJsProvider', function (ChartJsProvider) {

		  	// Configure all charts
  			ChartJsProvider.setOptions({
  				//chartColors: ['#FF5252', '#FF8A80'],
  				//chartColors: ['#AA5252', '#AA8A80'],
  				responsive: true,
  				fullWidth: true,
  				maintainAspectRatio: false,
  				title: { display: false },
  				legend: { display: false }
  			});

  			// Configure all line charts
  			ChartJsProvider.setOptions('line', {
  				showLines: true,
  				showLabels: false,
  				legend: { display: false }
  			});
		  }]);

}());