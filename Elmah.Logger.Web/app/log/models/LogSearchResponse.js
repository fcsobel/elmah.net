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
