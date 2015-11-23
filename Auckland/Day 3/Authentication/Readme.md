# Authentication - Client Side

Overview of Authentication in API Apps/Mobile Apps https://azure.microsoft.com/en-us/documentation/articles/app-service-authentication-overview/ 

## Setting up Facebook app

There are a few changes we need to make to make your API work with the frontend code:

1. Navigate to https://developers.facebook.com and sign in. 

2. Click My Apps -> Add a New App
![2-1](/Others/_images/Authentication/Step 1.PNG)

3. Select 'Websites'
![3-1](/Others/_images/Authentication/Step 2.PNG)

4. Click 'Skip and Create App ID'
![4-1](/Others/_images/Authentication/Step 3.PNG)

5. Choose a display name, select a category for your app, then click 'Create App ID'
![5-1](/Others/_images/Authentication/Step 4.PNG)

6. From the side menu click 'Settings'
![6-1](/Others/_images/Authentication/Step 5.PNG)

7. Note your App ID, you'll need it later. Add your contact email address then click '+Add Platform' to add your website's URL
![7-1](/Others/_images/Authentication/Step 6.PNG)

8. From the popup select 'Websites'
![8-1](/Others/_images/Authentication/Step 7.PNG)

9. Next enter the URL to your website in the 'Site URL' field as well as in the 'App Domains' field.
![9-1](/Others/_images/Authentication/Step 8.PNG)

10. Click save then go into your project in Visual Studio and create a new JavaScript file called 'facebooklogin.js'

11. Next replace any code that might have been generated with the following code:

```
	// This is called with the results from from FB.getLoginStatus().
	function statusChangeCallback(response) {
		console.log('statusChangeCallback');
		console.log(response);
		// The response object is returned with a status field that lets the
		// app know the current login status of the person.
		// Full docs on the response object can be found in the documentation
		// for FB.getLoginStatus().
		if (response.status === 'connected') {
			// Logged into your app and Facebook.
			testAPI();
		} else if (response.status === 'not_authorized') {
			// The person is logged into Facebook, but not your app.
			document.getElementById('status').innerHTML = 'Please log ' +
				'into this app.';
		} else {
			// The person is not logged into Facebook, so we're not sure if
			// they are logged into this app or not.
			document.getElementById('status').innerHTML = 'Please log ' +
				'into Facebook.';
		}
	}

	// This function is called when someone finishes with the Login
	// Button.  See the onlogin handler attached to it in the sample
	// code below.
	function checkLoginState() {
		FB.getLoginStatus(function (response) {
			statusChangeCallback(response);
		});
	}

	window.fbAsyncInit = function () {
		FB.init({
			//******************************************************************************//
			appId: 'REPLACE-WITH-YOUR-FACEBOOK'S APP ID',
			//*****************************************************************************//
			cookie: true,  // enable cookies to allow the server to access 
			// the session
			xfbml: true,  // parse social plugins on this page
			version: 'v2.2' // use version 2.2
		});

		// Now that we've initialized the JavaScript SDK, we call 
		// FB.getLoginStatus().  This function gets the state of the
		// person visiting this page and can return one of three states to
		// the callback you provide.  They can be:
		//
		// 1. Logged into your app ('connected')
		// 2. Logged into Facebook, but not your app ('not_authorized')
		// 3. Not logged into Facebook and can't tell if they are logged into
		//    your app or not.
		//
		// These three cases are handled in the callback function.

		FB.getLoginStatus(function (response) {
			statusChangeCallback(response);
		});

	};

	// Load the SDK asynchronously
	(function (d, s, id) {
		var js, fjs = d.getElementsByTagName(s)[0];
		if (d.getElementById(id)) return;
		js = d.createElement(s); js.id = id;
		js.src = "http://connect.facebook.net/en_US/sdk.js";
		fjs.parentNode.insertBefore(js, fjs);
	}(document, 'script', 'facebook-jssdk'));

	// Here we run a very simple test of the Graph API after login is
	// successful.  See statusChangeCallback() for when this call is made.
	function testAPI() {
		console.log('Welcome!  Fetching your information.... ');
		FB.api('/me', function (response) {
			console.log('Successful login for: ' + response.name);
			
			// This line adds text to the div tag with the id of 'status'
			// to show the user they're currently logged in.
			document.getElementById('status').innerHTML =
				'Thanks for logging in, ' + response.name + '!';
		});
	}
```
	
Please make sure you enter your App's ID in the field above where is says 'REPLACE-WITH-YOUR-FACEBOOK'S APP ID'

12. Next add reference to facebooklogin.js at the top of your index.html like you've done many times before.

13. Now to log users in add the following code somewhere in the bottom of your html page:

	<fb:login-button scope="public_profile,email" onlogin="checkLoginState();"></fb:login-button>
	
'scope' is used to ask the user permission to assess more information. By default the when the user logs in you are given permission to view the user's public profile and that's it.
Beyond that, the user can reject any additional permissions. 
	
14. Add this next line to display to the user they're currently logged in:

	<div id="status"></div>
	
The above line is used to display a message to the user informing them they're currently logged in.

15. Now if you look at console you should see the user's id, assess token and their name. You can now use Facebook's JavaScript SDK to access user's information.

For more info on using Facebook's JavaScript SDK: https://developers.facebook.com/docs/javascript

16. Now public to Azure and enjoy!