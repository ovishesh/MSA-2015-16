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