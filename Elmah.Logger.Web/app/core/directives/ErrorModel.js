(function () {

	angular.module('c3o.core')

		.directive('c3oErrorModel', function () {
			return {
				restrict: 'AE',
				scope: { c3oErrorModel: '=' },
				controller: function ($scope, ErrorService) {
					if (!$scope.c3oErrorModel) {
						$scope.c3oErrorModel = ErrorService.model.errors;
					}
				},
			};
		})

}());