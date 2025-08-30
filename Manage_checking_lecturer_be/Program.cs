using MongoElearn.Infrastructure;
using MongoElearn.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<EmployeeService>(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<EmployeeService>();
builder.Services.AddSingleton<ProjectService>();
builder.Services.AddSingleton<TimeEntryProjectService>();
builder.Services.AddSingleton<GiangVienService>();
builder.Services.AddSingleton<LopHocService>();
builder.Services.AddSingleton<GiangVienLopHocService>();
builder.Services.AddSingleton<RoleService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
