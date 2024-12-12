using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xxf.Common.Exceptions
{
    public static class DomainDataErrorExtensions 
    {
        public static void Throw(this DomainDataError error)
        {
            throw new DomainDataErrorException(error);
        }
        public static void Throw(this DomainDataError error, IDictionary<string, object?> errors)
        {
            throw new DomainDataErrorException(error, errors);
        }
        public static void Throw(this DomainDataError error, string[] args, Exception exc)
        {
            throw new DomainDataErrorException(exc.Message, null, string.IsNullOrEmpty(error.Title)?"":string.Format(error.Title, args), null, 0, "", exc); //string.IsNullOrEmpty( error.Detail)?"":string.Format(error.Detail, args)
        }
    }
}
