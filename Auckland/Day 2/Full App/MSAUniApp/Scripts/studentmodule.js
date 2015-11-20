var StudentModule = (function () {

    return {
        getStudents: function (callback) {
             $.ajax({ 
                type: "GET",
                dataType: "json",
                // API url here
                url: "http://msauniapp.azurewebsites.net/api/Students",
                success: function(data){        
                    console.log(data);
                    callback(data);
                }
             });
        },

        getStudentById: function (id, callback){
            
            $.ajax({ 
                type: "GET",
                dataType: "json",
                url: "http://msauniapp.azurewebsites.net/api/Students/" + id,
                success: function(data){        
                    console.log(data);
                    callback(data);
                }
             });

        },

        addStudent: function (student, callback) {
             
             $.ajax({
                url : "http://msauniapp.azurewebsites.net/api/Students/",
                type: "POST",
                data : student,
                success: function(data, textStatus, jqXHR)
                {
                    callback();
                }
             });

        },

        updateStudent: function (studentid, student, callback){
            
            $.ajax({
                url : "http://msauniapp.azurewebsites.net/api/Students/" + studentid,
                type: "PUT",
                data : student,
                success: function(data, textStatus, jqXHR)
                {
                    callback();
                }
             });
        },

        deleteStudent: function (studentid, callback) {
            
            $.ajax({ 
                type: "DELETE",
                dataType: "json",
                url: "http://msauniapp.azurewebsites.net/api/Students/" + studentid,
                success: function(data){        
                    callback();
                }
             });
        }
    };

}());