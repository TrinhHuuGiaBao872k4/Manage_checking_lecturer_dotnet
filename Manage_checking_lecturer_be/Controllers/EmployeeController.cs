using Microsoft.AspNetCore.Mvc;
using MongoElearn.Services;
using MongoElearn.Models;
using MongoElearn.DTOs.Employees;
using MongoElearn.Api.Common;

namespace MongoElearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _svc;
    public EmployeesController(IEmployeeService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<HTTPResponseClient<List<Employee>>>> GetAll()
    {
        var list = await _svc.GetAllAsync();
        return Ok(HTTPResponseClient<List<Employee>>.Ok(list));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HTTPResponseClient<Employee>>> GetById(string id)
    {
        var e = await _svc.GetByIdAsync(id);
        return e is null
            ? NotFound(HTTPResponseClient<Employee>.Fail("Employee not found", 404))
            : Ok(HTTPResponseClient<Employee>.Ok(e));
    }

    [HttpGet("by-email")]
    public async Task<ActionResult<HTTPResponseClient<Employee>>> GetByEmail([FromQuery] string email)
    {
        var e = await _svc.GetByEmailAsync(email);
        return e is null
            ? NotFound(HTTPResponseClient<Employee>.Fail("Employee not found", 404))
            : Ok(HTTPResponseClient<Employee>.Ok(e));
    }

    [HttpGet("search")]
    public async Task<ActionResult<HTTPResponseClient<List<Employee>>>> Search([FromQuery] string? keyword, [FromQuery] bool? isActive, [FromQuery] string? role)
    {
        var list = await _svc.SearchAsync(keyword, isActive, role);
        return Ok(HTTPResponseClient<List<Employee>>.Ok(list));
    }

    [HttpPost]
    public async Task<ActionResult<HTTPResponseClient<Employee>>> Create([FromBody] EmployeeCreateDto dto)
    {
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.id },
            HTTPResponseClient<Employee>.Ok(created, "Created", 201));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Update(string id, [FromBody] EmployeeUpdateDto dto)
    {
        var ok = await _svc.UpdateAsync(id, dto);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Updated"))
            : NotFound(HTTPResponseClient<string>.Fail("Employee not found", 404));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Delete(string id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Deleted"))
            : NotFound(HTTPResponseClient<string>.Fail("Employee not found", 404));
    }
}
