// When the document is loaded, the DOMContentLoaded event occurs 
// We execute all our other logic after this occurs.
document.addEventListener("DOMContentLoaded", function () {
    console.log("DOM loaded and ready to go!");
    loadStudents();
});

// Used to load the student data - can actually do without this function
// if you are not doing anything else in the load.
function loadStudents() {
    StudentModule.getStudents(setupStudentsTable);
}

// This is the function we pass into StudentModule as the callback
// It takes the data returned from the API call (studenList) and an input
function setupStudentsTable(studentsList) {
    
    // Get a reference to the table body so we can add our rows in
    var studentTable = document.getElementById("studentList");

    // Loop through all the student/name objects 
    for (var i = 0; i < studentsList.length; i++) {
        //Create a new row element
        var row = document.createElement("tr");

        //Create our data cells and append to row
        var firstNameCol = document.createElement("td");
        firstNameCol.innerHTML = studentsList[i].name;
        row.appendChild(firstNameCol);

        var lastNameCol = document.createElement("td");
        lastNameCol.innerHTML = studentsList[i].surname;
        row.appendChild(lastNameCol);

        var country = document.createElement("td");
        country.innerHTML = studentsList[i].country;
        row.appendChild(country);

        // Append our rows to the table 
        studentTable.appendChild(row);


    }

}