using BusinessLogic.Controller;
using BusinessLogic.Options;
using BusinessLogic.Services;
using Data.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Extensions
{
    public static class BusinessExtensions
    {

        //nelllo startup devo fare:
        //var businessOptions = _configuration.GetSection(nameof(BusinessOptions));
        //services.Configure<BusinessOptions>(businessOptions);

        //public static IServiceCollection AddBusiness(this IServiceCollection services, Action<BusinessOptions> configuration)
        //{
        //    var businessOption = new BusinessOptions();
        //    configuration.Invoke(businessOption);
        //    services.AddSingleton(businessOption);

        //    services
        //        .AddScoped<BusinessController>()
        //        .AddScoped<BusinessServices>();

        //    return services;
        //}

        public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOptions()
                .Configure<BusinessOptions>(configuration.GetSection("BusinessOptions"));

            services
                .AddScoped<BusinessController>()
                .AddScoped<BusinessServices>();

            return services;
        }
    }
}
