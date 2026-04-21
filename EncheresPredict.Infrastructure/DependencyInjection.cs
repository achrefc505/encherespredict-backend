using EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Domain.Repositories;
using EncheresPredict.Infrastructure.Persistence;
using EncheresPredict.Infrastructure.Persistence.Repositories;
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

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<IAlertRepository, AlertRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();

        return services;
    }
}
