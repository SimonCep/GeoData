using GeoDataBusiness.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GeoDataBusiness;

public static class ServiceCollectionExtensions
{
    public static void AddBusinessLayer(this IServiceCollection services)
    {
        services.AddTransient<IPlaceService, PlaceService>();
        services.AddTransient<INameService, NameService>();
        services.AddTransient<IPlaceCreationRequestService, PlaceCreationRequestService>();
        services.AddTransient<IBoundaryService, BoundaryService>();
        services.AddTransient<ITagService, TagService>();
        services.AddTransient<ILocationService, LocationService>();
        services.AddTransient<IUserService, UserService>();
    }
}