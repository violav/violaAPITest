using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xxf.Common.Exceptions
{
    public class OperationResult
    {
        public OperationResult()
        {
            OperationResultValidationErrors = new HashSet<OperationResultValidationError>();
        }    
        public bool Success { get; set; } = true;
        public ICollection<OperationResultValidationError> OperationResultValidationErrors { get; set; }
        public IDictionary<string, object?> ValidationErrors() => OperationResultValidationErrors.GroupBy(v => v.Key).ToDictionary(k => k.Key, e => (object?)e.Select(a => a.Value)) ;     
    }

    public class OperationResultValidationError
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }

    public static class ValidationErrorExtensions
    {
        public static void Add(this OperationResultValidationError error, OperationResult opResult, string[] args)
        {
            error.Value = string.Format(error.Value, args);
            opResult.OperationResultValidationErrors.Add(error);
        }
        public static void Add(this OperationResultValidationError error, OperationResult opResult, string arg)
        {
            error.Value = string.Format(error.Value, arg);
            opResult.OperationResultValidationErrors.Add(error);
        }
    }
}
