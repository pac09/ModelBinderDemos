using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BinderDemo.ValueProviders
{
    public class CustomValueProvider : IValueProvider
    {
        NameValueCollection qs;

        public CustomValueProvider(IQueryCollection queryString, string keyAllParameters)
        {
            if (queryString.Count > 0)
            {
                qs = new NameValueCollection();
                foreach (var kvp in queryString)
                {
                    qs.Add(kvp.Key, kvp.Value.ToString());
                }

                string allKeys = string.Join(",", queryString.Select(a => a.Key));
                qs.Add(keyAllParameters, allKeys);
            }
        }

        public bool ContainsPrefix(string prefix) => qs[prefix] != null;

        public ValueProviderResult GetValue(string key)
        {
            return new ValueProviderResult(qs[key], CultureInfo.CurrentCulture);
        }
    }
}
