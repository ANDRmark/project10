using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplicationClient.Models
{
    public class NewSectionBindingModel
    {
        [Required]
        public string SectionName { get; set; }
    }
}
