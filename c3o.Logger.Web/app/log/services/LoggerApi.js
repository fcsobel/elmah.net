(function () {

    angular.module('c3o.core')

		.factory('LoggerApi', ['$http', '$q', 'ErrorService', function ($http, $q, errorService) {

			var url = "/api.logger/messages/"

			return {
                
			    // get intial data
			    init: function () {
			        var promise = $http.get(url + 'init/')
						.then(
							function (response) {
							    return response.data;
							})
						.catch(function (error) {
						    errorService.add(error, url + 'init', data, "LoggerApi.search");
						    return $q.reject(error);
						});
			        return promise;
			    },
                
			    // Search and Create or Update Filter
			    searchAndUpdate: function (name, limit, span, logs, applications, severities, types, sources, users, start, end) {

			        var query = { limit: limit, span: span, logs: logs, applications: applications, severities: severities, types: types, sources: sources, users: users, start: start, end: end };

			        //console.log('post', url + 'search/' + name, query);

			        var promise = $http.post(url + 'search/' + encodeURIComponent(name), query)
						.then(
							function (response) {

							    // get list 
							    var model = response.data;

							    return model;
							})
						.catch(function (error) {
						    errorService.add(error, url + 'search/' + encodeURIComponent(name), query, "LoggerApi.searchAndUpdate");
						    return $q.reject(error);
						});
			        return promise;
			    },

				
			    // search by name
			    find: function (name) {
			        // /api.logger/search/{name}
			        var promise = $http.get(url + 'search/' + encodeURIComponent(name))
						.then(
							function (response) {
							    return response.data; // get list 
							})
						.catch(function (error) {
						    errorService.add(error, url + 'search/' + name, name, "LoggerApi.search");
						    return $q.reject(error);
						});
			        return promise;
			    },


			    // delete by name
			    deleteByName: function (name) {
			        // /api.logger/search/{name}
			        var promise = $http.delete(url + 'search/' + encodeURIComponent(name))
						.then(
							function (response) {
							    return response.data;
							})
						.catch(function (error) {
						    errorService.add(error, url + 'search/' + name, data, "LoggerApi.deleteByName");
						    return $q.reject(error);
						});
			        return promise;
			    },
                
				// Get List of Editors
			    search: function (limit, span, logs, applications, severities, types, sources, users, start, end) {

			        var data = { params: { limit: limit, span: span, logs: logs, applications: applications, severities: severities, types: types, sources: sources, users: users, start: start, end: end } }; //span: 10000

			        var promise = $http.get(url + 'search', data)
						.then(
							function (response) {
							    return response.data; // get list 
							})
						.catch(function (error) {
						    errorService.add(error, url + 'search', data, "LoggerApi.search");
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

			    // delete by name
				deleteMessage: function (id) {
				    var promise = $http.delete(url + id)
						.then(
							function (response) {
							    return response.data;
							})
						.catch(function (error) {
						    errorService.add(error, url + id, null, "LoggerApi.deleteMessage");
						    return $q.reject(error);
						});
				    return promise;
				},
			}
		}
		]);
}());