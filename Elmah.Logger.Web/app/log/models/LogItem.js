/////////////////////////////////////////////////////////////
// LogQuery Class
/////////////////////////////////////////////////////////////
c3o.Core.Data.LogItem = function (model, countData) {
	var self = this;

	_.extend(this, model);

	this.visible = false;
	this.messages = this.messages || [];

	this.counts = [];

	if (countData)
	{
		// get counts for this type
		var list = _.filter(countData, { id: this.id });

		_.forEach(list, function (value, key) {
			self.counts.push({ x: moment(new Date(value.name)), y: value.count })
		});
	}

}

// SiteContent class methods
c3o.Core.Data.LogItem.prototype = {    
    get Visible() { return this.visible || this.selected || (this.messages && this.messages.length > 0); }
}
