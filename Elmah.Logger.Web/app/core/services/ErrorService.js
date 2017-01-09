(function () {

    angular.module('c3o.core')
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
			    var siteError = new c3o.Core.Data.SiteError(error, cause, message, key, type);

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
    //c3o.Core.Data.SiteError = function (error, cause, message, key, type) {
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