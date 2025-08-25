using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;
using Serilog;
using NZWalks.API.CustomActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 1024 * 1024, // 1 MB
        rollOnFileSizeLimit: true,
        retainedFileCountLimit: 10,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(1))
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogResultActionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<IDifficultyRepository, SQLDifficultyRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
// Filters
builder.Services.AddScoped<LogResultActionFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting NZWalks.API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
