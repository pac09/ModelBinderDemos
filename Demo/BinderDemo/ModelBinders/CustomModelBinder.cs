using BinderDemo.Models;
using BinderDemo.Models.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinderDemo.ModelBinders
{
    public class CustomModelBinder : ComplexTypeModelBinder
    {
        private readonly IDictionary<ModelMetadata, IModelBinder> _propertyBinders;
        public readonly ILoggerFactory _loggerFactory;
        private IOptions<BindingConfig> _bindingConfig;
        
        private List<string> defaultRequestKeyNames = new List<string>() { "FirstName", "LastName", "Phone", "EmailAddress" };

        public CustomModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders, ILoggerFactory loggerFactory)
        : base(propertyBinders, loggerFactory)
        {
            _propertyBinders = propertyBinders;
            _loggerFactory = loggerFactory;
        }

        protected override Task BindProperty(ModelBindingContext bindingContext)
        {
            string fieldName = bindingContext.FieldName;
            string fieldValue = bindingContext.ValueProvider.GetValue(fieldName).FirstValue;

            if (!String.IsNullOrEmpty(fieldValue))
            {
                bindingContext.Result = ModelBindingResult.Success(fieldValue);
                return Task.CompletedTask;
            }
            else if (fieldName.Equals(nameof(PersonaModel.Attributes)))
            {
                Dictionary<string, string> extraAttributes = BuildExtraAttributes(bindingContext);
                bindingContext.Result = ModelBindingResult.Success(extraAttributes);
                return Task.CompletedTask;
            }
            else
            {
                return base.BindProperty(bindingContext);
            }
        }

        protected override void SetProperty(ModelBindingContext bindingContext, string modelName, ModelMetadata propertyMetadata, ModelBindingResult result)
        {
            base.SetProperty(bindingContext, modelName, propertyMetadata, result);
        }

        private Dictionary<string, string> BuildExtraAttributes(ModelBindingContext context)
        {
            Dictionary<string, string> extraAttributes = new Dictionary<string, string>();

            // Requesting registered Services 
            var svcProvider = context.ActionContext.HttpContext.RequestServices;
            _bindingConfig = (IOptions<BindingConfig>)svcProvider.GetService(typeof(IOptions<BindingConfig>));

            //Get all Keys for Extra Parameters
            string allKeys = context.ValueProvider.GetValue(_bindingConfig.Value.KeyAllParameters).FirstValue;

            if (!String.IsNullOrEmpty(allKeys))
            {
                List<string> attribs = allKeys.Split(",").ToList();
                attribs = attribs
                    .Where(a => !defaultRequestKeyNames.Contains(a, StringComparer.InvariantCultureIgnoreCase))
                    .ToList();

                //Get all Values for Extra Parameters
                foreach (var parameter in attribs)
                {
                    var parameterValue = context.ValueProvider.GetValue(parameter).FirstValue;
                    extraAttributes.Add(parameter, parameterValue);
                }
            }

            return extraAttributes;
        }
    }
}
