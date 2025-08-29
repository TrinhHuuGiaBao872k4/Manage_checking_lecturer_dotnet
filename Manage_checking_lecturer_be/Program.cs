using MongoElearn.Infrastructure;
using MongoElearn.Infrastructure.Repositories;  // << thêm
using MongoElearn.Services;

var builder = WebApplication.CreateBuilder(args);

// Mongo settings + context
// Lưu ý: section name phải khớp appsettings (MongoSettings hay MongoDb)
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<MongoDbContext>();

// Repositories (lấy collection từ MongoDbContext)
builder.Services.AddScoped<IEmployeeRepository>(sp =>
    new EmployeeRepository(sp.GetRequiredService<MongoDbContext>().Employee));
builder.Services.AddScoped<IProjectRepository>(sp =>
    new ProjectRepository(sp.GetRequiredService<MongoDbContext>().Project));
builder.Services.AddScoped<ITimeEntryProjectRepository>(sp =>
    new TimeEntryProjectRepository(sp.GetRequiredService<MongoDbContext>().TimeEntryProject));
builder.Services.AddScoped<IGiangVienRepository>(sp =>
    new GiangVienRepository(sp.GetRequiredService<MongoDbContext>().GiangVien));
builder.Services.AddScoped<ILopHocRepository>(sp =>
    new LopHocRepository(sp.GetRequiredService<MongoDbContext>().LopHoc));
builder.Services.AddScoped<IGiangVienLopHocRepository>(sp =>
    new GiangVienLopHocRepository(sp.GetRequiredService<MongoDbContext>().GiangVien_LopHoc));
builder.Services.AddScoped<IRoleRepository>(sp =>
    new RoleRepository(sp.GetRequiredService<MongoDbContext>().role));

// Services (đăng ký qua interface, dùng Scoped)
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
// builder.Services.AddScoped<IProjectService, ProjectService>();
// builder.Services.AddScoped<ITimeEntryProjectService, TimeEntryProjectService>();
// builder.Services.AddScoped<IGiangVienService, GiangVienService>();
// builder.Services.AddScoped<ILopHocService, LopHocService>();
// builder.Services.AddScoped<IGiangVienLopHocService, GiangVienLopHocService>();
// builder.Services.AddScoped<IRoleService, RoleService>();

// MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// (khuyến nghị) tạo index khi app start
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await ctx.EnsureIndexesAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
