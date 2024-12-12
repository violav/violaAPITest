using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Xxf.Common.Exceptions
{   
    public class DomainDataErrorException : Exception
    {
        public DomainDataError Error { get; protected set; }

        public DomainDataErrorException(DomainDataError error) : base(error.Title + " " + string.Join(":", error.Errors))
        {
            Error = error;
        }
        public DomainDataErrorException(string? title = null, IDictionary<string, object?> errors = null, string instance = "", string? type = default, int status = 0, string? detail = null, Exception? exception = null) : base(title)
        {
            var errTitle = string.IsNullOrEmpty(title) ? exception?.GetType().ToString() : title;
            var errInstance = string.IsNullOrEmpty(title) ? exception?.Source : instance;
            var errType = string.IsNullOrEmpty(type) ? exception?.GetType().ToString() : type;
            var errStatus = status;
            var errDetail = string.IsNullOrEmpty(detail) ? exception?.Message : detail;

            if (errors == null)
                errors = new Dictionary<string, object?>();
            if (exception != null)
            {
                errors.Add("InnerException", new string[] { exception.InnerException?.Message });
                errors.Add("ExceptionSource", new string[] { exception.Source });
                errors.Add("Request", new string[] { instance });
            }
            Error = new DomainDataError(
                errTitle,
                errors,
                errInstance,
                errType,
                errStatus,
                errDetail
                );
        }

        public DomainDataErrorException(DomainDataError error, IDictionary<string, object?> errors = null, string detail = "") : base(error.Title + " " + string.Join(":", error.Errors))
        {
            var errTitle = error.Title;
            var errErrors = errors ?? error.Errors;
            var errInstance = error.Instance;
            var errType = error.Type;
            var errStatus = error.Status ?? 0;
            var traceId = error.TraceId;

            Error = new DomainDataError(
                errTitle,
                errErrors,
                errInstance,
                errType,
                errStatus,
                "",
                traceId
            );
        }
    }
}
