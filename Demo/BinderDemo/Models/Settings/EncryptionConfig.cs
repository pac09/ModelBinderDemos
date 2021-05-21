using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinderDemo.Models.Settings
{
    public class EncryptionConfig
    {
        public string HashKey { get; set; }
        public string Salt { get; set; }
    }
}
