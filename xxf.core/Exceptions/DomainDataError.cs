using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xxf.Common.Exceptions
{

    public class DomainDataError : IEquatable<DomainDataError>
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type")]
        public string Type { get; set; }

        //
        // Riepilogo:
        //     A short, human-readable summary of the problem type.It SHOULD NOT change from
        //     occurrence to occurrence of the problem, except for purposes of localization(e.g.,
        //     using proactive content negotiation; see[RFC7231], Section 3.4).
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title")]
        public string Title { get; set; }

        //
        // Riepilogo:
        //     The HTTP status code([RFC7231], Section 6) generated by the origin server for
        //     this occurrence of the problem.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "status")]
        public int? Status { get; set; }

        //
        //// Riepilogo:
        ////     A human-readable explanation specific to this occurrence of the problem.
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "detail")]
        //public string Detail { get; set; }

        //
        // Riepilogo:
        //     A URI reference that identifies the specific occurrence of the problem.It may
        //     or may not yield further information if dereferenced.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "instance")]
        public string Instance { get; set; }

        //
        // Riepilogo:
        //     Gets the System.Collections.Generic.IDictionary`2 for extension members.
        //     Problem type definitions MAY extend the problem details object with additional
        //     members. Extension members appear in the same namespace as other members of a
        //     problem type.
        //
        // Commenti:
        //     The round-tripping behavior for Microsoft.AspNetCore.Mvc.ProblemDetails.Extensions
        //     is determined by the implementation of the Input \ Output formatters. In particular,
        //     complex types or collection types may not round-trip to the original type when
        //     using the built-in JSON or XML formatters.
        //[JsonExtensionData]
        public IDictionary<string, object?> Errors { get; } = new Dictionary<string, object?>(StringComparer.Ordinal);
        //public IEnumerable<ValidationError> Extensions { get; } = new List<ValidationError>();
        //
        // Riepilogo:
        //     A URI reference that identifies the specific occurrence of the problem.It may
        //     or may not yield further information if dereferenced.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "traceId")]
        public string TraceId { get; set; }

        public DomainDataError() { }
        public DomainDataError(string title)
        {
            Title = title;
        }

        public DomainDataError(string title, IDictionary<string, object?> errors, string instance, string type, int status = 0, string detail = "", string traceId = "") //IDictionary<string, object> extensions, string instance,  string type, int status =0, string detail = "")
        {
            Errors = errors ?? new Dictionary<string, object?>();
            Instance = instance;
            Type = type;
            Title = title;
            //Status = status;
            //Detail = detail;
            TraceId = traceId;
        }

        #region IEquatable

        // SEE: https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1.equals?view=netframework-4.7

        public bool Equals(DomainDataError other)
        {
            if (other == null)
                return false;

            return (this.Title == other.Title);
        }
        public override bool Equals(Object other)
        {
            if (other == null)
                return false;

            if (other.GetType() == typeof(string))
                return this.Title.Equals((string)other);

            var asError = other as DomainDataError;
            if (asError == null)
                return false;
            else
                return Equals(asError);
        }

        public override int GetHashCode()
        {
            return this.Title.GetHashCode();
        }

        #endregion

        public override string ToString()
        {
            return (string.IsNullOrEmpty(Title) ? "<null>" : Title) + " - " + (Errors != null ? string.Join(",", Errors) : "<null>" );
        }
    }

}