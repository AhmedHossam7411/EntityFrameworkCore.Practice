using EFcore.data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FootballLeagueDbContext>();

//var connectionString = builder.Configuration.GetConnectionString("SqlDatabaseConnectionString");
var sqliteDatabaseName = builder.Configuration.GetConnectionString("SqliteDatabaseConnectionString");
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = Path.Combine(path, sqliteDatabaseName);
var connectionString = $"Data Source={dbPath}";

builder.Services.AddDbContext<FootballLeagueDbContext>(options => {
    options.UseSqlite(connectionString);
    //.UseLazyLoadingProxies()
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.LogTo(Console.WriteLine, LogLevel.Information);

    if (!builder.Environment.IsProduction())
    {
        options.EnableSensitiveDataLogging();//OK in testing a project, but be careful about enabling this in production.
        options.EnableDetailedErrors();
    }
});

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

app.Run();
