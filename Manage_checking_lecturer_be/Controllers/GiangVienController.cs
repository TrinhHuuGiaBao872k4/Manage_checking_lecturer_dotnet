using Microsoft.AspNetCore.Mvc;
using MongoElearn.Services;
using MongoElearn.Models;
using MongoElearn.DTOs.GiangVien;
using MongoElearn.Api.Common;

namespace MongoElearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GiangViensController : ControllerBase
{
    private readonly IGiangVienService _svc;
    public GiangViensController(IGiangVienService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<HTTPResponseClient<List<GiangVien>>>> GetAll()
        => Ok(HTTPResponseClient<List<GiangVien>>.Ok(await _svc.GetAllAsync()));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<GiangVien>>> GetById(int id)
    {
        var gv = await _svc.GetByIdAsync(id);
        return gv is null
            ? NotFound(HTTPResponseClient<GiangVien>.Fail("Lecturer not found", 404))
            : Ok(HTTPResponseClient<GiangVien>.Ok(gv));
    }

    [HttpPost]
    public async Task<ActionResult<HTTPResponseClient<string>>> Create([FromBody] GiangVienCreateDto dto)
    {
        await _svc.CreateAsync(dto);
        return Ok(HTTPResponseClient<string>.Ok("Created", "Lecturer created"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Update(int id, [FromBody] GiangVienUpdateDto dto)
    {
        var ok = await _svc.UpdateAsync(id, dto);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Updated"))
            : NotFound(HTTPResponseClient<string>.Fail("Lecturer not found", 404));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Deleted"))
            : NotFound(HTTPResponseClient<string>.Fail("Lecturer not found", 404));
    }
}
