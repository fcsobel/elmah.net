/////////////////////////////////////////////////////////////
// Site Error
/////////////////////////////////////////////////////////////
c3o.Core.Data.SiteError = function (error, cause, message, key, type) {
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