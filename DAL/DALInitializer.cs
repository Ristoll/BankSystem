using Core.Entities;
using Core;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class DALInitializer
{
    public static void AddDataAccessServices(IServiceCollection services)
    {
        services.AddDbContext<BankDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
