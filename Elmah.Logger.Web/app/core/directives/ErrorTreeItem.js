(function () {

    angular.module('c3o.core')

		// Package Detail
		.directive('c3oErrorTreeItem', function (RecursionHelper) {
			return {
				restrict: 'AE',
				scope:
				{
				    error: '=c3oErrorTreeItem'
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