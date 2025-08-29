using Microsoft.AspNetCore.Mvc;
using MongoElearn.Models;
using MongoElearn.Services;

namespace MongoElearn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _svc;
    public EmployeeController(EmployeeService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
        => Ok(await _svc.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> Get(string id)
    {
        var emp = await _svc.GetByIdAsync(id);
        return emp is null ? NotFound() : Ok(emp);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(Employee doc)
    {
        await _svc.CreateAsync(doc);
        return CreatedAtAction(nameof(Get), new { id = doc.id }, doc);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Employee doc)
        => await _svc.UpdateAsync(id, doc) ? NoContent() : NotFound();

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => await _svc.DeleteAsync(id) ? NoContent() : NotFound();
}
