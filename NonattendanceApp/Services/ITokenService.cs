namespace NonattendanceApp.Services;

public interface ITokenService
{
    string CreateToken(Student student);
}