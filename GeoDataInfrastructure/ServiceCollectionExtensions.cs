using GeoDataInfrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace GeoDataInfrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IPlaceRepository, PlaceRepository>();
        services.AddTransient<INameRepository, NameRepository>();
        services.AddTransient<IPlaceCreationRequestRepository, PlaceCreationRequestRepository>();
        services.AddTransient<IBoundaryRepository, BoundaryRepository>();
        services.AddTransient<ILocationRepository, LocationRepository>();
        services.AddTransient<ITagRepository, TagRepository>();
        services.AddTransient(_ => new MySqlConnection(configuration.GetConnectionString("Default")));
        services.AddTransient<IUserRepository, UserRepository>();
    }
}