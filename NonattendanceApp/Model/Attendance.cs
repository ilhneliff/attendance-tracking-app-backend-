public class Attendance
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int ClassId { get; set; }
    public Class Class { get; set; } 
}