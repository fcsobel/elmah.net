(function () {

    angular.module('elmah.net.core')

		// Package Detail
		.directive('elmahNetErrorItem', function () {
			return {
				restrict: 'AE',
				scope:
				{
				    error: '=elmahNetErrorItem'
				},
				//replace : true,
				templateUrl: '/app/core/directives/ErrorItem.html',
				controller: function ($scope, ErrorService) {
				}
			};
		})
}());
