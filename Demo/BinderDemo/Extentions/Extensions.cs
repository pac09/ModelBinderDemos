using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinderDemo.Extentions
{
    public static class Extensions
    {
        public static string Base64Encode(this string plainText)
        {
            if (plainText == null)
                plainText = String.Empty;

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception)
            {
                return base64EncodedData;
            }
        }
    }
}
