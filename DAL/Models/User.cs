using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserInfo : Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ExternalUserId { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
