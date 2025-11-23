
using WordCoachBot.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Временная заглушка сервиса слов
builder.Services.AddScoped<IWordService, DummyWordService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/ping", () => "WordCoachBot is up!");

app.MapControllers();

app.Run();

// Временная реализация IWordService
public class DummyWordService : IWordService
{
}