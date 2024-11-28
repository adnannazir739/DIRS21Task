using Microsoft.Extensions.DependencyInjection;
using System;

namespace DynamicMappingSystem
{
    public class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IMapHandler, MapHandler>();

            return services.BuildServiceProvider();
        }
    }
}
