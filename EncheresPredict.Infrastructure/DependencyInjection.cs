using EncheresPredict.Application.Common.Configuration;
using EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Domain.Repositories;
using EncheresPredict.Infrastructure.Identity;
using EncheresPredict.Infrastructure.Persistence;
using EncheresPredict.Infrastructure.Persistence.Readers;
using EncheresPredict.Infrastructure.Persistence.Repositories;
using EncheresPredict.Infrastructure.Services;
using EncheresPredict.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EncheresPredict.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<ICreditAccountRepository, CreditAccountRepository>();
        services.AddScoped<IAlertRepository, AlertRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IAuctionSummaryReader, AuctionSummaryReader>();
        services.AddScoped<IFileStorage, LocalFileStorage>();
        services.Configure<N8nOptions>(
    config.GetSection(N8nOptions.SectionName));
        services.AddHttpClient<IN8nClient, N8nClient>();
        services.Configure<AppOptions>(
    config.GetSection(AppOptions.SectionName));

        return services;
    }
}
