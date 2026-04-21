using EncheresPredict.Api.Middleware;
using EncheresPredict.Application;
using EncheresPredict.Infrastructure;
using EncheresPredict.Infrastructure.Persistence;
using EncheresPredict.Infrastructure.Persistence.Seed;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Couches DDD ──────────────────────────────────────────────
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// ── API ──────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Enchères Predict API",
        Version = "v1",
        Description = "Backend DDD + CQRS pour la plateforme d'analyse d'enchères immobilières judiciaires"
    });
});

// ── CORS pour Angular (localhost:4200) ───────────────────────
builder.Services.AddCors(opt =>
    opt.AddPolicy("Angular", p => p
        .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? ["http://localhost:4200"])
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

// ── Seed DB au démarrage ──────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await SeedData.InitializeAsync(db);
}

// ── Middleware ────────────────────────────────────────────────
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enchères Predict API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("Angular");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
