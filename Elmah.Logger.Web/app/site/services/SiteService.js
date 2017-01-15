(function () {

	angular.module('elmah.net.core')

		.factory('SiteService', ['$q', 'SiteApi', 'ErrorService', function ($q, siteApi, errorService) {

		    // Context Object - site  
		    var context = { model: {} };

		    // Init
			var check = function () {
			    var promise = siteApi.check()
					.then(function (response) { // handle response

					    //console.log('SiteService', response);

					    if (response != null) {
					        context.model = new Elmah.Net.Models.Site(response);
					    }

					    // return container
					    return context;
					})
					.catch(function (error) {  // handle error
					    errorService.add(error, null, null, 'SiteService.check');
					    return $q.reject(error);
					});
			    return promise;
			};

			var create = function () {
			    var promise = siteApi.create()
					.then(function (response) { // handle response

					    if (response != null) {
					        context.model = new Elmah.Net.Models.Site(response);
					    }

					    // return container
					    return context;
					})
					.catch(function (error) {  // handle error
					    errorService.add(error, null, null, 'SiteService.check');
					    return $q.reject(error);
					});
			    return promise;
			};


			return {
			    context: function () { return context; },
			    check: check,
			    create: create,
			};
		}
		]);
}());