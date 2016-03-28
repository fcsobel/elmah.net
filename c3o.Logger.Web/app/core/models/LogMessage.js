/////////////////////////////////////////////////////////////
// LogMessage Class
/////////////////////////////////////////////////////////////
c3o.Core.Data.LogMessage = function (data, model) {
	var self = this;

	_.extend(this, data);
		
	this.log = this.log || _.find(model.logs, { id: this.logId });
	this.application = this.application || _.find(model.applications, { id: this.applicationId });
	this.user = this.user || _.find(model.users, { id: this.userId });
	this.messageType = this.messageType || _.find(model.types, { id: this.messageTypeId });
	this.source = this.source || _.find(model.sources, { id: this.sourceId });
	var severityObj = _.find(model.severities, { name: this.severity });

	if (this.log) {
		this.log.messages = this.log.messages || [];
		this.log.messages.push(this);
	};

	if (this.application) {
		this.application.messages = this.application.messages || [];
		this.application.messages.push(this);
	};

	if (this.user) {
		this.user.messages = this.user.messages || [];
		this.user.messages.push(this);
	};

	if (this.messageType) {
		this.messageType.messages = this.messageType.messages || [];
		this.messageType.messages.push(this);
	};

	if (this.source) {
		this.source.messages = this.source.messages || [];
		this.source.messages.push(this);
	};

	if (severityObj) {
		severityObj.messages = severityObj.messages || [];		
		severityObj.messages.push(this);
	};
}

// SiteContent class methods
c3o.Core.Data.LogMessage.prototype = {
	//get Name() { return this.firstName + " " + this.lastName; },
	//get NameLastFirst() { return this.lastName + ", " + this.firstName; }
	
}
