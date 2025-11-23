using Microsoft.Extensions.Options;
using WordCoachBot.Application;
using WordCoachBot.Application.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<TelegramBotOptions>(
    builder.Configuration.GetSection(TelegramBotOptions.SectionName)
);

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