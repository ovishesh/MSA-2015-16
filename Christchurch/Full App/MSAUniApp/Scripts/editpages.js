document.addEventListener("DOMContentLoaded", function () {

    var id = getUrlParameters("id", "", true);

    StudentModule.getStudentById(id, function (student) {
        loadForm(student);
    });

    //Setup submit button
    setupStudentSubmit(id);
    //Setup cancel buttons
    setupReturn();

});

function getUrlParameters(parameter, staticURL, decode) {
    /*
     Function: getUrlParameters
     Description: Get the value of URL parameters either from 
                  current URL or static URL
     Author: Tirumal
     URL: www.code-tricks.com
    */
    var currLocation = (staticURL.length) ? staticURL : window.location.search,
        parArr = currLocation.split("?")[1].split("&"),
        returnBool = true;

    for (var i = 0; i < parArr.length; i++) {
        parr = parArr[i].split("=");
        if (parr[0] == parameter) {
            return (decode) ? decodeURIComponent(parr[1]) : parr[1];
            returnBool = true;
        } else {
            returnBool = false;
        }
    }

    if (!returnBool) return false;
};

function loadForm(obj) {

    // Prefill form with details
    for (var key in obj) {
        if (key.toLowerCase() !== "id") {
            var forminput = document.getElementById(key + 'input');
            forminput.value = obj[key];
        }
    }

    //show form after loaded
    document.forms.edit.classList.remove("hidden");
};

function setupStudentSubmit(id) {

    //Creating student from form and update db
    var form = document.forms.edit;
    form.onsubmit = function (e) {
        e.preventDefault();
        var newStudent = {
            id: id,
            lastname: document.getElementById("LastNameinput").value,
            firstmidname: document.getElementById("FirstMidNameinput").value,
            enrollmentdate: document.getElementById("EnrollmentDateinput").value
        }

        StudentModule.updateStudent(id, newStudent, function () {
            window.location.href = "index.html";
        });
    }
};

//Go back to home without saving changes
function setupReturn() {
    document.getElementById('btncancel').addEventListener('click', function () {
        window.location.href = "index.html";
    });
}