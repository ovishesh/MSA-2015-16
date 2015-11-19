#Web II

We are going to use AJAX to make calls to the API, and then display the retrieved data to the front end. There are 3 main concepts you will need to grasp:

## 1. AJAX

Read this and look through the first few steps of the tutorial to understand what AJAX is, and how AJAX is used to make calls to your API. It can be a little confusing initially:
http://www.w3schools.com/ajax/default.asp
http://www.tutorialspoint.com/ajax/what_is_ajax.htm
http://www.onextrapixel.com/2012/02/06/a-beginners-guide-to-using-ajax-in-your-website/

## 2. Dynamically displaying data using JavaScript 

Check out how elements are dynamically added in javascript:
http://www.mysamplecode.com/2012/04/generate-html-table-using-javascript.html
For our case, we will need to dynamically add table rows. You will need this when displaying your loaded data.

## 3. Module pattern (optional, but good practice)

We will follow the module pattern in making AJAX calls to the API. Read the following links to get an understanding of the module pattern:
http://bjarneo.codes/module-pattern-javascript/
http://javascriptplayground.com/blog/2012/04/javascript-module-pattern/
https://css-tricks.com/how-do-you-structure-javascript-the-module-pattern-edition/

Essentially, we will have a module that's dedicated to making calls to the API so that our code remains tidy and easily understandable. The structure of the module will look something like this:

![3](/Others/_images/Web II/3.PNG)

## 4. Hands-on Exercise

1a - Create a new Empty ASP.NET Web Application Project, and check the option for Web API 
1b - Alternatively Download the initial project which has the HTML and CSS with Bootstrap - https://github.com/ovishesh/MSA-2015-16/tree/master/Auckland/Day%201/3.%20Web%20Sessions/Web%20II/MSAWebApp


2- Create (if not already created) a new JavaScript file called index.js to the project and reference it in the header of index.html as shown below.

	<script src="Scripts/index.js"></script>

In the newly created index.js, add the following  

	// This event triggers on page load
	document.addEventListener("DOMContentLoaded", function () {
	});

Anything within this block of code will be executed when the page is loaded.


3- Create another JavaScript file called StudentModule.js. Reference this in the header of index.html like step 2.

	<script src="Scripts/StudentModule.js"></script> 

In StudentModule.js, add the following

	var StudentModule = (function () {
		// Return anything that you want to expose outside the closure
		return {
			getStudents: function (callback) {  
	
			}
		};
	}());

This is the module pattern talked about (3)

We now want to call the 'getStudents' function. In index.js, add a helper method named 'loadStudent()' and call it from the Event Listener. The js file should look like this

	document.addEventListener("DOMContentLoaded", function () {
		loadStudents();
	});
	
	function loadStudents(){
		StudentModule.getStudents();
	}

4- Once 'getStudent' is called, we want to make a GET request to get the data. In StudentModule.js, add the following
The GET type means we are requesting data from a source/server
The data type means we are expecting JSON encoded data back (if you want to read about why its 

	// Return anything that you want to expose outside the closure
	return {
		getStudents: function (callback) {
	
			$.ajax({ 
				type: "GET",
				dataType: "jsonp",
				url: "http://api.uinames.com/?amount=25",
				success: function(data){        
					console.log(data);
					callback(data);
				}
			});
			
		}
	};

5- We now want to display the data in a table. In change loadStudents() in index.js so it looks like this

	function loadStudents(){
		
		// We need a reference to the div/table that we are going to chuck our data into
		var studentsTable = document.getElementById("tblstudentcontent");
	
		StudentModule.getStudents(function (studentsList) {
			setupStudentsTable(studentsList);
		});
		
		// This is the callback for when the data comes back in the studentmodule
		function setupStudentsTable(students) {
	
		}
	}

6-  Let's display the data. Further add on to loadStudents() so it looks like this.
For each person we need to add a row to the table (tr elements). Within each row
we need to add td elements - these are like columns for each property

	function loadStudents(){
		
		// We need a reference to the div/table that we are going to chuck our data into
		var studentsTable = document.getElementById("tblstudentcontent");
	
		StudentModule.getStudents(function (studentsList) {
			setupStudentsTable(studentsList);
		});
		
		// This is the callback for when the data comes back in the studentmodule
		function setupStudentsTable(students) {
			console.log(students);
			for (i = 0; i < students.length; i++) {
				
				// Create row 
				var row = document.createElement('tr');
	
				// Add the columns in the row (td / data cells)
				var lastnamecol = document.createElement('td');
				lastnamecol.innerHTML = students[i].surname;
				row.appendChild(lastnamecol);
	
				var firstnamecol = document.createElement('td');
				firstnamecol.innerHTML = students[i].name;
				row.appendChild(firstnamecol);
	
				var enrollmentdatecol = document.createElement('td');
				enrollmentdatecol.innerHTML = students[i].country;
				row.appendChild(enrollmentdatecol);
				
				// Append the row to the end of the table
				studentsTable.appendChild(row);
	
			}
		}
	}


