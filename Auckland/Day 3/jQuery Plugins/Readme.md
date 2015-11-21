#jQuery Plugins

In this tutorial, we will be creating a simple web site that uses a few jQuery plugins. 
Before beginning this tutorial, you should first read the "jQuery Plugins" Powerpoint.

## 1. Setting up the Website

Navigate to File -> New -> Web Site (NOT File -> New -> Project). Select Templates -> Visual C# -> ASP.NET Empty Web Site and give your project a meaningful name. 

![1-1](/Others/_images/jQuery Plugins/1-1.PNG)

Click OK. If you did the above correctly, you should get an empty project with Web.config.

![1-2](/Others/_images/jQuery Plugins/1-2.PNG)

## 2. Creating Folders for CSS and JavaScript

We will create a folder to where we will store the CSS. We will call this folder "css".

To do this, Right click the Website Name which should be right below the Solution Name in the Solution Explorer. Then select Add -> New Folder. Name this folder "css". This folder will contain the stylesheets of our plugins.

![2-1](/Others/_images/jQuery Plugins/2-1.PNG)

Create another folder and name it "js". This folder will contan the JavaScript files of our plugins.

## 3. Creating our HTML file

We will create an HTML file that will be displayed when we run this website.

To do this, Right click the Website Name which should be right below the Solution Name in the Solution Explorer. Then select Add -> HTML Page. Name this page "index".

![3-1](/Others/_images/jQuery Plugins/3-1.PNG)

Double click on index.html. We can see that Visual Studio generated a basic html page. It is empty but the Doctype, HTML, Head and Body tags are present. This saves us time from typing these necessary lines of code.

Write a meaningful title inside the title tags.

## 4. Including jQuery

Now let's include the jQuery (Core) library. On Day 1 of the training, you learned that you can do this by using the NuGet Package Manager to download the jQuery files locally to your project.

We will use a different way this time. We will use jQuery's Content Delivery Network. This might improve performance because the jQuery code will be hosted on different servers spread around the world instead of in a central location. Our website will try to get the jQuery library from the nearest server that is part of jQuery's CDN.

To do this, type in the following code inside the body tags:

	<!-- Include jQuery -->
    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>

Don't forget that the url starts with "http:". It will not work without "http". 

## 5. Including our first plugin: fakeLoader.js

fakeLoader.js is a lightweight animated spinner that can serve as a loading screen.

Let's get the minified JavaScript file from https://github.com/joaopereirawd/fakeLoader.js/blob/master/fakeLoader.min.js . Next, we will make an empty fakeloader.min.js in the js folder and paste the source code of fakeLoader.min.js inside.

To do this , Right click the js folder inside the Solution Explorer. Then select Add -> JavaScript File. Name this file "fakeLoader.min.js". Then paste the code from GitHub inside this file.

After that, go back to index.html. Below our code that included the jQuery library in Step 4, type in the following:

    <!-- Include the fakeLoader.js -->
    <script src="js/fakeLoader.min.js"></script>

## 6. Including the custom stylesheet for fakeLoader.js

fakeLoader.js needs the fakeLoader.css to show the animation.
	
You can get the css code from https://github.com/joaopereirawd/fakeLoader.js/blob/master/fakeLoader.css . Next, we will make an empty fakeloader.css in the css folder and paste the source code of fakeLoader.css inside.

To do this , Right click the css folder inside the Solution Explorer. Then select Add -> Style Sheet. Name this file "fakeLoader.css". Then paste the code from GitHub inside this file.

After that, go back to index.html. Inside the head tags, type in the following:

    <!-- Include the fakeLoader.css -->
    <link rel="stylesheet" type="text/css" href="css/fakeLoader.css">


## 7. Creating a div for fakeLoader.js

Create a pair of div tags just below the opening body tag. Assign that div an id which has the value of "fakeLoader".

Your code will look like this:

    <div id="fakeLoader"></div>

## 8. Use the fakeLoader method

Below our code that included the fakeLoader.js file in Step 5, type in the following:

    <!-- Using fakeLoader -->
    <script>
    	$("#fakeLoader").fakeLoader();
    </script>

This will use the default settings.

## 9. Modify the fakeLoader method

We can modify the fakeLoader method to customise our animated spinner. Change our code from Step 8 into the following:

	<!-- Using fakeLoader -->
	<script type="text/javascript">
		$("#fakeLoader").fakeLoader({
			// imagePath:"yourPath/customizedImage.gif", //If you want can you insert your custom image
			timeToHide: 1000,		// Animation lasts 1000 ms
			zIndex: 999,			// Make sure that the loader is always on top of any other elements
			spinner: "spinner2",	// Change spinner animation
			bgColor: "#5c2d91"		// Custom background colour
		});
	</script>

## 10. Include our second plugin: SweetAlert

SweetAlert has more features and is better-looking than JavaScript’s alert() method.

Let's get the minified JavaScript file from https://github.com/t4t5/sweetalert/blob/master/dist/sweetalert.min.js . Next, we will make an empty sweetalert.min.js in the js folder and paste the source code of sweetalert.min.js inside.

To do this , Right click the js folder inside the Solution Explorer. Then select Add -> Style Sheet. Name this file "sweetalert.min.js". Then paste the code from GitHub inside this file.

After that, go back to index.html. Below our code that included fakeLoader.js in Step 5, type in the following:

    <!-- Include the sweetalert JavaScript file -->
    <script src="js/sweetalert.min.js"></script>

## 11. Including the custom stylesheet for sweetalert

sweetalert needs the sweetalert.css to properly format the alert boxes.
	
You can get the css code from https://github.com/t4t5/sweetalert/blob/master/dist/sweetalert.css . Next, we will make an empty sweetalert.css in the css folder and paste the source code of sweetalert.css inside.

To do this , Right click the css folder inside the Solution Explorer. Then select Add -> Style Sheet. Name this file "sweetalert.css". Then paste the code from GitHub inside this file.

After that, go back to index.html. Inside the head tags, type in the following:

    <!-- Include the sweetalert.css -->
    <link rel="stylesheet" type="text/css" href="css/sweetalert.css">


## 12. Creating a button for sweetalert

Create a pair of button tags just below the div we made in Step 7. Assign that button an id which has the value of "sweetalert".

Your code will look like this:

    <button id="sweetalert">Log out</button>

## 13. Use the sweetalert method

Below our code that used the fakeLoader.js method in Step 8, type in the following:

    <!-- Using sweetalert -->
    <script>
    	$( "#sweetalert" ).click(function() {
  			swal( "Hello World!" );
		});
    </script>

This will use the default settings.

## 14. Modify the sweetalert method

We can modify the sweetalert method to customise our alert. Change our code from Step 13 into the following:

	<!-- Using sweetalert -->
    <script>
    	$( "#sweetalert" ).click(function() {
  			swal({   
					title: "Log out Confirmation",   
					text: "Are you sure you want to log out without saving your work?",   
					type: "warning",   
					showCancelButton: true,   
					confirmButtonColor: "#5c2d91",   
					confirmButtonText: "Yes",   
					closeOnConfirm: false }, 
					function(){   
						swal("Success", 
							"You have successfully logged out.", 
							"success"); 
					});
		});
    </script>

## 15. You are Done!
If you need to access the source code of the demo, click the jQueryPluginDemo folder.