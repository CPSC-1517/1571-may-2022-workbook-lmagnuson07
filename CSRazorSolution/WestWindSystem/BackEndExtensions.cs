using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WestWindSystem.BLL;
using WestWindSystem.DAL;
#endregion

namespace WestWindSystem
{
    // must be a static class
    public static class BackEndExtensions 
    {
        public static void WWBackendDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            // we will register all the services that will be used by the system (context steup) and will be provided by the system (BLL services) 

            // Register the context service 
            // options contains the connection string information
            services.AddDbContext<WestWindContext>(options);

            // Register EACH service class (BLL classes)
            // each service class will have an AddTransient<T> 

            // use the AddTransient<T>() method where T is your BLL class name
            // AddTransient is called a factory 
            // Read the lamba symbol => as "do the following..."
            services.AddTransient<BuildVersionServices>((serviceProvider) =>
            {
                // get the connection class that was registered above and AddDbContext
                var context = serviceProvider.GetService<WestWindContext>();

                // create an instance of the service class (BuildVersionServices) supplying the context reference to the service class 
                return new BuildVersionServices(context);
            });
            services.AddTransient<RegionServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new RegionServices(context);
            });
            services.AddTransient<TerritoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new TerritoryServices(context);
            });
        }
    }
}
