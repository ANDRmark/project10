﻿using System;
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

        [Required]
        public int SectionId { get; set; }
    }
    public class ThemeToUpdateBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int SectionId { get; set; }
    }
    public class ThemeDeleteBindingModel
    {
        public int ThemeId { get; set; }
    }


}