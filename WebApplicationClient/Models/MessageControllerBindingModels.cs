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
    public class MessageToUpdateBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ThemeId { get; set; }

        [Required]
        public string MessageBody { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
    public class DeleteMessageBindingModel
    {
        public int MessageId { get; set; }
    }
}
