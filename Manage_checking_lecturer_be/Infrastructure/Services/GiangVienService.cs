using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;
using MongoElearn.DTOs.GiangVien;
using MongoElearn.Infrastructure.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;
namespace MongoElearn.Services;


public interface IGiangVienService
{
    Task<List<GiangVien>> GetAllAsync();
    Task<GiangVien?> GetByIdAsync(int id);
    Task CreateAsync(GiangVienCreateDto dto);
    Task<bool> UpdateAsync(int id, GiangVienUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
public class GiangVienService : IGiangVienService
{
    private readonly IGiangVienRepository _repo;
    private readonly ISequenceService _seq;
    public GiangVienService(IGiangVienRepository repo,ISequenceService seq) => _repo = repo;

    public Task<List<GiangVien>> GetAllAsync() => _repo.GetAllAsync();
    public Task<GiangVien?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(GiangVienCreateDto dto)
    {
        var doc = new GiangVien
        {
            Ten = dto.Ten,
            KiNang = dto.KiNang,
            ChiNhanh = dto.ChiNhanh,
            KhungGioDay = dto.KhungGioDay,
            DayOff = dto.DayOff,
            Mau = dto.Mau,
            GhiChu = dto.GhiChu,
            email = dto.email,
            account = dto.account,
            password = string.IsNullOrWhiteSpace(dto.password) ? null : BCryptNet.HashPassword(dto.password),
            role = dto.role,
            isActive = dto.isActive ?? true
        };
         var next = await _seq.GetNextAsync("GiangVien");   // << tự tăng
        doc.id = (int)next;
        await _repo.CreateAsync(doc);
    }

    public async Task<bool> UpdateAsync(int id, GiangVienUpdateDto dto)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e is null) return false;

        e.Ten = dto.Ten ?? e.Ten;
        e.KiNang = dto.KiNang ?? e.KiNang;
        e.ChiNhanh = dto.ChiNhanh ?? e.ChiNhanh;
        e.KhungGioDay = dto.KhungGioDay ?? e.KhungGioDay;
        e.DayOff = dto.DayOff ?? e.DayOff;
        e.Mau = dto.Mau ?? e.Mau;
        e.GhiChu = dto.GhiChu ?? e.GhiChu;
        e.email = dto.email ?? e.email;
        e.account = dto.account ?? e.account;
        if (!string.IsNullOrWhiteSpace(dto.password))
            e.password = BCryptNet.HashPassword(dto.password);
        e.role = dto.role ?? e.role;
        if (dto.isActive.HasValue) e.isActive = dto.isActive.Value;

        return await _repo.ReplaceAsync(Builders<GiangVien>.Filter.Eq(x => x.id, id), e);
    }

    public Task<bool> DeleteAsync(int id)
        => _repo.DeleteAsync(Builders<GiangVien>.Filter.Eq(x => x.id, id));
}
