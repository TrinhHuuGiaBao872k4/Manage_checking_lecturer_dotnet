using Microsoft.AspNetCore.Mvc;
using MongoElearn.Services;
using MongoElearn.Models;
using MongoElearn.DTOs.TimeEntries;
using MongoElearn.Api.Common;

namespace MongoElearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TimeEntryProjectsController : ControllerBase
{
    private readonly ITimeEntryProjectService _svc;
    public TimeEntryProjectsController(ITimeEntryProjectService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<HTTPResponseClient<List<TimeEntryProject>>>> GetAll()
        => Ok(HTTPResponseClient<List<TimeEntryProject>>.Ok(await _svc.GetAllAsync()));

    [HttpGet("{id}")]
    public async Task<ActionResult<HTTPResponseClient<TimeEntryProject>>> GetById(string id)
    {
        var t = await _svc.GetByIdAsync(id);
        return t is null
            ? NotFound(HTTPResponseClient<TimeEntryProject>.Fail("Time entry not found", 404))
            : Ok(HTTPResponseClient<TimeEntryProject>.Ok(t));
    }

    [HttpGet("by-employee/{employeeId}")]
    public async Task<ActionResult<HTTPResponseClient<List<TimeEntryProject>>>> GetByEmployee(string employeeId)
        => Ok(HTTPResponseClient<List<TimeEntryProject>>.Ok(await _svc.GetByEmployeeAsync(employeeId)));

    [HttpGet("by-project/{projectId}")]
    public async Task<ActionResult<HTTPResponseClient<List<TimeEntryProject>>>> GetByProject(string projectId)
        => Ok(HTTPResponseClient<List<TimeEntryProject>>.Ok(await _svc.GetByProjectAsync(projectId)));

    [HttpPost]
    public async Task<ActionResult<HTTPResponseClient<string>>> Create([FromBody] TimeEntryCreateDto dto)
    {
        await _svc.CreateAsync(dto);
        return Ok(HTTPResponseClient<string>.Ok("Created"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Update(string id, [FromBody] TimeEntryUpdateDto dto)
    {
        var ok = await _svc.UpdateAsync(id, dto);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Updated"))
            : NotFound(HTTPResponseClient<string>.Fail("Time entry not found", 404));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Delete(string id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Deleted"))
            : NotFound(HTTPResponseClient<string>.Fail("Time entry not found", 404));
    }
}
