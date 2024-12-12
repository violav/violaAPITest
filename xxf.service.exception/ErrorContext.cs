using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xxf.Service.Exception
{
    public class ErrorContext
    {
        /// <summary>
        /// Initializes the ErrorContext with the specified <see cref="HttpContext"/> and <see cref="Exception"/>.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        public ErrorContext(HttpContext httpContext, System.Exception exception)
        {
            HttpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        /// <summary>
        /// The <see cref="HttpContext"/>.
        /// </summary>
        public HttpContext HttpContext { get; }

        /// <summary>
        /// The <see cref="Exception"/> thrown during request processing.
        /// </summary>
        public System.Exception Exception { get; }
    }
}
