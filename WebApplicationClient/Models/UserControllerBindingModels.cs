using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplicationClient.Models
{
    public class DeleteUserBindingModel
    {
        [Required]
        public int UserId { get; set; }
    }

    public class SetRolesBindingModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public RolesSet Roles {get; set;}

    public class RolesSet
    {
        public bool IsUser { get; set; }
        public bool IsModerator { get; set; }
        public bool IsAdmin { get; set; }
    }
    }
}