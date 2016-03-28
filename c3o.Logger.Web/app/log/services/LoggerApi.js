(function () {

    angular.module('c3o.core')

		.factory('LoggerApi', ['$http', '$q', 'ErrorService', function ($http, $q, errorService) {

			var url = "/api.logger/messages/"

			return {

				
				// Get List of Editors
				search: function (log, severity, limit, span, types, sources, users, start, end) {

					// /api.logger/search/Production
					// /api.logger/search/{log}
					var data = { params: { severity: severity, limit: limit, span: span, types: types, sources: sources, users : users, start: start, end: end } }; //span: 10000

					console.log(data);

					var promise = $http.get(url +  'search/' + log, data)
						.then(
							function (response) {

								// get list 
							    var model = response.data;

								return model;
							})
						.catch(function (error) {
							errorService.add(error, url +  'search/' + log, data, "LoggerApi.search");
							return $q.reject(error);
						});
					return promise;
				},

				// Minimal create new account
				detail: function (id) {
					var promise = $http.get(url + id)
						.then(
							function (response) {
								return response.data;								
							})
						.catch(function (error) {
							errorService.add(error, url + id, null, "LoggerApi.detail");
							return $q.reject(error);
						});
					return promise;
				},			
			}
		}
		]);

}());

