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
    public class RenameSectionBindingModel
    {
        [Required]
        public int SectionId { get; set; }
        [Required]
        public string NewSectionName { get; set; }
    }
    public class UpdateSectionBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
    public class DeleteSectionBindingModel
    {
        [Required]
        public int SectionId { get; set; }
    }
}
