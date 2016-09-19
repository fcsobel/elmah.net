(function () {

    angular.module('c3o.core')

		// Package Detail
		.directive('c3oErrorItem', function () {
			return {
				restrict: 'A',
				scope:
				{
				    error: '=c3oErrorItem'
				},
				//replace : true,
				templateUrl: '/app/core/directives/ErrorItem.html',
				controller: function ($scope, ErrorService) {
				}
			};
		})

}());



