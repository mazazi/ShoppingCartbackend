using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tatweer.Core.Repositories;
using Tatweer.Insrastructure.Data;
using Tatweer.Insrastructure.Repositories;

namespace Tatweer.Insrastructure.Extensions;

public static class InfraServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<TatweerContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("TatweerConnectionString"))); 
        serviceCollection.AddScoped(typeof(TatweerContext), typeof(TatweerContext));
        serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        return serviceCollection;
    }
}