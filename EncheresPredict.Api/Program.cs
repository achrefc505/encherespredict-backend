using EncheresPredict.Api.Filters;
using EncheresPredict.Api.Middleware;
using EncheresPredict.Application;
using EncheresPredict.Application.Common.Configuration;
using EncheresPredict.Infrastructure;
using EncheresPredict.Infrastructure.Identity;
using EncheresPredict.Infrastructure.Persistence;
using EncheresPredict.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using EncheresPredict.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// ── Couches DDD ──────────────────────────────────────────────
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<InternalApiOptions>(
    builder.Configuration.GetSection(InternalApiOptions.SectionName));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// ── API ──────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddScoped<ApiKeyFilter>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Enchères Predict API",
        Version = "v1",
        Description = "Backend DDD + CQRS pour la plateforme d'analyse d'enchères immobilières judiciaires"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Bearer token. Format: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// ── CORS pour Angular (localhost:4200) ───────────────────────
builder.Services.AddCors(opt =>
    opt.AddPolicy("Angular", p => p
        .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? ["http://localhost:4200"])
        .AllowAnyHeader()
        .AllowAnyMethod()));

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key is missing.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// ── Seed DB au démarrage ──────────────────────────────────────
// Skip si EP_SKIP_SEED=true (utile quand les vraies données arrivent via ETL ep-licitor-scraper)
var skipSeed = Environment.GetEnvironmentVariable("EP_SKIP_SEED")?.Equals("true", StringComparison.OrdinalIgnoreCase) == true;
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!app.Environment.IsEnvironment("Testing"))
    {
        await db.Database.MigrateAsync();
        await IdentitySeedData.InitializeAsync(scope.ServiceProvider);

        if (!skipSeed)
        {
            await SeedData.InitializeAsync(db);
        }
    }
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
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "Storage")),
    RequestPath = "/storage"
});
app.MapControllers().RequireAuthorization();
app.Run();
public partial class Program
{
}
