/////////////////////////////////////////////////////////////
// LogQuery Class
/////////////////////////////////////////////////////////////
c3o.Core.Data.LogQuery = function (model) {
	var self = this;

	_.extend(this, model);

	if (model.start && model.end) {
		model.startMoment = moment(model.start);
		model.endMoment = moment(model.end);
	}
}

// SiteContent class methods
//c3o.Core.Data.Query.prototype = {}
