using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinderDemo.Models.Settings
{
    public class BindingConfig
    {
        public string DefaultEncodedParameter { get; set; }
        public string KeyAllParameters { get; set; }
        public bool EnableBase64Encoding { get; set; }
    }
}
