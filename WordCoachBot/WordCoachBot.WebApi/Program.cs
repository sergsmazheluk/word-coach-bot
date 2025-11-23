using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WordCoachBot.Application;
using WordCoachBot.Application.Options;
using WordCoachBot.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// DbContext + PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// Configure TelegramBot options
builder.Services.Configure<TelegramBotOptions>(
    builder.Configuration.GetSection(TelegramBotOptions.SectionName));

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

app.MapGet("/api/config-test", (IOptions<TelegramBotOptions> options) =>
{
    var token = options.Value.Token;
    var hasToken = !string.IsNullOrWhiteSpace(token);

    return Results.Ok(new
    {
        HasToken = hasToken,
        TokenLength = token?.Length ?? 0
    });
});

app.MapControllers();

app.Run();

// Временная реализация IWordService
public class DummyWordService : IWordService
{
}