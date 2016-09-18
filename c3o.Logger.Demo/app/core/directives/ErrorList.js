(function () {

    angular.module('c3o.core')

		// Package Detail
		.directive('c3oErrorList', function () {
			return {
				restrict: 'E',
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