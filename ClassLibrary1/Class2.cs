using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public static class Class2
    {
        public static IServiceCollection AddClass(this IServiceCollection services)
        {
            services
                .AddScoped<Class1>();

            return services;
        }
    }
}
