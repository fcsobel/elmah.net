(function () {

    angular.module('c3o.core')

		.factory('LogService', ['$q', 'LoggerApi', 'ErrorService', function ($q, loggerApi, errorService) {

			// Context Object - Model  
			var container = {
				model: { messages: [] }
			};

			filter = function (value, index, array) {

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

			// Search Loge Messages
			search = function (log, limit, span, logs, applications, severities, types, sources, users, start, end) {
			    var promise = loggerApi.search(log, limit, span, logs, applications, severities, types, sources, users, start, end)
					.then(function (response) { // handle response
						// convert message list - Transform json data to objects
						response.messages = $.map(response.messages, function (item, i) {
						    return new c3o.Core.Data.LogMessage(item, response);
						});

						// reset model with serach results
						container.model = response;

						// auto select first message
						container.model.message = container.model.messages[0];

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
			detail = function (id) {
				var promise = loggerApi.detail(id)
					// Handle message Response
					.then(function (response) {

						// convert response message to message object
					    var message = new c3o.Core.Data.LogMessage(response, container.model);

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
			update = function (obj) {
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
				detail: detail,
				//search: function (log, severity, limit, span, types, sources, start, end) { return search(log, severity, limit, span, types, sources, start, end); },
				search: search,
				update: function (obj) { return update(obj); }
			};
		}
		]);
}());