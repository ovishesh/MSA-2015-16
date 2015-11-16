document.addEventListener("DOMContentLoaded", function () {
    setupStudentSubmit();
    setupReturn();
});

function setupStudentSubmit() {

    //Creating student from form parameters

    var form = document.forms.create;
    form.onsubmit = function (e) {
        e.preventDefault();
        var newStudent = {
            lastname: document.getElementById("LastNameinput").value,
            firstmidname: document.getElementById("FirstNameinput").value,
            enrollmentdate: document.getElementById("EnrollmentDateinput").value
        }
        StudentModule.addStudent(newStudent, function () {
            window.location.href = "index.html";
        });
    }

};


function setupReturn() {

    document.getElementById('btncancel').addEventListener('click', function () {
        window.location.href = "index.html";
    });
}