(function () {

	angular.module('elmah.net.core')

		.factory('SiteApi', ['$http', '$q', 'ErrorService', function ($http, $q, errorService) {

			var url = "/api.site/sites/"

			return {
                
			    // get intial data
			    check: function () {
			        var promise = $http.get(url)
						.then(
							function (response) {

							    //console.log('SiteApi', response.data);

							    return response.data;
							})
						.catch(function (error) {
						    errorService.add(error, url, null, "SiteApi.search");
						    return $q.reject(error);
						});
			        return promise;
			    },
                
			    // Search and Create or Update Filter
			    create: function (name) {
			        var query = { name: name };
			        var promise = $http.post(url, query)
						.then(
							function (response) {
							    return response.data;
							})
						.catch(function (error) {
						    errorService.add(error, url, query, "SiteApi.create");
						    return $q.reject(error);
						});
			        return promise;
			    },


			    delete: function () {
			        var promise = $http.delete(url)
						.then(
							function (response) {
							    return response.data;
							})
						.catch(function (error) {
						    errorService.add(error, url, name, "SiteApi.deleteByName");
						    return $q.reject(error);
						});
			        return promise;
			    },
                
			}
		}
		]);
}());