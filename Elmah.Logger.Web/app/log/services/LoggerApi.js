(function () {

	angular.module('elmah.net.logger')

		.factory('LoggerApi', ['$http', '$q', 'ErrorService', function ($http, $q, errorService) {

			var url = "/api.logger"

			return {

				//// check site exists
				//check: function () {
				//	var promise = $http.get(url + '/sites/')
				//		.then(
				//			function (response) {
				//				return response.data;
				//			})
				//		.catch(function (error) {
				//			errorService.add(error, url, null, "LoggerApi.check");
				//			return $q.reject(error);
				//		});
				//	return promise;
				//},


			    // get intial data
			    init: function () {
			    	var promise = $http.get(url + '/messages/init/')
						.then(
							function (response) {
							    return response.data;
							})
						.catch(function (error) {
							errorService.add(error, url + '/messages/init', data, "LoggerApi.init");
						    return $q.reject(error);
						});
			        return promise;
			    },
                
			    // Search and Create or Update Filter
			    searchAndUpdate: function (name, filter, limit, span, logs, applications, severities, types, sources, users, start, end, searchText) {

			        filter.query = { search: searchText, limit: limit, span: span, logs: logs, applications: applications, severities: severities, types: types, sources: sources, users: users, start: start, end: end };

			        var promise = $http.post(url + '/messages/search/' + encodeURIComponent(name), filter)
						.then(
							function (response) {

							    // get list 
							    var model = response.data;

							    return model;
							})
						.catch(function (error) {
							errorService.add(error, url + '/messages/search/' + name, filter, "LoggerApi.searchAndUpdate");
						    return $q.reject(error);
						});
			        return promise;
			    },
				
			    // search by name
			    find: function (name) {
			        // /api.logger/search/{name}
			    	var promise = $http.get(url + '/messages/search/' + encodeURIComponent(name))
						.then(
							function (response) {
							    return response.data; // get list 
							})
						.catch(function (error) {
							errorService.add(error, url + '/messages/search/' + name, name, "LoggerApi.searchByName");
						    return $q.reject(error);
						});
			        return promise;
			    },


			    // delete by name
			    deleteByName: function (name) {
			        // /api.logger/search/{name}
			    	var promise = $http.delete(url + '/messages/search/' + encodeURIComponent(name))
						.then(
							function (response) {
							    return response.data;
							})
						.catch(function (error) {
							errorService.add(error, url + '/messages/search/' + name, data, "LoggerApi.deleteByName");
						    return $q.reject(error);
						});
			        return promise;
			    },
                
			    // Search by Query
			    search: function (limit, span, logs, applications, severities, types, sources, users, start, end, searchText) {

			        var data = { params: { search: searchText, limit: limit, span: span, logs: logs, applications: applications, severities: severities, types: types, sources: sources, users: users, start: start, end: end } }; //span: 10000

			        var promise = $http.get(url + '/messages/search', data)
						.then(
							function (response) {
							    return response.data; // get list 
							})
						.catch(function (error) {
							errorService.add(error, url + '/messages/search', data, "LoggerApi.search");
						    return $q.reject(error);
						});
			        return promise;
			    },


				// Minimal create new account
				detail: function (id) {
					var promise = $http.get(url + '/messages/' + id)
						.then(
							function (response) {
								return response.data;								
							})
						.catch(function (error) {
							errorService.add(error, url + '/messages/' + id, null, "LoggerApi.detail");
							return $q.reject(error);
						});
					return promise;
				},

			    // delete by name
				deleteMessage: function (id) {
					var promise = $http.delete(url + '/messages/' + id)
						.then(
							function (response) {
							    return response.data;
							})
						.catch(function (error) {
							errorService.add(error, url + '/messages/' + id, null, "LoggerApi.deleteMessage");
						    return $q.reject(error);
						});
				    return promise;
				},
			}
		}
		]);

}());