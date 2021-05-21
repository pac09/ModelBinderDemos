using BinderDemo.Extentions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BinderDemo.ValueProviders
{
    public class Base64ParamsValueProvider : IValueProvider
    {
        NameValueCollection qs;

        public Base64ParamsValueProvider(StringValues encodedParams, string keyAllParameters)
        {
            string decodedToken = Convert.ToString(encodedParams).Base64Decode();
            var queryStringDictionary = QueryHelpers.ParseQuery(decodedToken);

            if (queryStringDictionary.Count > 0)
            {
                qs = new NameValueCollection();
                foreach (var kvp in queryStringDictionary)
                {
                    qs.Add(kvp.Key, kvp.Value.ToString());

                }

                string allKeys = string.Join(",", queryStringDictionary.Select(a => a.Key));
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
