var REX = REX || {};
REX.auth = REX.auth || {};
REX.auth.providers = [];

// Initialize login providers
REX.auth.init = function (name, app, scope) {

	// Add provider 
	var provider = { name: name, app: app, scope: scope };
	REX.auth.providers.push(provider);

	// Initialize login provider script and callback
	switch (name) {

		// Setup Google
		case 'Google':
			// configuration callback
			window.initGoogle = function () {
				// Check access without popup to avoid popup blocker?
				//window.setTimeout(REX.auth.Login(name, null, true), 1);
				//http://stackoverflow.com/questions/26932435/why-window-settimeout-throws-an-exception-in-ie9				
				window.setTimeout(function() {
					REX.auth.Login(name, null, true); 
				},1);
					
			};

			// inject script with onload call to googleAsyncInit();
			REX.auth.addScript(document, 'script', 'google-jssdk', 'https://apis.google.com/js/client:platform.js?onload=window.initGoogle');
			break;

		// Setup Facebook 			
		case 'Facebook':
			// configuration callback
			window.fbAsyncInit = function () {
				// Initialize fb api with our app id - Upgrade from 2.1 to 2.4 probably not necessary but just in case
				FB.init({ appId: app, xfbml: true, version: 'v2.4' });

				// Call check access auth without popup to avoid popup blocker?  Suggested by google docs
				//window.setTimeout(REX.auth.Login(name, null, true), 1);
				//http://stackoverflow.com/questions/26932435/why-window-settimeout-throws-an-exception-in-ie9
				window.setTimeout(function() {
					REX.auth.Login(name, null, true); 
				},1);
			};

			// inject script callback = fbAsyncInit();
			REX.auth.addScript(document, 'script', 'facebook-jssdk', '//connect.facebook.net/en_US/sdk.js');
			break;

		// oauth redirect stuff
		case 'OAuth':
			window.setAuthData = function(data)
			{
				alert(data.success);
				window.location.reload();
			}

	}
}

// external provider login 
REX.auth.Login = function (name, callback, check) {

	// find provider object
	var provider = {};
	for (index = 0; index < REX.auth.providers.length; ++index) {
		if (REX.auth.providers[index].name == name) { provider = REX.auth.providers[index]; }
	}

	switch (name) {

		// Process Google login
		case 'Google':
			// Use response_type: 'code', redirect_uri: 'postmessage' to get auth code which is exchanged for token
			// http://stackoverflow.com/questions/11485271/google-oauth-2-authorization-error-redirect-uri-mismatch
			// If you're using google javascript api without redirect_uri use 'postmessage' instead of actual URI. 
			var params = { client_id: provider.app, scope: provider.scope || 'email', response_type: 'code', redirect_uri: 'postmessage' };

			// get back client token which needs to be validated on the server
			//var params = { client_id: provider.app, scope: provider.scope || 'email' };

			// Note:The first call to gapi.auth.authorize can trigger popup blockers. 
			// To prevent popup blocker call gapi.auth.authorize with immediate: false parameter before 
			if (check) { params.immediate = true; };

			gapi.auth.authorize(params,
					function (response) {

						if (response.status) {
							provider.authenticated = true;
							provider.token = response.access_token;
							provider.code = response.code;

							// process login
							if (!check) { REX.auth.ExternalProviderLogin(provider, callback); }
						}
					});
			break;

		// process facebook login
		case 'Facebook':
			var params = { scope: provider.scope || 'public_profile,email' };  //, response_type: 'code' // has no effect

			if (check) {
				// only check login status
				FB.getLoginStatus(function (response) {
					if (response.status === 'connected') {
						provider.authenticated = true;
						provider.token = response.authResponse.accessToken;
					}
				});
			}
			else {
				// process external login
				FB.login(function (response) {

					if (response.status === 'connected') {

						// Logged into your app and Facebook.
						provider.authenticated = true;
						provider.token = response.authResponse.accessToken;

						// process login							
						REX.auth.ExternalProviderLogin(provider, callback);

					} else if (response.status === 'not_authorized') {
						// The person is logged into Facebook, but NOT your app.
					} else {
						// The person is not logged into Facebook, so we're not sure if
						// they are logged into this app or not.
					}
				},
						params);
			}
			//});
			break;

		default:
			// Show asp.net identity oauth in popup window
			$.oauthpopup({
				path: '/Account/ExternalLogin?provider=' + name + '&returnUrl=[close]',
				callback: function () {

					// callback to finish process					
					provider.isLinked = true;
					callback(provider);
				}
			});
			break;
	}
};


// Process external provider login
REX.auth.ExternalProviderLogin = function (provider, callback) {

	//// start spinner
	////REX.ui.spin.start();
	//$.ajax({
	//	type: "GET",
	//	//url: '/signin-' + provider.name + '?state=' + provider.token + '&code=' + provider.code,
	//	url: '/signin-' + provider.name + '?code=' + provider.code,
	//	//data: { token: provider.token, code: provider.code },
	//	success: function (response) {
	//		provider.isLinked = response.success;
	//		provider.message = response.message;

	//		// stop spinner
	//		//REX.ui.spin.stop();

	//		callback(provider);
	//	},
	//	complete: function(response) {
	//		// stop spinner
	//		//REX.ui.spin.stop();
	//	}
	//});


	$.ajax({
		type: "POST",
		url: '/account/ExternalProviderLogin/' + provider.name,
		data: { token: provider.token, code: provider.code },
		success: function (response) {
			provider.isLinked = response.success;
			provider.message = response.message;

			// stop spinner
			//REX.ui.spin.stop();

			callback(provider);
		},
		complete: function(response) {
			// stop spinner
			//REX.ui.spin.stop();
		}
	});
}


// Select Provider Profile as default
REX.auth.ExternalProviderSelect = function (name, callback) {

	// start spinner
	REX.ui.spin.start();

	$.ajax({
		type: "POST",
		url: '/account/SelectProvider/' + name,
		data: {  },
		success: function (response) {

			// stop spinner
			REX.ui.spin.stop();

			callback(response);
		},
		complete: function(response) {

			// stop spinner
			REX.ui.spin.stop();			
		}
	});
}

// Button clicks
$(function () {

	// External provider login
	$('.provider-login').click(function (e) {
		e.preventDefault();
		e.stopPropagation();

		var name = $(this).data('provider');
		var returnUrl = $(this).data('return');
		var form = $(this).closest("form");

		REX.auth.Login(name,
				function (provider) {

					//console.log('callback', provider)

					if (provider.isLinked) {
						if (returnUrl) {
							if (returnUrl == '.') {
								window.location.reload();
							}
							else {
								window.location = returnUrl;
							}
						}
						else {
							//window.location = '/';
						}
					}
					else {
						// display validation message
						var validator = $("#externalLogin").validate();
						validator.showErrors({ "SocialLogin": provider.message })
					}
				}
			);
	});

	// Select Profile provider
	$('.provider-select').click(function (e) {

		e.preventDefault();
		e.stopPropagation();

		var name = $(this).data('provider');
		var returnUrl = $(this).data('return');

		//console.log(name, returnUrl);

		REX.auth.ExternalProviderSelect(name,
				function (response) {
					if (response.success) {
						if (returnUrl == '.') {
							window.location.reload();
						}
						else {
							window.location = returnUrl;
						}
					}
					else {
						var validator = $("#externalLogin").validate();
						if (!validator) {
							validator = form.validate();
						}
						if (validator) {
							validator.showErrors({ "SocialLogin": response.message });
						}
					}

				}
			);
	});

});

// taken from fb script inject code
REX.auth.addScript = function (d, s, id, src) {
	var js, fjs = d.getElementsByTagName(s)[0];
	if (d.getElementById(id)) { return; }
	js = d.createElement(s); js.id = id;
	js.src = src;
	fjs.parentNode.insertBefore(js, fjs);
}