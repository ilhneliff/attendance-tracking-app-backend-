namespace NonattendanceApp.DTOs;

public class NoAttendanceRequest
{
    public int ClassId  { get; set; }
    public int ClassName { get; set; }
    public DateTime Date { get; set; }
}