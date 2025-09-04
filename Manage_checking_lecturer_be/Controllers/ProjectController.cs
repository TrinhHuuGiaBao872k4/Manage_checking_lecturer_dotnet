using Microsoft.AspNetCore.Mvc;
using MongoElearn.Services;
using MongoElearn.Models;
using MongoElearn.DTOs.Projects;
using MongoElearn.Api.Common;

namespace MongoElearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProjectsController : ControllerBase
{
    private readonly IProjectService _svc;
    public ProjectsController(IProjectService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<HTTPResponseClient<List<Project>>>> GetAll()
        => Ok(HTTPResponseClient<List<Project>>.Ok(await _svc.GetAllAsync()));

    [HttpGet("{id}")]
    public async Task<ActionResult<HTTPResponseClient<Project>>> GetById(string id)
    {
        var p = await _svc.GetByIdAsync(id);
        return p is null
            ? NotFound(HTTPResponseClient<Project>.Fail("Project not found", 404))
            : Ok(HTTPResponseClient<Project>.Ok(p));
    }

    [HttpGet("by-code")]
    public async Task<ActionResult<HTTPResponseClient<Project>>> GetByCode([FromQuery] string code)
    {
        var p = await _svc.GetByCodeAsync(code);
        return p is null
            ? NotFound(HTTPResponseClient<Project>.Fail("Project not found", 404))
            : Ok(HTTPResponseClient<Project>.Ok(p));
    }

    [HttpPost]
    public async Task<ActionResult<HTTPResponseClient<Project>>> Create([FromBody] ProjectCreateDto dto)
    {
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.id },
            HTTPResponseClient<Project>.Ok(created, "Created", 201));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Update(string id, [FromBody] ProjectUpdateDto dto)
    {
        var ok = await _svc.UpdateAsync(id, dto);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Updated"))
            : NotFound(HTTPResponseClient<string>.Fail("Project not found", 404));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Delete(string id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Deleted"))
            : NotFound(HTTPResponseClient<string>.Fail("Project not found", 404));
    }
}
