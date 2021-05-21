using BinderDemo.Attributes;
using BinderDemo.ModelBinders;
using BinderDemo.Models;
using BinderDemo.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BinderDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<EncryptionConfig> _config;

        public HomeController(ILogger<HomeController> logger, IOptions<EncryptionConfig> config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Base64ValueProvider]
        public IActionResult GetPersona([ModelBinder(Name = "PersonaModel", BinderType = typeof(CustomModelBinder))] PersonaModel data)
        {
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region PrivateMethods

        private RouteValueDictionary GenerateRouteDictionary(ICollection<string> keys)
        {
            RouteValueDictionary qs = new RouteValueDictionary();

            foreach (var key in keys)
            {
                qs.Add(key, Request.Form[key]);
            }

            return qs;
        }

        private static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        private string GenerateToken()
        {
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            DateTime currentDate = DateTime.Now;

            long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;

            return elapsedTicks.ToString();
        }
        #endregion
    }
}
