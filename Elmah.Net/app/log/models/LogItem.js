/////////////////////////////////////////////////////////////
// LogQuery Class
/////////////////////////////////////////////////////////////
c3o.Core.Data.LogItem = function (model) {
	var self = this;

	_.extend(this, model);

	this.visible = false;
	this.messages = this.messages || [];
}

// SiteContent class methods
c3o.Core.Data.LogItem.prototype = {    
    get Visible() { return this.visible || this.selected || (this.messages && this.messages.length > 0); }
}
