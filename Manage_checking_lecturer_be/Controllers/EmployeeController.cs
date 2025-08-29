// Controllers/EmployeesController.cs
using Microsoft.AspNetCore.Mvc;
using MongoElearn.Models;
using MongoElearn.Services;
using MongoElearn.DTOs.Employees; // nơi bạn để EmployeeCreateDto/UpdateDto/ViewDto
using System.Linq;

namespace MongoElearn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _svc;
    public EmployeesController(IEmployeeService svc) => _svc = svc;

    // map domain -> view dto (ẩn password)
    private static EmployeeViewDto ToView(Employee e) => new(
        e.id,
        e.code,
        e.email,
        e.fullname,
        e.current_school,
        e.internship_position,
        e.internship_start_time,
        e.internship_end_time,
        e.Skills,                     
        e.role ?? new List<string>(),
        e.isActive,
        e.manager
    );

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeViewDto>>> GetAll()
        => Ok((await _svc.GetAllAsync()).Select(ToView));

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeViewDto>> Get(string id)
    {
        var emp = await _svc.GetByIdAsync(id);
        return emp is null ? NotFound() : Ok(ToView(emp));
    }

    // GET api/employees/search?q=alice&active=true&role=Intern
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<EmployeeViewDto>>> Search([FromQuery] string? q, [FromQuery] bool? active, [FromQuery] string? role)
    {
        var list = await _svc.SearchAsync(q, active, role);
        return Ok(list.Select(ToView));
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeViewDto>> Create([FromBody] EmployeeCreateDto dto)
    {
        try
        {
            var emp = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = emp.id }, ToView(emp));
        }
        catch (InvalidOperationException ex)           // ví dụ duplicate code/email
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] EmployeeUpdateDto dto)
        => await _svc.UpdateAsync(id, dto) ? NoContent() : NotFound();

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => await _svc.DeleteAsync(id) ? NoContent() : NotFound();
}
