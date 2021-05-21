using BinderDemo.Models.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinderDemo.ValueProviders
{
    public class CustomValueProviderFactory : IValueProviderFactory
    {
        private IOptions<BindingConfig> _bindingConfig;

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Requesting registered Services 
            var svcProvider = context.ActionContext.HttpContext.RequestServices;
            _bindingConfig = (IOptions<BindingConfig>)svcProvider.GetService(typeof(IOptions<BindingConfig>));

            var qs = context.ActionContext.HttpContext.Request.Query;
            if (qs != null && qs.Count > 0)
                context.ValueProviders.Insert(0, new CustomValueProvider(qs, _bindingConfig.Value.KeyAllParameters));

            return Task.CompletedTask;
        }
    }
}
