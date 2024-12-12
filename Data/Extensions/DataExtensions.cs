using Data.Data.EF.Context;
using Data.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Data.Extensions
{
    public static class DataExtensions
    {
        //public static IServiceCollection AddDataContext(this IServiceCollection services, Action<DataOptions> options)
        //{
        //    var securityOptions = new DataOptions();
        //    options.Invoke(securityOptions);
        //    services.AddSingleton(securityOptions);
        //    services
        //        .AddDbContext<NorthwindContext>((serviceProvider, opt) =>
        //        {
        //            opt.UseSqlServer(securityOptions.Connectionstring);
        //        });

        //    return services;
        //}


        //public static IServiceCollection AddDataContext2(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services
        //        .AddOptions()
        //        .Configure<DataOptions>(configuration.GetSection("DataOptions"));

        //    //services.AddSingleton(s => s.GetService<IOptions<DataOptions>>()?.Value);

        //    services
        //        .AddDbContext<NorthwindContext>((serviceProvider, opt) =>
        //        {
        //            var securityOptions = serviceProvider.GetService<IOptions<DataOptions>>()?.Value;
        //            //var securityOptions = serviceProvider.GetService<DataOptions>();
        //            opt.UseSqlServer(securityOptions.Connectionstring);
        //        });

        //    return services;
        //}
        public static IServiceCollection AddDataContext2(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOptions()
                .Configure<DataOptions>(configuration.GetSection("DataOptions"));

            services
                .AddDbContext<NorthwindContext>((serviceProvider, opt) =>
                {
                    var securityOptions = serviceProvider.GetService<IOptions<DataOptions>>()?.Value;
                    opt.UseSqlServer(securityOptions.Connectionstring);
                });

            return services;
        }
    }
}
