/////////////////////////////////////////////////////////////
// LogQuery Class
/////////////////////////////////////////////////////////////
Elmah.Net.Models.LogQuery = function (model) {
    var self = this;

    _.extend(this, model);

    if (model.start && model.end) {
        model.startMoment = moment(model.start);
        model.endMoment = moment(model.end);
    }
};

// SiteContent class methods
//Elmah.Net.Models.Query.prototype = {}
