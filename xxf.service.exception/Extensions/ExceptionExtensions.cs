using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xxf.Service.Exception;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Xxf.Service.Exception.Extensions
{

    /// <summary>
    /// <see cref="IApplicationBuilder"/> extension methods for the <see cref="DeveloperExceptionPageMiddleware"/>.
    /// </summary>
    ///         /// <summary>
    /// Captures synchronous and asynchronous <see cref="Exception"/> instances from the pipeline and generates HTML error responses.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <returns>A reference to the <paramref name="app"/> after the operation has completed.</returns>
    /// <remarks>
    /// This should only be enabled in the Development environment.
    /// </remarks>
    ///

    public static class ExceptionExtensions
    {
        #region "SERVICES"
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
        {
            //services.AddTransient<SqlServerExceptionHandler>();
            return services;
        }
        #endregion

        #region "MIDDLEWARE"

        public static IApplicationBuilder UseDomainDataErrorException(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ExceptionMiddleware>();
        }
        public static IApplicationBuilder UseDomainDataErrorException(
            this IApplicationBuilder app,
            Options.DomainErrorExceptionOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<ExceptionMiddleware>(Microsoft.Extensions.Options.Options.Create(options));
        }
        #endregion
    }
}
