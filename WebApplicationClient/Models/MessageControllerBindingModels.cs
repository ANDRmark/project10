using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplicationClient.Models
{
    public class NewMessageBindingModel
    {
        [Required]
        public string MessageBody { get; set; }

        [Required]
        public int ThemeId { get; set; }
    }
}
