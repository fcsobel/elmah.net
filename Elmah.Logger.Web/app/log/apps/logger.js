(function () {

	// Declare app level module which depends on filters, and services
	angular.module('elmah.net.logger', [
	  'ngSanitize',
	  'ngCookies',
	  'ngRoute',
	  'ngAnimate',
	  'angularSpinner',
	  'elmah.net.core',
	  'chart.js'
	])

	// Optional configuration
		  .config(['ChartJsProvider', function (ChartJsProvider) {

		  	// Configure all charts
		  	ChartJsProvider.setOptions({
				colors : [ '#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'],
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