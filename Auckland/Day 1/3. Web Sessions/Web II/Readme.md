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
	
	1. Download the initial code which has the HTML and CSS with Bootstrap - https://github.com/ovishesh/MSA-2015-16/tree/master/Others/Front%20End%20Demo
	
	To open this in Visual Studio, go File -> Open -> Web Site -> [Select the folder with the initial source code]
	
![4-1](/Others/_images/Web II/4-1.PNG)
	
	
	2. Create (if not already created) a new JavaScript file called index.js to the project and reference it in the header of index.html as shown below.
	
![4-2a](/Others/_images/Web II/4-2a.PNG)
	
	In the newly created index.js, add the following  
	
![4-2b](/Others/_images/Web II/4-2b.PNG)
	
	Anything within this block of code will be executed when the page is loaded.
	
	
	3. Create another JavaScript file called StudentModule.js. Reference this in the header of index.html like step 2.
	
	In StudentModule.js, add the following
	
![4-3a](/Others/_images/Web II/4-3a.PNG)
	
	This is the module pattern talked about (3)
	
	We now want to call the 'getStudents' function. In index.js, add a helper method named 'loadStudent()' and call it from the Event Listener. The js file should look like this
	
![4-3b](/Others/_images/Web II/4-3b.PNG)
	
	
	4. Once 'getStudent' is called, we want to make a GET request to get the data. In StudentModule.js, add the following
	The GET type means we are requesting data from a source/server
	The data type means we are expecting JSON encoded data back (if you want to read about why its 
	
![4-4](/Others/_images/Web II/4-4.PNG)
	
	5. We now want to display the data in a table. In change loadStudents() in index.js so it looks like this
	
![4-5](/Others/_images/Web II/4-5.PNG)
	
	6.  Let's display the data. Further add on to loadStudents() so it looks like this.
	For each person we need to add a row to the table (tr elements). Within each row
	we need to add td elements - these are like columns for each property
	
![4-6](/Others/_images/Web II/4-6.PNG)




