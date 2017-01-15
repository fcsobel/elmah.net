(function () {

	angular.module('elmah.net.core')

		// Package Detail
		.directive('elmahNetErrorTreeItem', function (RecursionHelper) {
			return {
				restrict: 'AE',
				scope:
				{
					error: '=elmahNetErrorTreeItem'
				},
				//replace : true,
				templateUrl: '/app/core/directives/ErrorTreeItem.html',
				controller: function ($scope, ErrorService) {

					var model = {
						showRaw: false,
						showDetail: false
					}
				},
				compile: function (element) {
					// Use the compile function from the RecursionHelper,
					// And return the linking function(s) which it returns
					return RecursionHelper.compile(element);
				}
			};
		})

}());