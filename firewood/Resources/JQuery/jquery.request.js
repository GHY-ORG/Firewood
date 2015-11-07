$.extend({
	ajaxJsonRequest: function (type, obj) {
		var send = obj;
		send.type = type;
		send.contentType = "application/json";
		send.dataType = "json";
		this.ajax(send);
	},
	ajaxJsonPost: function (obj) {
		this.ajaxJsonRequest("post", obj);
	},
	ajaxJsonGet: function (obj) {
		this.ajaxJsonRequest("get", obj);
	}
});