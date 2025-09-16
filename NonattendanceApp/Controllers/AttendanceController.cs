using Microsoft.AspNetCore.Mvc;
using NonattendanceApp.DTOs;
using NonattendanceApp.Repositories;

namespace NonattendanceApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : Controller
{
    private readonly  IRepository<Attendance> _attendanceRepository;
    private readonly IRepository<Class> _classRepository;
    private readonly IAttendanceRepository _iattendanceRepository;

    public AttendanceController(IRepository<Attendance> attendanceRepository, IRepository<Class> classRepository,  IAttendanceRepository iattendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
        _classRepository = classRepository;
        _iattendanceRepository = iattendanceRepository;
    }
    
    [HttpPost("AddNoAttendance")]
    public async Task<IActionResult> AddNoAttendance([FromBody] AttendanceDto attendanceDto)
    {
        if (attendanceDto == null) return BadRequest("Invalid request.");

        var attendance = new Attendance
        {
            ClassId = attendanceDto.ClassId,
            Date = attendanceDto.Date,
            StudentId = attendanceDto.StudentId
        };

        await _attendanceRepository.AddAsync(attendance);
        await _attendanceRepository.SaveAsync();

        return Ok("Attendance added successfully.");
    }

    [HttpGet("GetAttendance")]
    public async Task<IActionResult> GetAttendance([FromQuery] int classId )
    {
        var attendances = await _attendanceRepository.GetAllAsync();

        var classInfo = await _classRepository.GetByIdAsync(classId);
        if (classInfo == null)
            return NotFound("Class not found.");

        var result = attendances
            .Where(a => a.ClassId == classId)
            .Select(a => new AttendanceResponseDto
            {
                Id = a.Id,
                ClassId = a.ClassId,
                Date = a.Date,
                ClassName = classInfo.Name,
            }).ToList();

        return Ok(result);

    }
    
    [HttpGet("GetAttendanceDates/{studentId}/{classId}")]
    public async Task<IActionResult> GetAttendanceDates(int studentId, int classId)
    {
        var dates = await _iattendanceRepository.GetAttendanceDatesAsync(studentId, classId);
        return Ok(dates);
    }
        
        
    [HttpDelete("DeleteAttendance")]
    public async Task<IActionResult> DeleteAttendance([FromQuery] int classId, [FromQuery] DateTime date)
    {
        var attendances = await _attendanceRepository.GetAllAsync();
        var attendance = attendances
            .FirstOrDefault(a => a.ClassId == classId && a.Date.Date == date.Date);

        if (attendance == null)
            return NotFound("No matching attendance found.");

        _attendanceRepository.Delete(attendance);
        await _attendanceRepository.SaveAsync();

        return Ok("Attendance deleted successfully.");
    }
}