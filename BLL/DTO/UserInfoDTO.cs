using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class UserInfoDTO : EntityDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ExternalUserId { get; set; }
        public string PasswordHash { get; set; }
    }
}
