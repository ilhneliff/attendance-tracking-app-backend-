namespace NonattendanceApp.DTOs;

public class AttendanceResponseDto
{
    public int Id { get; set; }
    public int ClassId { get; set; }
    public DateTime Date { get; set; }
    public string ClassName { get; set; }
}