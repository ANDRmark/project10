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
        public RolesSet Roles { get; set; }
    }

    public class RolesSet
    {
        [Required]
        public bool IsUser { get; set; }

        [Required]
        public bool IsModerator { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }

    public class UpdateUserBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Email { get; set; }


        public virtual List<RoleBindingModel> Roles { get; set; }
    }
    public class RoleBindingModel
    {
        public string Name { get; set; }
    }
}