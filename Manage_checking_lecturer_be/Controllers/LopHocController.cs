using Microsoft.AspNetCore.Mvc;
using MongoElearn.Services;
using MongoElearn.Models;
using MongoElearn.DTOs.LopHoc;
using MongoElearn.Api.Common;

namespace MongoElearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LopHocsController : ControllerBase
{
    private readonly ILopHocService _svc;
    public LopHocsController(ILopHocService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<HTTPResponseClient<List<LopHoc>>>> GetAll()
        => Ok(HTTPResponseClient<List<LopHoc>>.Ok(await _svc.GetAllAsync()));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<LopHoc>>> GetById(int id)
    {
        var lop = await _svc.GetByIdAsync(id);
        return lop is null
            ? NotFound(HTTPResponseClient<LopHoc>.Fail("Class not found", 404))
            : Ok(HTTPResponseClient<LopHoc>.Ok(lop));
    }

    [HttpPost]
    public async Task<ActionResult<HTTPResponseClient<string>>> Create([FromBody] LopHocCreateDto dto)
    {
        await _svc.CreateAsync(dto);
        return Ok(HTTPResponseClient<string>.Ok("Created", "Class created"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Update(int id, [FromBody] LopHocUpdateDto dto)
    {
        var ok = await _svc.UpdateAsync(id, dto);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Updated"))
            : NotFound(HTTPResponseClient<string>.Fail("Class not found", 404));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Deleted"))
            : NotFound(HTTPResponseClient<string>.Fail("Class not found", 404));
    }
}
