using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xxf.Common.Exceptions
{
    public static class OperationResultExtensions
    {
        public static void Add(this IDictionary<string, object> map, string key, object value, string[] args)
        {
            map.Add(key, string.Format(value.ToString(), args));
        }
    }
}
