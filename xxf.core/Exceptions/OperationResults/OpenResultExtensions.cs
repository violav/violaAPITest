using OperationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xxf.Common.Exceptions;

namespace xxf.validation.OperationResults
{
    public static class OpenResultExtensions
    {
        public static void Throw(this Result error, string instance)
        {
            Dictionary<string, object> erroressages = new Dictionary<string, object>();
            foreach (var err in error.ValidationErrors)
            {
                erroressages.Add(err.Name, err.Message);
            }
            throw new DomainDataErrorException(error.ErrorMessage, erroressages, instance, "Data consistency failure", error.FailureReason, error.ErrorDetail);
        }
    }
}
