using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;
using MongoElearn.DTOs.LopHoc;
using MongoElearn.Infrastructure.Repositories;

namespace MongoElearn.Services;
public interface ILopHocService
{
    Task<List<LopHoc>> GetAllAsync();
    Task<LopHoc?> GetByIdAsync(int id);
    Task CreateAsync(LopHocCreateDto dto);
    Task<bool> UpdateAsync(int id, LopHocUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

public class LopHocService : ILopHocService
{
    private readonly ILopHocRepository _repo;
      private readonly ISequenceService _seq;
   public LopHocService(ILopHocRepository repo, ISequenceService seq)    
    {
        _repo = repo;
        _seq  = seq;
    }

    public Task<List<LopHoc>> GetAllAsync() => _repo.GetAllAsync();
    public Task<LopHoc?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(LopHocCreateDto dto)
    {
        // (tuỳ chọn) validate NgayKetThuc >= NgayBatDau
        var doc = new LopHoc
        {
            TenLopHoc = dto.TenLopHoc,
            KhoaHoc = dto.KhoaHoc,
            ThoiKhoaBieu = dto.ThoiKhoaBieu,
            ChiNhanhLop = dto.ChiNhanhLop,
            NgayBatDau = dto.NgayBatDau,
            NgayKetThuc = dto.NgayKetThuc,
            khungGio = dto.khungGio,
            isActive = dto.isActive ?? true
        };
        var next = await _seq.GetNextAsync("LopHoc");      
        doc.LopHoc_Id = (int)next;
        await _repo.CreateAsync(doc);
    }

    public async Task<bool> UpdateAsync(int id, LopHocUpdateDto dto)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e is null) return false;

        e.TenLopHoc = dto.TenLopHoc ?? e.TenLopHoc;
        e.KhoaHoc = dto.KhoaHoc ?? e.KhoaHoc;
        e.ThoiKhoaBieu = dto.ThoiKhoaBieu ?? e.ThoiKhoaBieu;
        e.ChiNhanhLop = dto.ChiNhanhLop ?? e.ChiNhanhLop;
        e.NgayBatDau = dto.NgayBatDau ?? e.NgayBatDau;
        e.NgayKetThuc = dto.NgayKetThuc ?? e.NgayKetThuc;
        e.khungGio = dto.khungGio ?? e.khungGio;
        if (dto.isActive.HasValue) e.isActive = dto.isActive.Value;

        return await _repo.ReplaceAsync(Builders<LopHoc>.Filter.Eq(x => x.LopHoc_Id, id), e);
    }

    public Task<bool> DeleteAsync(int id)
        => _repo.DeleteAsync(Builders<LopHoc>.Filter.Eq(x => x.LopHoc_Id, id));
}