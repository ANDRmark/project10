using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplicationClient.Models
{
    public class NewThemeBindingModel
    {
        [Required]
        public string ThemeName { get; set; }
    }

}