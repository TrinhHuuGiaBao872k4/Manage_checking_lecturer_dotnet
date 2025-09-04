using Microsoft.AspNetCore.Mvc;
using MongoElearn.Services;
using MongoElearn.Models;
using MongoElearn.DTOs.Roles;
using MongoElearn.Api.Common;

namespace MongoElearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class RolesController : ControllerBase
{
    private readonly IRoleService _svc;
    public RolesController(IRoleService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<HTTPResponseClient<List<Role>>>> GetAll()
        => Ok(HTTPResponseClient<List<Role>>.Ok(await _svc.GetAllAsync()));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<Role>>> GetById(int id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r is null
            ? NotFound(HTTPResponseClient<Role>.Fail("Role not found", 404))
            : Ok(HTTPResponseClient<Role>.Ok(r));
    }

    [HttpPost]
    public async Task<ActionResult<HTTPResponseClient<string>>> Create([FromBody] RoleCreateDto dto)
    {
        await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = dto.id },
            HTTPResponseClient<string>.Ok("Created", "Role created", 201));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Update(int id, [FromBody] RoleUpdateDto dto)
    {
        var ok = await _svc.UpdateAsync(id, dto);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Updated"))
            : NotFound(HTTPResponseClient<string>.Fail("Role not found", 404));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Deleted"))
            : NotFound(HTTPResponseClient<string>.Fail("Role not found", 404));
    }
}
