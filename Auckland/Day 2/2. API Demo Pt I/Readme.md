#API Demo I

In this tutorial, we will be creating a Web API that manages a database of university students, courses and enrolments. 
Before beginning this tutorial, you should first read the "What's in the server side" powerpoint. This can be found in the
API Sessions folder. 

## 1. Setting up the ASP.NET Web API Project

Create a new project and navigate to Templates -> Visual C# -> Web. Select ASP.NET Web Application and give your project 
a meaningful name. 

![1-1](/Others/_images/API I/)

Select the Empty project. Make sure only "WebAPI" is selected.

![1-2](/Others/_images/API I/)

If you did the above correctly, you should get an empty project with the controller and model folders. This is where you 
will put you controllers and models.

![1-3](/Others/_images/API I/)]

## 2. Creating the Model Classes

We will be using something called Entity Framework to manage the operations between our API and the Database (saving you 
from writing your own SQL queries for common operations like inserting records in a database, retrieving records, etc). 
It will even create your tables for you!

To use Entity Framework, you first need to tell it what tables you want, and what attributes each table has. This is done 
through defining classes (models) that represent your data object. Firstly, we want to create a "Student" table that holds 
a list of students. Each student will have and ID, Name, Enrollment Date. We want to also associate a student with a list 
of enrollments to different courses, but we'll get to that later.

To do this, Right click the Models folder, then select Add -> New Item then find the template "Class". Name this class 
"Student".

![2-1](/Others/_images/API I/)]

Replace the class definition in Student.cs with

    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
	
You'll also need to add the following line at the top of your .cs file in order to use JsonIgnore:

	using Newtonsoft.Json;

Now Entity Framework (EF) will know that you want a table of Students with the follow properties. The "virtual Enrollments" 
declaration tells EF that it will be associated with a list of enrollments. It won't actually create a new column with the 
"enrollments", but it will help with the foreign key relationship later on.
 
Do the same with classes "Enrollment and "Course". The class definitions are here.

Enrollment.cs

    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        public Grade? Grade { get; set; }

        [JsonIgnore]
        public virtual Course Course { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }
    }
	
Course.cs

    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
	
Usually, the ID is automatically generated for us when we insert a new row into a table. However, the line 
[DatabaseGenerated(DatabaseGeneratedOption.None)] means that we turn off this automatic ID generation and we have to 
manually set the row IDs. The Enrollment class has a CourseID and StudentID because each student can have many 
enrollments (otherwise known as a one-to-many relationship), and each course can have many enrollments in it. The CourseID 
and StudentID are essentially there as Foreign Keys in the Enrollments table. 

BUILD your project by pressing Ctrl+Shift+B. If there are no errors, you are good to continue! 

## 3. Creating the Controllers

Now you've defined the database model, you need to setup the controllers that will respond to HTTP request from clients 
and perform the appropriate actions on the database (e.g. return a record of the student, add students, delete courses, 
etc). Fortunately, .NET is smart enough to look at your models and automatically create controllers that already have 
CRUD methods. This process is called scaffolding. 

To scaffold your controllers, right click on your "Controller" folder and click Add -> Controller. Select "Web API 2 
Controller with actions, using Entity Framework". Click Add.

![3-1](/Others/_images/API I/)

In the Add Controller dialog, select the model you want (Student to start with) and create a new Data context class 
by clicking the "+" button. You can accept the default name. Press Add. If you get an error about not having built 
your project, just go ahead and build your project.

![3-2](/Others/_images/API I/)

Now, add controllers for Enrollments and Courses the same way you added the controller for the students (right click on 
your Controllers folder > Add > Controller… etc.

## 4. Setting up your Local Database

Navigate to MSAUniversityContext.cs. This is where you will configure your database and populate the database initially. 
To start off, let’s make a class within the context to set configurations for our database. Like this: 

    public class MSAUniversityContext : DbContext
    {

        public class MyConfiguration : DbMigrationsConfiguration<MSAUniversityContext>
        {

           
        }
	}
	
You'll also need to add the following line at the top of your .cs file to allow us to use DbMigrationsConfiguration: 

	using System.Data.Entity.Migrations;

You may be wondering what the “DbMigrationsConfiguration” means. Basically, when we update the model of our database 
(maybe we added another table or attribute), the database itself isn’t smart enough to update itself based on the changes 
you made to your model. This is where automatic migrations come in. By enabling automatic migrations, we will specifically 
tell the database what our model changes were and automatically update the database based on this.  Add 
this.AutomaticMigrationsEnabled = true; to enable automatic migrations, like this:

    public class MyConfiguration : DbMigrationsConfiguration<MSAUniversityContext>
    {

        public MyConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

    }
	
By adding this line, we enable automatic migrations when the configuration is initialized. 

Next, we need to seed the database with some dummy data. This is done using the Seed method, also called in our configuration 
class. Add the following method after the public MyConfiguration() { … }:

            protected override void Seed(MSAUniversityContext context)
            {
                var students = new List<Student>
                {
                    new Student { FirstMidName = "Carson",   LastName = "Alexander",
                        EnrollmentDate = DateTime.Parse("2010-09-01") },
                    new Student { FirstMidName = "Meredith", LastName = "Alonso",
                        EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new Student { FirstMidName = "Arturo",   LastName = "Anand",
                        EnrollmentDate = DateTime.Parse("2013-09-01") },
                    new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
                        EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new Student { FirstMidName = "Yan",      LastName = "Li",
                        EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new Student { FirstMidName = "Peggy",    LastName = "Justice",
                        EnrollmentDate = DateTime.Parse("2011-09-01") },
                    new Student { FirstMidName = "Laura",    LastName = "Norman",
                        EnrollmentDate = DateTime.Parse("2013-09-01") },
                    new Student { FirstMidName = "Nino",     LastName = "Olivetto",
                        EnrollmentDate = DateTime.Parse("2005-08-11")}
                };
                students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
                context.SaveChanges();

                var courses = new List<Course>
                {
                    new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3, },
                    new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3, },
                    new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3, },
                    new Course {CourseID = 1045, Title = "Calculus",       Credits = 4, },
                    new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4, },
                    new Course {CourseID = 2021, Title = "Composition",    Credits = 3, },
                    new Course {CourseID = 2042, Title = "Literature",     Credits = 4, }
                };
                courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
                context.SaveChanges();

                var enrollments = new List<Enrollment>
                {
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alexander").ID,
                        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                        Grade = Grade.A
                    },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alexander").ID,
                        CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                        Grade = Grade.C
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alexander").ID,
                        CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                         StudentID = students.Single(s => s.LastName == "Alonso").ID,
                        CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                         StudentID = students.Single(s => s.LastName == "Alonso").ID,
                        CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alonso").ID,
                        CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Anand").ID,
                        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Anand").ID,
                        CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                        Grade = Grade.B
                     },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                        CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Li").ID,
                        CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Justice").ID,
                        CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                        Grade = Grade.B
                     }
                };

                foreach (Enrollment e in enrollments)
                {
                    var enrollmentInDataBase = context.Enrollments.Where(
                        s =>
                             s.Student.ID == e.StudentID &&
                             s.Course.CourseID == e.CourseID).SingleOrDefault();
                    if (enrollmentInDataBase == null)
                    {
                        context.Enrollments.Add(e);
                    }
                }
                context.SaveChanges();
            }

			
So there’s one last step: We need to make sure the database is being initialized with the configurations in the 
configuration class we just made, like this:

        public MSAUniversityContext() : base("name=MSAUniversityContext")
        {
            if (!Database.Exists("MSAUniversityContext"))
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<MSAUniversityContext, MyConfiguration>());
            }
        }

Now run your web app, it’ll come up with HTTP Error 403.14 - Forbidden. but your database is actually being initialized 
in the background (although you can’t see it right now). To view the data in your database, we’re going to use a tool 
called Swagger. 

## 5. Setting up Swagger (API Testing)

You can play around with your API by either using an HTTP debugging client like Postman (Chrome extension), or browser 
(limited to GET requests). A good way to both document and test your API is to use Swagger. Swagger will go through your 
models and controllers and generate documentation (inputs, outputs, etc) as well as provide you with a way to test out 
different CRUD operations (POST, GET, etc). To read more about this, see https://cmatskas.com/webapi-documentation-done-right-with-swagger/
 
To setup Swagger, simply right click your project, click “Manage NuGet Packages…”, and search for “Swashbuckle”. 
Install this and you’re done! 

![5-1](/Others/_images/API I/)

To use swagger, simply build and run your project locally. When your browser opens up, you’ll probably get some sort of 
error that resembles this. HTTP Error 403.14 - Forbidden. That’s okay! To access swagger, all you have to do is append 
the URL with /swagger. E.g. http://localhost:51731/swagger. 

![5-2](/Others/_images/API I/)

Here you can test the different CRUD operations from within your browser. For example, expand the “Students” row and select 
“GET”. Click “Try it out!” to execute the GET request. Give it about a minute and it should return to you a list of 
students in JSON format, if everything goes well. 

![5-3](/Others/_images/API I/)
