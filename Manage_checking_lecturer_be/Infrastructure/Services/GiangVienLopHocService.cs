using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;
using MongoElearn.DTOs.Links;
using MongoElearn.Infrastructure.Repositories;
namespace MongoElearn.Services;
public interface IGiangVienLopHocService
{
    Task<List<GiangVien_LopHoc>> GetAllAsync();
    Task<GiangVien_LopHoc?> GetByIdAsync(int id);
    Task CreateAsync(GiangVienLopHocCreateDto dto);
    Task<bool> UpdateAsync(int id, GiangVienLopHocUpdateDto dto);
    Task<bool> DeleteAsync(int id);

    Task<List<GiangVien_LopHoc>> GetByGiangVienAsync(int gvId);
    Task<List<GiangVien_LopHoc>> GetByLopHocAsync(int lopId);
}

public class GiangVienLopHocService : IGiangVienLopHocService
{
    private readonly IGiangVienLopHocRepository _repo;
     private readonly ISequenceService _seq;     
    public GiangVienLopHocService(IGiangVienLopHocRepository repo, ISequenceService seq) 
    {
        _repo = repo;
        _seq  = seq;
    }

    public Task<List<GiangVien_LopHoc>> GetAllAsync() => _repo.GetAllAsync();
    public Task<GiangVien_LopHoc?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(GiangVienLopHocCreateDto dto)
    {
        var doc = new GiangVien_LopHoc
        {
            GiangVien_Id = dto.GiangVien_Id,
            LopHoc_Id = dto.LopHoc_Id,
            isActive = dto.isActive ?? true
        };
        var next = await _seq.GetNextAsync("GiangVien_LopHoc"); // << tự tăng
        doc.GiangVien_LopHoc_Id = (int)next;

        await _repo.CreateAsync(doc);
    }

    public async Task<bool> UpdateAsync(int id, GiangVienLopHocUpdateDto dto)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e is null) return false;

        if (dto.isActive.HasValue) e.isActive = dto.isActive.Value;

        return await _repo.ReplaceAsync(
            Builders<GiangVien_LopHoc>.Filter.Eq(x => x.GiangVien_LopHoc_Id, id), e);
    }

    public Task<bool> DeleteAsync(int id)
        => _repo.DeleteAsync(Builders<GiangVien_LopHoc>.Filter.Eq(x => x.GiangVien_LopHoc_Id, id));

    public Task<List<GiangVien_LopHoc>> GetByGiangVienAsync(int gvId) => _repo.GetByGiangVienAsync(gvId);
    public Task<List<GiangVien_LopHoc>> GetByLopHocAsync(int lopId) => _repo.GetByLopHocAsync(lopId);
}
