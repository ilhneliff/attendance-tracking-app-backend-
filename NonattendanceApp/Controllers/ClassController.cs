using Microsoft.AspNetCore.Mvc;
using NonattendanceApp.DTOs;
using NonattendanceApp.Repositories;

namespace NonattendanceApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassController : Controller
{
    private readonly IRepository<Class> _classRepository;

    public ClassController(IRepository<Class> classRepository)
    {
        _classRepository = classRepository;
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] ClassDto classDto)
    {
        if (classDto == null)
            return BadRequest("Class cannot be null.");

        var newClass = new Class
        {
            Name = classDto.Name
        };

        await _classRepository.AddAsync(newClass);
        await _classRepository.SaveAsync();

        return Ok(new { Message = "Class added successfully."});
    }

    // GET ALL (GET: api/class/getall)
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var classes = await _classRepository.GetAllAsync();

        var result = classes
            .Select(a => new ClassResponseDto
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        
        return Ok(result);
    }

    // GET BY ID (GET: api/class/getbyid/5)
    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var classEntity = await _classRepository.GetByIdAsync(id);

        if (classEntity == null)
            return NotFound($"Class with ID {id} not found.");

        return Ok(classEntity);
    }

    // DELETE (DELETE: api/class/delete/5)
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var classEntity = await _classRepository.GetByIdAsync(id);
        if (classEntity == null)
            return NotFound($"Class with ID {id} not found.");

        _classRepository.Delete(classEntity);
        await _classRepository.SaveAsync();

        return Ok(new { Message = "Class deleted successfully." });
    }

}