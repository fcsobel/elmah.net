(function () {

	angular.module('elmah.net.core')

		// Package Detail
		.directive('elmahNetErrorList', function () {
			return {
				restrict: 'AE',
				//scope:
				//{
				//	errors: '='
				//},
				templateUrl: '/app/core/directives/ErrorList.html',
				controller: function ($scope, ErrorService) {
					$scope.errors = ErrorService.list();
				}
			};
		})

}());