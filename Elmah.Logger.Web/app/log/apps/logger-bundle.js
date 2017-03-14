'use strict';

var Elmah = {};
Elmah.Net = {};
Elmah.Net.Models = {};

//https://developer.mozilla.org/en-US/Add-ons/Overlay_Extensions/XUL_School/JavaScript_Object_Management
//https://developer.mozilla.org/en-US/docs/Web/JavaScript/Inheritance_and_the_prototype_chain
/////////////////////////////////////////////////////////////
// Site Error
/////////////////////////////////////////////////////////////
Elmah.Net.Models.SiteError = function (error, cause, message, key, type) {
	var self = this;
	if (!key) key = "";
	if (!message && error) { message = error.message; }
	if (!message && error && error.data) { message = error.data.message; }
	if (!message) message = "";
	if (!type) type = "error";
	this.error = error;
	this.cause = cause;
	this.message = message;
	this.hide = false;
	this.key = key;
	this.type = type;
	this.isParent = true;
	// see if already logged this error
	var childError = error.siteError;
		
	this.children = [];

	// if this error has already been logged then make it a child of thiis SiteError
	if (childError != null)
	{
		childError.isParent = false;
		this.child = childError;
		this.children.push(childError);
		this.index = childError.index;
	}

	// associate error with this siteError
	error.siteError = this;
}
/////////////////////////////////////////////////////////////
// LogQuery Class
/////////////////////////////////////////////////////////////
Elmah.Net.Models.LogItem = function (model, countData) {
	var self = this;

	_.extend(this, model);

	this.visible = false;
	this.messages = this.messages || [];

	// hold chart counts
	this.counts = [];

	if (countData) {
		// get counts for this type
		var list = _.filter(countData, { id: this.id });

		// add chart date for each count
		//var next = {};
		var last = {};
		_.forEach(list, function (value, key) {

			var next = { x: moment(new Date(value.name)), y: value.count };

			if (!last.x) {
				var prev = { x: moment(next.x).subtract(1, 'days'), y: 0 };
				self.counts.push(prev);
			}

			if (last.x) {
				if (next.x.diff(last.x, 'days') > 1) {
					// add trailing date
					var follow = { x: moment(last.x).add(1, 'days'), y: 0 };
					self.counts.push(follow);
				}

				if (next.x.diff(last.x, 'days') > 2) {
					// add trailing date
					var trail = { x: moment(next.x).subtract(1, 'd'), y: 0 };
					self.counts.push(trail);
				}
			}

			// add date with count
			self.counts.push(next);

			last = next;

		});

		if (self.counts.length > 0) {
			if (last.x) {
				var follow = { x: moment(last.x).add(1, 'days'), y: 0 };
				self.counts.push(follow);
			}
		}
	}

}

// SiteContent class methods
Elmah.Net.Models.LogItem.prototype = {
	get Visible() { return this.visible || this.selected || (this.messages && this.messages.length > 0); }
};

/////////////////////////////////////////////////////////////
// LogMessage Class
/////////////////////////////////////////////////////////////
Elmah.Net.Models.LogMessage = function (data, model) {
    var self = this;

    _.extend(this, data);

	//// uncommet because we need it for detail....may cause issue elsewhere...
    //model.logs = model.logs || [];
    //model.applications = model.applications || [];
    //model.users = model.users || [];
    //model.types = model.types || [];
    //model.sources = model.sources || [];

    // look for item in list and add it if missing
    this.log = this.CheckItem(this.logId, model.logs, this.log);
    this.application = this.CheckItem(this.applicationId, model.applications, this.application);
    this.user = this.CheckItem(this.userId, model.users, this.user);
    this.messageType = this.CheckItem(this.messageTypeId, model.types, this.messageType);
    this.source = this.CheckItem(this.sourceId, model.sources, this.source);

    //this.log = this.log || _.find(model.logs, { id: this.logId });
    //this.application = this.application || _.find(model.applications, { id: this.applicationId });
    //this.user = this.user || _.find(model.users, { id: this.userId });
    //this.messageType = this.messageType || _.find(model.types, { id: this.messageTypeId });
    //this.source = this.source || _.find(model.sources, { id: this.sourceId });    

    // look for severity in list
    this.severityObj = _.find(model.severities, { name: this.severity });

    // add message to item list if new	
    if (this.log) this.CheckMessage(this.log);
    if (this.application) this.CheckMessage(this.application);
    if (this.user) this.CheckMessage(this.user);
    if (this.messageType) this.CheckMessage(this.messageType);
    if (this.source) this.CheckMessage(this.source);
    if (this.severityObj) this.CheckMessage(this.severityObj);

    // Make sure message is in models message list
    //this.CheckMessage(model);
}

// SiteContent class methods
Elmah.Net.Models.LogMessage.prototype = {
	CheckItem: function (id, list, obj) {

		// look for item by id
		var item = _.find(list, { id: id });
		if (!item && obj) {
			item = obj;
			list.push(item);
		}
		return item;
	},

	// check for message in item
	CheckMessage: function (item) {
		if (item) {
			item.messages = item.messages || []; // check for message list
			var index = _.findIndex(item.messages, _.pick(this, 'id')); // get message index by id
			if (index !== -1) {
				item.messages.splice(index, 1, this); // replace message
			} else {
				item.messages.push(this); // add new message to list
			}
		}
	}
	//get Name() { return this.firstName + " " + this.lastName; },
	//get NameLastFirst() { return this.lastName + ", " + this.firstName; }	
};

/////////////////////////////////////////////////////////////
// LogQuery Class
/////////////////////////////////////////////////////////////
Elmah.Net.Models.LogQuery = function (model) {
	var self = this;

	_.extend(this, model);

	if (model.start && model.end) {
		model.startMoment = moment(model.start);
		model.endMoment = moment(model.end);
	}
}

// SiteContent class methods
//Elmah.Net.Models.Query.prototype = {}

/////////////////////////////////////////////////////////////
// LogSearchResponse Class
/////////////////////////////////////////////////////////////
Elmah.Net.Models.LogSearchResponse = function (model) {
	var self = this;

	_.extend(this, model);

    // init
	model.logs = model.logs || [];
	model.applications = model.applications || [];
	model.users = model.users || [];
	model.types = model.types || [];
	model.sources = model.sources || [];

	// convert message list - Transform json data to objects
	this.messages = $.map(model.messages, function (item, i) {
		return new Elmah.Net.Models.LogMessage(item, model);
	});

    // convert items
	this.logs = $.map(model.logs, function (item, i) { return new Elmah.Net.Models.LogItem(item); });
	this.applications = $.map(model.applications, function (item, i) { return new Elmah.Net.Models.LogItem(item); });
	this.severities = $.map(model.severities, function (item, i) { return new Elmah.Net.Models.LogItem(item); });
	// setup types and chart data
	this.types = $.map(model.types, function (item, i) { return new Elmah.Net.Models.LogItem(item, model.typeCount2); });
	this.sources = $.map(model.sources, function (item, i) { return new Elmah.Net.Models.LogItem(item); });
	this.users = $.map(model.users, function (item, i) { return new Elmah.Net.Models.LogItem(item); });

	// Map Query
	this.query = new Elmah.Net.Models.LogQuery(model.query);

	// auto select first message
	this.message = model.messages[0];
}

//// SiteContent class methods
//Elmah.Net.Models.LogSearchResponse.prototype = {
//	//get Name() { return this.firstName + " " + this.lastName; },
//	//get NameLastFirst() { return this.lastName + ", " + this.firstName; }	
//}

/////////////////////////////////////////////////////////////
// Site Class
/////////////////////////////////////////////////////////////
Elmah.Net.Models.Site = function (model) {
	var self = this;
	_.extend(this, model);
}

// SiteContent class methods
Elmah.Net.Models.Site.prototype = {
	//get Name() { return this.firstName + " " + this.lastName; },
	//get NameLastFirst() { return this.lastName + ", " + this.firstName; }	
};

(function () {

	// Declare Main Servcie Module
    angular.module('elmah.net.core', []);

}());

(function () {

	angular.module('elmah.net.core')

		.factory('RecursionHelper', ['$compile', function ($compile) {
			return {
				/**
				 * Manually compiles the element, fixing the recursion loop.
				 * @param element
				 * @param [link] A post-link function, or an object with function(s) registered via pre and post properties.
				 * @returns An object containing the linking functions.
				 */
				compile: function (element, link) {
					// Normalize the link parameter
					if (angular.isFunction(link)) {
						link = { post: link };
					}

					// Break the recursion loop by removing the contents
					var contents = element.contents().remove();
					var compiledContents;
					return {
						pre: (link && link.pre) ? link.pre : null,
						/**
						 * Compiles and re-adds the contents
						 */
						post: function (scope, element) {
							// Compile the contents
							if (!compiledContents) {
								compiledContents = $compile(contents);
							}
							// Re-add the compiled contents to the element
							compiledContents(scope, function (clone) {
								element.append(clone);
							});

							// Call the post-linking function, if any
							if (link && link.post) {
								link.post.apply(null, arguments);
							}
						}
					};
				}
			};
		}]);

}());
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
(function () {

    angular.module('elmah.net.core')

		// Package Detail
		.directive('elmahNetErrorItem', function () {
			return {
				restrict: 'AE',
				scope:
				{
				    error: '=elmahNetErrorItem'
				},
				//replace : true,
				templateUrl: '/app/core/directives/ErrorItem.html',
				controller: function ($scope, ErrorService) {
				}
			};
		})
}());

(function () {

	angular.module('elmah.net.core')

		// Package Detail
		.directive('elmahNetErrorList', function () {
			return {
				restrict: 'AE',
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
(function () {

	angular.module('elmah.net.core')
	.factory('ErrorService', [function () {

		var model = { errors: [] };

		return {
			get model() { return model; },
			list: function () {
				return model.errors;
			},
			hide: function () {
				_.each(model.errors, function (error) {
					error.hide = true;
				});
			},
			add: function (error, cause, message, key, type) {

				//var data = $.param(error);

				//// send error to track.js
				//if (!error.siteError) {				
				//	trackJs.track(error);
				//}

				// create new error object
			    var siteError = new Elmah.Net.Models.SiteError(error, cause, message, key, type);

				if (siteError.index > 0) {
					model.errors[siteError.index - 1] = siteError; // replace error
				}
				else {
					siteError.index = model.errors.push(siteError); // add new error and record index

				}
				//// Arbitrary log messages. 'critical' is most severe; 'debug' is least.
				//if (type == 'critical') Rollbar.critical(message, siteError);
				//if (type == 'error') Rollbar.error(message, siteError);
				//if (type == 'warning') Rollbar.warning(message, siteError);
				//if (type == 'info') Rollbar.info(message, siteError);
				//if (type == 'debug') Rollbar.debug(message, siteError);
			}
		}
	}
	]);

	///////////////////////////////////////////////////////////////
	//// Site Error
	///////////////////////////////////////////////////////////////
    //Elmah.Net.Models.SiteError = function (error, cause, message, key, type) {
	//	var self = this;
	//	if (!key) key = "";
	//	if (!message && error) { message = error.message; }
	//	if (!message && error && error.data) { message = error.data.message; }
	//	if (!message) message = "";
	//	if (!type) type = "error";
	//	this.error = error;
	//	this.cause = cause;
	//	this.message = message;
	//	this.hide = false;
	//	this.key = key;
	//	this.type = type;
	//	this.isParent = true;
	//	// see if already logged this error
	//	var childError = error.siteError;
		
	//	this.children = [];

	//	// if this error has already been logged then make it a child of thiis SiteError
	//	if (childError != null)
	//	{
	//		childError.isParent = false;
	//		this.child = childError;
	//		this.children.push(childError);
	//		this.index = childError.index;
	//	}

	//	// associate error with this siteError
	//	error.siteError = this;

	//}
		

}());
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
(function () {

	// Declare app level module which depends on filters, and services
	angular.module('elmah.net.logger', [
	  'ngSanitize',
	  'ngCookies',
	  'ngRoute',
	  'ngAnimate',
	  'angularSpinner',
	  'elmah.net.core',
	  'chart.js'
	])

	// Optional configuration
		  .config(['ChartJsProvider', function (ChartJsProvider) {

		  	// Configure all charts
		  	ChartJsProvider.setOptions({
				colors : [ '#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'],
		  		//chartColors: ['#FF5252', '#FF8A80'],
		  		//chartColors: ['#AA5252', '#AA8A80'],
		  		responsive: true,
		  		fullWidth: true,
		  		maintainAspectRatio: false,
		  		title: { display: false },
		  		legend: { display: false }
		  	});

		  	// Configure all line charts
		  	ChartJsProvider.setOptions('line', {
		  		showLines: true,
		  		showLabels: false,
		  		legend: { display: false }
		  	});
		  }]);

}());
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
(function () {

	angular.module('elmah.net.logger')

		.factory('LogService', ['$q', 'LoggerApi', 'ErrorService', function ($q, loggerApi, errorService) {

			// Context Object - Model  
			var container = {
				model: { messages: [] }
			};


			// builds filter used by list ng-repeat filter:
			var filter = function (value, index, array) {

			    if (value.deleted) return false;

			    var logs = _.filter(container.model.logs, { 'selected': true });
			    logs = _.map(logs, 'id');

				var applications = _.filter(container.model.applications, { 'selected': true });
				applications = _.map(applications, 'id');

				var types = _.filter(container.model.types, { 'selected': true });
				types = _.map(types, 'id');

				var sources = _.filter(container.model.sources, { 'selected': true });
				sources = _.map(sources, 'id');

				var users = _.filter(container.model.users, { 'selected': true });
				users = _.map(users, 'id');

				var severities = _.filter(container.model.severities, { 'selected': true });
				severities = _.map(severities, 'name');

			    //console.log('users', users);
				//var sources = _.filter(container.model.sources, { 'selected': true }).map('id');

				return (types.length <= 0 || _.includes(types, value.messageTypeId))
					&& (sources.length <= 0 || _.includes(sources, value.sourceId))
					&& (applications.length <= 0 || _.includes(applications, value.applicationId))
                    && (logs.length <= 0 || _.includes(logs, value.logId))
					&& (severities.length <= 0 || _.includes(severities, value.severity))
					&& (users.length <= 0 || _.includes(users, value.userId));

			};

			
			//// Check Site
			//check = function () {
			//	var promise = siteApi.check()
			//		.then(function (response) { // handle response

			//			if (response != null) {
			//				container.site = new Elmah.Net.Models.Site(response);
			//			}

			//			// return container
			//			return container;
			//		})
			//		.catch(function (error) {  // handle error
			//			errorService.add(error, null, null, 'logService.check');
			//			return $q.reject(error);
			//		});
			//	return promise;
			//};


		    // Init
			var init = function () {
			    var promise = loggerApi.init()
					.then(function (response) { // handle response

					    container.model = new Elmah.Net.Models.LogSearchResponse(response);

					    //// convert message list - Transform json data to objects
					    //response.messages = $.map(response.messages, function (item, i) {
					    //    return new Elmah.Net.Models.LogMessage(item, response);
					    //});

					    //// Map Query
					    //response.query = new Elmah.Net.Models.LogQuery(response.query);

					    //// reset model with serach results
					    //container.model = response;

					    //// auto select first message
					    //container.model.message = container.model.messages[0];

					    // return container
					    return container;
					})
					.catch(function (error) {  // handle error
					    errorService.add(error, null, null, 'logService.search');
					    return $q.reject(error);
					});
			    return promise;
			};

		    // Create or update filter
			var searchAndUpdate = function (name, filter, limit, span, logs, applications, severities, types, sources, users, start, end, searchText) {
			    var promise = loggerApi.searchAndUpdate(name, filter, limit, span, logs, applications, severities, types, sources, users, start, end, searchText)
					.then(function (response) { // handle response

					    container.model = new Elmah.Net.Models.LogSearchResponse(response);

					    //// convert message list - Transform json data to objects
					    //response.messages = $.map(response.messages, function (item, i) {
					    //    return new Elmah.Net.Models.LogMessage(item, response);
					    //});

					    //// reset model with serach results
					    //container.model = response;

					    //// auto select first message
					    //container.model.message = container.model.messages[0];




					    // return container
					    return container;
					})
					.catch(function (error) {  // handle error
					    errorService.add(error, null, null, 'logService.searchAndUpdate');
					    return $q.reject(error);
					});
			    return promise;
			};


		    // Delete
			var deleteByName = function (name) {
			    var promise = loggerApi.deleteByName(name)
					.then(function (response) { // handle response

					    container.model = new Elmah.Net.Models.LogSearchResponse(response);

					    //// convert message list - Transform json data to objects
					    //response.messages = $.map(response.messages, function (item, i) {
					    //    return new Elmah.Net.Models.LogMessage(item, response);
					    //});

					    //// reset model with serach results
					    //container.model = response;

					    //// auto select first message
					    //container.model.message = container.model.messages[0];

					    // return container
					    return container;
					})
					.catch(function (error) {  // handle error
					    errorService.add(error, null, null, 'logService.search');
					    return $q.reject(error);
					});
			    return promise;
			};


		    // Search Loge Messages
			var find = function (name) {
			    var promise = loggerApi.find(name)
					.then(function (response) { // handle response

					    container.model = new Elmah.Net.Models.LogSearchResponse(response);

					    //// convert message list - Transform json data to objects
					    //response.messages = $.map(response.messages, function (item, i) {
					    //    return new Elmah.Net.Models.LogMessage(item, response);
					    //});

					    //// reset model with serach results
					    //container.model = response;

					    //// auto select first message
					    //container.model.message = container.model.messages[0];

					    // return container
					    return container;
					})
					.catch(function (error) {  // handle error
					    errorService.add(error, null, null, 'logService.search');
					    return $q.reject(error);
					});
			    return promise;
			};

			// Search Loge Messages
			var search = function (limit, span, logs, applications, severities, types, sources, users, start, end, searchText) {
			    var promise = loggerApi.search(limit, span, logs, applications, severities, types, sources, users, start, end, searchText)
					.then(function (response) { // handle response

					    container.model = new Elmah.Net.Models.LogSearchResponse(response);

						//// convert message list - Transform json data to objects
						//response.messages = $.map(response.messages, function (item, i) {
						//    return new Elmah.Net.Models.LogMessage(item, response);
						//});

					    //// Map Query
					    //response.query = new Elmah.Net.Models.LogQuery(response.query);

						//// reset model with serach results
						//container.model = response;

						//// auto select first message
						//container.model.message = container.model.messages[0];

						// return container
						return container;
					})					
					.catch(function (error) {  // handle error
						errorService.add(error, null, null, 'logService.search');
						return $q.reject(error);
					});
				return promise;
			};

			// get full Message detail
			var detail = function (id) {
				var promise = loggerApi.detail(id)
					// Handle message Response
					.then(function (response) {

						// convert response message to message object
					    var message = new Elmah.Net.Models.LogMessage(response, container.model);

						// get index by id
						var index = _.findIndex(container.model.messages, _.pick(message, 'id'));

						if (index !== -1) {
							// refresh message object in list
							container.model.messages.splice(index, 1, message);
						} else {
							// add new message to list
							container.model.messages.push(message);
						}

						// auto select message
						container.model.message = message;

						//return message;
						// return container
						return container;
					})
					// Handle error
					.catch(function (error) {
						errorService.add(error, null, null, 'logService.detail');
						return $q.reject(error);
					});
				return promise;
			};

		    // Update Message
			var deleteMessage = function (message) {
			    var promise = loggerApi.deleteMessage(message.id).then(
						function (data) {

						    message.deleted = true;

						    //container.model = data;
						    return container;
						}).catch(
								function (error) {
								    errorService.add(error, null, null, 'logService.deleteMessage');
								    return $q.reject(error);
								});
			    return promise;
			};

			// Update Message
			var update = function (obj) {
				var promise = loggerApi.update(obj).then(
						function (data) {
							container.model = data;
							return container;
						}).catch(
								function (error) {
									errorService.add(error, null, null, 'logService.update');
									return $q.reject(error);
								});
				return promise;
			};

			return {
				model: function () { return container; },
				filter: filter,
				find: find,
				searchAndUpdate: searchAndUpdate,
				detail: detail,
				init: init,
				search: search,
				update: function (obj) { return update(obj); },
			    deleteByName: deleteByName,
			    deleteMessage: deleteMessage
			    //check: check
			};
		}
		]);

}());
(function () {
	'use strict';

	angular.module('elmah.net.logger').directive('logLogger', logLogger);

	// dependencies
	logLogger.$inject = ['$window', '$cookies', '$timeout', 'SiteService', 'usSpinnerService'];

	// Package Options Directive
	function logLogger($window, $cookies, $timeout, siteService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: {},
			templateUrl: '/app/log/directives/Logger.html',
			link: link
		};
		
		// Link Function
		function link(scope, el, attrs) {

		    scope.context = {};

		    scope.Create = function () {
		        usSpinnerService.spin('spinner-1');

                // create site
		        siteService.create()
					.then(function (response) { // sucess
					    //console.log(response);
                        
					    scope.context = response;
					    usSpinnerService.stop('spinner-1');
					});
		    };

		    scope.Check = function () {
		        usSpinnerService.spin('spinner-1');
		        siteService.check()
					.then(function (response) { // sucess

					    //console.log(response);

					    scope.context = response;
					    usSpinnerService.stop('spinner-1');
					});
		    };

		    scope.Check();

		}
	}

})();
(function () {
	'use strict';

	angular.module('elmah.net.logger').directive('logMessageFilter', logMessageFilter);

	// dependencies
	logMessageFilter.$inject = ['$window', '$cookies', '$timeout', '$rootScope', 'LogService', 'usSpinnerService'];

	// Package Options Directive
	function logMessageFilter($window, $cookies, $timeout, $rootScope, logService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: {
				log: '@',
				severity: '@',
				limit: '@',
				span: '@',
				type: '@',
				source: '@'
			},
			templateUrl: '/app/log/directives/MessageFilter.html',
			link: link
		};


		// Link Function
		function link(scope, el, attrs) {

			scope.model = { types: [], sources: [], users: [], logs: [], applications: [], severities: [] };

			// setup query
			scope.model.query = {
				log: scope.log,
				severity: scope.severity,
				limit: scope.limit,
				span: scope.span,
				type: scope.type,
				source: scope.source,
				startMoment: moment().subtract(7, 'days').startOf('day'), //utc().
				endMoment: moment().endOf('day'), //utc()
			};

			scope.filter = {};

			scope.Clear = function () {
				scope.filter = {};
				scope.model.query = {
					limit: 100,
					span: 60 * 24,
					startMoment: moment().subtract(7, 'days').startOf('day'), //utc().
					endMoment: moment().endOf('day'), //utc()
				};
			}

			scope.Showhide = function (list) {
				_.forEach(list, function (value, key) {
					value.visible = !value.visible;
				});
			}

			scope.SetFilter = function (item) {
				// set current filter
				scope.filter = item;

				scope.model.query = item.query;

				//scope.query.name = item.name;

				if (scope.model.query.span == 0) {
					scope.model.query.startMoment = moment(item.query.start);
					scope.model.query.endMoment = moment(item.query.end);

					var drp = $('#demo').data('daterangepicker');
					drp.setStartDate(scope.model.query.startMoment.format('MM/DD/YYYY'));
					drp.setEndDate(scope.model.query.endMoment.format('MM/DD/YYYY'));
				}

				scope.Find(scope.filter.name);
			}


			scope.RefreshQuery = function () {
				// find all selected objects
				var types = _.filter(scope.model.types, { 'selected': true });
				var sources = _.filter(scope.model.sources, { 'selected': true });
				var users = _.filter(scope.model.users, { 'selected': true });
				var logs = _.filter(scope.model.logs, { 'selected': true });
				var applications = _.filter(scope.model.applications, { 'selected': true });
				var severities = _.filter(scope.model.severities, { 'selected': true });

				// get ids for query
				scope.model.query.types = _.map(types, 'id');
				scope.model.query.sources = _.map(sources, 'id');
				scope.model.query.users = _.map(users, 'id');
				scope.model.query.logs = _.map(logs, 'id');
				scope.model.query.applications = _.map(applications, 'id');
				scope.model.query.severities = _.map(severities, 'name');
			}

			scope.Delete = function (item) {

				usSpinnerService.spin('spinner-1');

				// clear current filter selection
				scope.filter = {};

				// get query data
				scope.RefreshQuery();

				// find by name
				logService.deleteByName(item.name)
					.then(function (response) { // sucess
						scope.model = response.model;
						usSpinnerService.stop('spinner-1');

						scope.Refresh();
					});
			}

			scope.Find = function (name) {

				usSpinnerService.spin('spinner-1');

				// get query data
				scope.RefreshQuery();


				// find by name
				logService.find(name)
					.then(function (response) { // sucess
						scope.model = response.model;
						usSpinnerService.stop('spinner-1');

						scope.Refresh();
					});
			}

			scope.SearchAndUpdate = function () {

				usSpinnerService.spin('spinner-1');

				// get query data
				scope.RefreshQuery();

				// Get dates 
				var startDate = null;
				var endDate = null;
				if (scope.model.query.span === 0) {
					if (scope.model.query.startMoment && scope.model.query.endMoment) {
						// gets the native Date object that Moment.js wraps
						startDate = scope.model.query.startMoment.toDate();
						endDate = scope.model.query.endMoment.toDate();
					}
				}

				logService.searchAndUpdate(scope.filter.name, scope.filter,
                    scope.model.query.limit, scope.model.query.span, scope.model.query.logs, scope.model.query.applications, scope.model.query.severities, scope.model.query.types, scope.model.query.sources, scope.model.query.users, startDate, endDate, scope.model.query.search)
					.then(function (response) { // sucess
						scope.model = response.model;
						usSpinnerService.stop('spinner-1');
						scope.Refresh();
					});
			}



			// Search
			scope.Search = function () {

				usSpinnerService.spin('spinner-1');

				// get query data
				scope.RefreshQuery();

				// Get dates 
				var startDate = null;
				var endDate = null;
				if (scope.model.query.span === 0) {
					if (scope.model.query.startMoment && scope.model.query.endMoment) {
						// gets the native Date object that Moment.js wraps
						startDate = scope.model.query.startMoment.toDate();
						endDate = scope.model.query.endMoment.toDate();
					}
				}

			    logService.search(scope.model.query.limit, scope.model.query.span, scope.model.query.logs, scope.model.query.applications, scope.model.query.severities, scope.model.query.types, scope.model.query.sources, scope.model.query.users, startDate, endDate, scope.model.query.search)
					.then(function (response) { // sucess
						scope.model = response.model;

						// refresh UI date from response
						if (scope.model.query.span === 0) {
							if (scope.model.query.start && scope.model.query.end) {
								scope.model.query.startMoment = moment(scope.model.query.start);
								scope.model.query.endMoment = moment(scope.model.query.end);
							}
						}

						usSpinnerService.stop('spinner-1');

						scope.Refresh();
					});
			};

			// Load Intital values
			scope.Init = function () {

				usSpinnerService.spin('spinner-1');

				logService.init()
					.then(function (response) { // sucess
						scope.model = response.model;

						usSpinnerService.stop('spinner-1');

						// auto select matching query items
						scope.Refresh();
					});
			};



			// Update UI based on response
			scope.Refresh = function () {

				if (scope.model.query.span == 0) {
					if (scope.model.query.startMoment && scope.model.query.endMoment) {
						var drp = $('#demo').data('daterangepicker');
						//scope.model.query.start = moment(scope.model.query.start);
						//scope.model.query.end = moment(scope.model.query.end);
						drp.setStartDate(scope.model.query.startMoment.format('MM/DD/YYYY'));
						drp.setEndDate(scope.model.query.endMoment.format('MM/DD/YYYY'));
					}
				}

				scope.Select(scope.model.query.types, scope.model.types);
				scope.Select(scope.model.query.sources, scope.model.sources);
				scope.Select(scope.model.query.users, scope.model.users);
				scope.Select(scope.model.query.logs, scope.model.logs);
				scope.Select(scope.model.query.applications, scope.model.applications);
				scope.Select(scope.model.query.severities, scope.model.severities);

				if (scope.model.messages.length > 0) {
					// get first message
					var message = scope.model.messages[0];

					usSpinnerService.spin('spinner-1');

					// auto select 1st message
					logService.detail(message.id).then(function (response) {
						usSpinnerService.stop('spinner-1');
					});
				}
			}

			scope.Select = function (idList, objList) {
				_.forEach(idList, function (value) {
					var obj = _.find(objList, { 'id': value });
					if (obj) { obj.selected = true; }
				});
			}

			scope.SelectName = function (idList, objList) {
				_.forEach(idList, function (value) {
					var obj = _.find(objList, { 'name': value });
					if (obj) { obj.selected = true; }
				});
			}

			scope.Filter = function ()	{
				//broadcast to refresh ui, chart...etc
				$rootScope.$broadcast("refreshFilter");
			}

			// Load Message Filter 
			scope.init = function () {

				//var start = scope.query.start.format('MM/DD/YYYY');
				//var end = scope.query.end.format('MM/DD/YYYY');

				$('#demo').daterangepicker({
					"autoApply": true,
					//"timePicker": true,
					"startDate": scope.model.query.startMoment.format('MM/DD/YYYY'),
					"endDate": scope.model.query.endMoment.format('MM/DD/YYYY'),
					"maxDate": moment().format('MM/DD/YYYY')
				}, function (start, end, label) {
					//scope.query.start = start;
					//scope.query.end = end;

					scope.model.query.startMoment = moment(start).startOf('day');
					scope.model.query.endMoment = moment(end).endOf('day');

					scope.Search();
				});

				if (scope.log) {
					scope.Find(scope.log);
				}
				else {
					scope.Search();
				}
			}

			// initialize
			scope.init();
		}
	}
})();
(function () {
	'use strict';

	angular.module('elmah.net.logger').directive('logMessageList', exampleApiDetail);

	// dependencies
	exampleApiDetail.$inject = ['$window', '$cookies', '$timeout', 'LogService', 'usSpinnerService'];

	// Package Options Directive
	function exampleApiDetail($window, $cookies, $timeout, logService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: {
				log: '@',
				severity: '@',
				limit: '@',
				span: '@'
			},
			templateUrl: '/app/log/directives/MessageList.html',
			link: link
		};


		// Link Function
		function link(scope, el, attrs) {

			scope.model = {};

			scope.messageFilter = function (value, index, array) {
				return logService.filter(value, index, array);
			}

			scope.Select = function (message)
			{
				usSpinnerService.spin('spinner-1');

				// select message
				logService.detail(message.id).then(function (response) {
					usSpinnerService.stop('spinner-1');
				});
			}

		    // find by name
			scope.Find = function (name) {
			    usSpinnerService.spin('spinner-1');
			    logService.find(name)
					.then(function (response) { // sucess
					    //scope.model = response.model;
					    scope.model.model = response.model;
					    usSpinnerService.stop('spinner-1');
					});
			}

			scope.Delete = function (item) {
			    usSpinnerService.spin('spinner-1');
			    // find by name
			    logService.deleteMessage(item)
					.then(function (response) { // sucess
					    usSpinnerService.stop('spinner-1');
					});
			}

			// Load Message List 
			scope.init = function () {

			    if (scope.log) {
			        scope.Find(scope.log);
					//logService.search(scope.log, scope.severity, scope.limit, scope.span).then(function (response) { // sucess
						//scope.model.model = response.model;
					//});
				}
				else {
					scope.model = logService.model();
				}
			}

			// initialize
			scope.init();
		}
	}
})();
(function () {
	
	angular.module('elmah.net.logger').directive('logMessageChart', exampleApiDetail);

	// dependencies
	exampleApiDetail.$inject = ['$window', '$cookies', '$timeout', 'LogService', 'usSpinnerService'];

	// Package Options Directive
	function exampleApiDetail($window, $cookies, $timeout, logService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: { },
			templateUrl: '/app/log/directives/MessageChart.html?v=1',
			link: link
		};


		// Link Function
		function link(scope, el, attrs) {
			scope.hasData = false;
			scope.model = {};
			// Load Message Chart 
			scope.init = function () {
		    	scope.model = logService.model();
			}	

			// initialize
			scope.init();

			//http://www.chartjs.org/docs/#scales-linear-scale
			scope.chartOptions = {
			    //pointHitDetectionRadius : 1,
			    //pointDotRadius: 1,			    
			    tooltips: {
			        mode: 'index',
			        intersect: true,
			        //itemSirt: '',
			        //filter: '',
			        //position: 'nearest',
			        position: 'myCustomPosition',			        
			        callbacks: {
			            label: function (tooltipItem, data) {
			                var dataset = data.datasets[tooltipItem.datasetIndex]; //.label;
			                var item = dataset.data[tooltipItem.index];
			                if (item.y > 0) {
			                    var label = data.labels[tooltipItem.index];
			                    return dataset.label + ': ' + item.y;
			                }
			                else {
			                    return null;
			                }
			            }
			        }
			    },
				responsive: true,
				spanGaps: true,
				fullWidth: true,
				maintainAspectRatio: false,
				scales: {
					xAxes: [{
						type: 'time',
						display: true,						
						spanGaps: true,
						//minUnit: 'day',
						//round: 'day',
						time: {							
							//min: scope.newDate(-80),
							//max: scope.newDate(-0),
							//unitStepSize: 1,
							//round: 'day',
							minUnit: 'day',							
							displayFormats: {
								'millisecond': 'SSS [ms]',
								'second': 'h:mm:ss a',
								'minute': 'h:mm:ss a',
								'hour': 'MMM D, hA',
								'day': 'MMM DD',
								'week': 'MMM DD YY',
								'month': 'MMM YY',
								'quarter': '[Q]Q - YY',
								'year': 'YYYY',
							}
						}
					}, ],
					yAxes: [{ display: false }, ]
				}
			};

			Chart.Tooltip.positioners.myCustomPosition = function (unused, position) {
			    return { x: position.x, y: 6 };
			}
			
			//Chart.Tooltip.positioners.myCustomPosition = function (unused, position) {
			//    return { x: 10, y: 10 };
			//}

			//scope.$watch('model.model.typeCount', function (newValue, oldValue, scope) {
			//	scope.RefreshChart();
			//});
					
			scope.$watch('model.model.types', function (newValue, oldValue, scope) {
				scope.RefreshChart();
			});

			//$rootScope.$broadcast("refreshFilter");

			scope.$on("refreshFilter", function () {
				scope.RefreshChart();
			});


			//scope.$watch('messageFilter()', function (newValue, oldValue, scope) {
			//	scope.RefreshChart();
			//});


			//scope.GetData = function () {
			//	scope.hasData = false;
			//	var data = [];
			//	var selected = scope.model.model.types;
	
			//	// get selected types
			//	selected = _.filter(selected, { 'selected': true });
			//	if (selected.length <= 0) { selected = scope.model.model.types };
							
			//	_.forEach(selected, function (value, key) {
			//		data.push(value.counts);
			//		if (value.counts && value.counts.length > 0) { scope.hasData = true; }

			//		scope.series.push(value.name);
			//	});

			//	return data;
			//};

			//scope.GetSeries = function () {
			//	var series = [];
			//	var selected = scope.model.model.types;

			//	// get selected types
			//	selected = _.filter(selected, { 'selected': true });
			//	if (selected.length <= 0) { selected = scope.model.model.types };

			//	_.forEach(selected, function (value, key) {
			//		series.push(value.name);
			//	});

			//	return series;
			//};

			scope.RefreshChart = function () {

				//console.log('RefreshChart');

				scope.data = [];
				scope.series = [];
				scope.colors = [];
				scope.hasData = false;

				// get selected types
				var selected = scope.model.model.types;
				var selected = _.filter(selected, { 'selected': true });

				//console.log('selected1', selected);

				if (selected.length <= 0) { selected = scope.model.model.types };
				
				//console.log('selected', selected);

				_.forEach(selected, function (value, key) {
				
					// set flag 
					if (value.counts && value.counts.length > 0)
					{
						//console.log('add', value.name);

						var color = '#97BBCD';
						if (value.color == undefined) {
							color = '#97BBCD'
						}
						else {
							color = value.color;
							//console.log('color', color);
						}
						if (value.color == 'brown') { color = '#800000'; }
						if (value.color == 'orange') { color = '#FF4500'; }
						if (value.color == 'green') { color = '#008000'; }
						if (value.color == 'red') { color = '#FF4500'; }
						if (value.color == 'blue') { color = '#4682B4'; }

						scope.colors.push(color);  // Add color
						scope.series.push(value.name); // Add series names
						scope.data.push(value.counts); // Add counts				
						scope.hasData = true;
					}
				});

				//scope.chartOptions.chartColors = colors;

				//scope.chartOptions.colors = [];
				//: [ '#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'],
				
			}
		}
	}
})();
(function () {
	'use strict';

	angular.module('elmah.net.logger').directive('logMessageDetail', logMessageDetail);

	// dependencies
	logMessageDetail.$inject = ['$window', '$cookies', '$timeout', 'LogService', 'usSpinnerService'];

	// Package Options Directive
	function logMessageDetail($window, $cookies, $timeout, logService, usSpinnerService) {

		// Directive Settings
		return {
			restrict: 'AE',
			scope: {
				id: '@'				
			},
			templateUrl: '/app/log/directives/MessageDetail.html',
			link: link
		};
		
		// Link Function
		function link(scope, el, attrs) {

			scope.context = {};

			scope.tab = 'code';

			scope.Delete = function () {
			    usSpinnerService.spin('spinner-1');
			    logService.deleteMessage(scope.context.model.message)
					.then(function (response) { // sucess
					    usSpinnerService.stop('spinner-1');
					});
			}

			// Load Message Detail 
			scope.init = function () {

				if (scope.id) {
					logService.detail(scope.id).then(function (response) { // sucess
						//scope.model = response.model;
						//scope.context = response;						
						scope.context = response;
					});
				}
				else {
					scope.context = logService.model();
				}
			}

			// initialize
			scope.init();
		}
	}
})();