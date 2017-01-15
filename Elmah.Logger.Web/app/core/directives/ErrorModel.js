(function () {

	angular.module('elmah.net.core')

		.directive('elmahNetErrorModel', function () {
			return {
				restrict: 'AE',
				scope: { elmahNetErrorModel: '=' },
				controller: function ($scope, ErrorService) {
					if (!$scope.elmahNetErrorModel) {
						$scope.elmahNetErrorModel = ErrorService.model.errors;
					}
				},
			};
		})

}());