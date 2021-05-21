using BinderDemo.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BinderDemo.Models
{
    [ModelBinder(BinderType = typeof(CustomModelBinder))]
    public class PersonaModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public Dictionary<string, string> Attributes { get; set; }

        public PersonaModel()
        {
            Attributes = new Dictionary<string, string>();
        }
    }
}
