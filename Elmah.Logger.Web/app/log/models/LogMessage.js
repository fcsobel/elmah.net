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
};

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
