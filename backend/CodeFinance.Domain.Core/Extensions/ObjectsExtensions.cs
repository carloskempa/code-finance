using Newtonsoft.Json;
using System;

namespace CodeFinance.Domain.Core.Extensions
{
    public static class ObjectsExtensions
    {
        public static string Json(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
