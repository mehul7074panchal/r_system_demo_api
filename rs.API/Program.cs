using Microsoft.Extensions.Caching.Memory;
using rs.API;
using rs.Contract;
using rs.Helper;
using rs.Repository;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day,restrictedToMinimumLevel: LogEventLevel.Error)
    .CreateLogger();
var services = builder.Services;

// Add services to the container.
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<HttpHelerClient>();

services.AddSingleton<IHttpHelerClient, HttpHelerClient>();
services.AddScoped<IStory, StoryRepository>();


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

try
{
    using var scope = app.Services.CreateScope();
    var cache = app.Services.GetRequiredService<IMemoryCache>();
    var client = scope.ServiceProvider.GetRequiredService<HttpHelerClient>();
    var storyIds = await client.HttpGetRequest<List<int>>("newstories");
    cache.Set(HttpHelerClient.case_key_stories, storyIds, TimeSpan.FromHours(5));
}
catch (Exception ex)
{
    Log.Error(ex, "Program.cs");
    throw;
}


//app.UseHttpsRedirection();


app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});


app.UseAuthorization();


app.MapControllers();

app.Run();

public partial class Program { }
