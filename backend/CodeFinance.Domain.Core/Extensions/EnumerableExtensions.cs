using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CodeFinance.Domain.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> itemAction)
        {
            foreach (var item in items)
            {
                itemAction(item);
            }
        }

        public static string ObterStringLista(this List<string> items)
        {
            var str = new StringBuilder();

            foreach (var item in items)
            {
                str.Append(item + "; ");
            }

            return str.ToString();
        }
       
    }
}
