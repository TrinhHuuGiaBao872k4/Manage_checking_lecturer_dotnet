using Microsoft.AspNetCore.Mvc;
using MongoElearn.Services;
using MongoElearn.Models;
using MongoElearn.DTOs.Links;
using MongoElearn.Api.Common;

namespace MongoElearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GiangVienLopHocController : ControllerBase
{
    private readonly IGiangVienLopHocService _svc;
    public GiangVienLopHocController(IGiangVienLopHocService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<HTTPResponseClient<List<GiangVien_LopHoc>>>> GetAll()
        => Ok(HTTPResponseClient<List<GiangVien_LopHoc>>.Ok(await _svc.GetAllAsync()));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<GiangVien_LopHoc>>> GetById(int id)
    {
        var doc = await _svc.GetByIdAsync(id);
        return doc is null
            ? NotFound(HTTPResponseClient<GiangVien_LopHoc>.Fail("Not found", 404))
            : Ok(HTTPResponseClient<GiangVien_LopHoc>.Ok(doc));
    }

    [HttpGet("by-giangvien/{gvId:int}")]
    public async Task<ActionResult<HTTPResponseClient<List<GiangVien_LopHoc>>>> GetByGiangVien(int gvId)
        => Ok(HTTPResponseClient<List<GiangVien_LopHoc>>.Ok(await _svc.GetByGiangVienAsync(gvId)));

    [HttpGet("by-lophoc/{lopId:int}")]
    public async Task<ActionResult<HTTPResponseClient<List<GiangVien_LopHoc>>>> GetByLopHoc(int lopId)
        => Ok(HTTPResponseClient<List<GiangVien_LopHoc>>.Ok(await _svc.GetByLopHocAsync(lopId)));

    [HttpPost]
    public async Task<ActionResult<HTTPResponseClient<string>>> Create([FromBody] GiangVienLopHocCreateDto dto)
    {
        await _svc.CreateAsync(dto);
        return Ok(HTTPResponseClient<string>.Ok("Created"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Update(int id, [FromBody] GiangVienLopHocUpdateDto dto)
    {
        var ok = await _svc.UpdateAsync(id, dto);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Updated"))
            : NotFound(HTTPResponseClient<string>.Fail("Not found", 404));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<HTTPResponseClient<string>>> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(HTTPResponseClient<string>.Ok("Deleted"))
            : NotFound(HTTPResponseClient<string>.Fail("Not found", 404));
    }
}
