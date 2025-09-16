using NonattendanceApp.AppDb;

namespace NonattendanceApp.Repositories;

public class AttendanceRepository : Repository<Attendance> , IAttendanceRepository
{
    private readonly AppDbContext _context;
    
    public AttendanceRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<DateTime>> GetAttendanceDatesAsync(int studentId, int classId)
    {
        return _context.Attendances
            .Where(a => a.StudentId == studentId && a.ClassId == classId)
            .Select(a => a.Date)
            .ToList();
    }
}