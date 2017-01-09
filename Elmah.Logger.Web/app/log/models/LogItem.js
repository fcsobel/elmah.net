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

		// add chart date for each count
		//var next = {};
		var last = {};
		_.forEach(list, function (value, key) {

			var next = { x: moment(new Date(value.name)), y: value.count };

			if (!last.x) {
				var prev = { x: moment(next.x).subtract(1, 'days'), y: 0 };
				self.counts.push(prev);
			}

			if (last.x) {
				if (next.x.diff(last.x, 'days') > 1) {
					// add trailing date
					var follow = { x: moment(last.x).add(1, 'days'), y: 0 };
					self.counts.push(follow);
				}

				if (next.x.diff(last.x, 'days') > 2) {
					// add trailing date
					var trail = { x: moment(next.x).subtract(1, 'd'), y: 0 };
					self.counts.push(trail);
				}
			}

			// add date with count
			self.counts.push(next);

			last = next;

		});

		if (self.counts.length > 0) {
			if (last.x) {
				var follow = { x: moment(last.x).add(1, 'days'), y: 0 };
				self.counts.push(follow);
			}
		}
	}

}

// SiteContent class methods
c3o.Core.Data.LogItem.prototype = {
	get Visible() { return this.visible || this.selected || (this.messages && this.messages.length > 0); }
};
