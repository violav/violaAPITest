using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Xxf.Service.Exception.Options;

namespace Xxf.Service.Exception
{
    /// <summary>
    /// Captures synchronous and asynchronous exceptions from the pipeline and generates error responses.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DomainErrorExceptionOptions _options;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly DiagnosticSource _diagnosticSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="diagnosticSource"></param>
        /// <param name="filters"></param>
        public ExceptionMiddleware(
            RequestDelegate next,
            IOptions<DomainErrorExceptionOptions> options,
            ILogger<ExceptionMiddleware> logger,
            DiagnosticSource diagnosticSource
            )
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            _next = next;
            _options = options.Value;
            _logger = logger;
            _diagnosticSource = diagnosticSource;
        }

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogCritical(ex, $"Response has been already started ({ex.Message})");
                    throw;
                }
                try
                {
                    var errorContext = new ErrorContext(context, ex);
                    var status = (int)HttpStatusCode.BadRequest;
                 
                    context.Response.Clear();
                    switch (errorContext.Exception.Source)
                    {
                        case "Microsoft.EntityFrameworkCore.SqlServer":
                            _logger.LogCritical(ex, $"DATABASE : {errorContext.Exception.Message}");
                            break;
                        default:
                            _logger.LogCritical(ex, $"GENERIC : {errorContext.Exception.Message}");
                            break;
                    }

                    var problem = new
                    {
                        title = ReasonPhrases.GetReasonPhrase(status),
                        instance = errorContext.HttpContext.Request.Path,
                        type = string.Concat("https://httpstatuses.io/", status),
                        errors = new Dictionary<string, IEnumerable<string>>()
                        {
                            { "error type", new string[] { ex.GetType().ToString() } },
                            { "error message", new string[]{ex.Message } }
                        },
                        status = status,
                        //detail = "",
                        traceId = Activity.Current?.Id ?? errorContext.HttpContext.TraceIdentifier
                    };                 

                    var jsonResponse = JsonConvert.SerializeObject(problem);
                    errorContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; 
                    errorContext.HttpContext.Response.ContentType = "application/json";
                    await errorContext.HttpContext.Response.WriteAsync(jsonResponse);
                }
                catch (System.Exception innerException)
                {
                    _logger.LogCritical(ex, $"Unhandled exception in {(nameof(ExceptionMiddleware))} ({innerException.Message})");
                }
            }
        }
    }
}
