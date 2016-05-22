/////////////////////////////////////////////////////////////
// LogSearchResponse Class
/////////////////////////////////////////////////////////////
c3o.Core.Data.LogSearchResponse = function (model) {
	var self = this;

	_.extend(this, model);

	// convert message list - Transform json data to objects
	this.messages = $.map(model.messages, function (item, i) {
		return new c3o.Core.Data.LogMessage(item, model);
	});

	// Map Query
	this.query = new c3o.Core.Data.LogQuery(model.query);

	// auto select first message
	this.message = model.messages[0];
}

//// SiteContent class methods
//c3o.Core.Data.LogSearchResponse.prototype = {
//	//get Name() { return this.firstName + " " + this.lastName; },
//	//get NameLastFirst() { return this.lastName + ", " + this.firstName; }	
//}
