// Infrastructure/MongoSettings.cs
namespace MongoElearn.Infrastructure;

public class MongoSettings
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public CollectionNames Collections { get; set; } = new();
}

public class CollectionNames
{
    public string Employee { get; set; } = "Employee";
    public string GiangVien { get; set; } = "GiangVien";
    public string GiangVien_LopHoc { get; set; } = "GiangVien_LopHoc";
    public string LopHoc { get; set; } = "LopHoc";
    public string Project { get; set; } = "Project";
    public string role { get; set; } = "role";
    public string TimeEntryProject { get; set; } = "TimeEntryProject";
}
