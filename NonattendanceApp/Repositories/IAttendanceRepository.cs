namespace NonattendanceApp.Repositories;

public interface IAttendanceRepository : IRepository<Attendance>
{
    Task<List<DateTime>> GetAttendanceDatesAsync(int studentId, int classId);
}