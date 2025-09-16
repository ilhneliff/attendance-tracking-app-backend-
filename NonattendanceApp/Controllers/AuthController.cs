using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.Configuration;
using NonattendanceApp.DTOs;
using NonattendanceApp.Repositories;
using NonattendanceApp.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRepository<Student> _studentRepository;
    private readonly ITokenService _tokenService;

    public AuthController(IRepository<Student> studentRepository,  ITokenService tokenService)
    {
        _studentRepository = studentRepository;
        _tokenService = tokenService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] StudentDto studentDto)
    {
        // Email zaten varsa hata ver
        var allStudents = await _studentRepository.GetAllAsync();
        if (allStudents.Any(s => s.Email == studentDto.Email))
            return BadRequest("Email already exists.");

        var student = new Student
        {
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName,
            Email = studentDto.Email,
            Password = studentDto.Password
        };

        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveAsync();

        return Ok(new { Message = "Student registered successfully." });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var allStudents = await _studentRepository.GetAllAsync();
        var student = allStudents.FirstOrDefault(s => s.Email == login.Email && s.Password == login.Password);

        if (student == null)
            return Unauthorized("Invalid email or password.");

        var tokenString = _tokenService.CreateToken(student);

        return Ok(new
        {
            Token = tokenString,
            StudentId = student.Id
        });
    }
}