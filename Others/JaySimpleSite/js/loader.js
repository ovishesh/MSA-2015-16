document.addEventListener("DOMContentLoaded", function()
{
    var controller = document.body.getAttribute("data-controller");
    if(controller === "students")
    {
       // this stuff is working
        loadStudentTable(controller);
    }
});

// This starts loading the table
function loadStudentTable(controller)
{
    Internet.getStudents(function (studentsList)
    {
        setupStudentTable(studentsList);
    });
}

// parameter students is a list
function setupStudentTable(students)
{
    var studentTable = document.getElementById("studentTable");

    for(i=0; i < students.length; i++)
    {
        var row = document.createElement("tr");
        
        var col_lastname = document.createElement("td");
        var col_firstname = document.createElement("td");
        var col_enrol = document.createElement("td");

        col_lastname.innerHTML = students[i].LastName;
        col_firstname.innerHTML = students[i].FirstMidName;
        col_enrol.innerHTML = students[i].EnrollmentDate;

        row.appendChild(col_lastname);
        row.appendChild(col_firstname);
        row.appendChild(col_enrol);


        studentTable.appendChild(row);

    }
}