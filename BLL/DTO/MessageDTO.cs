using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class MessageDTO : EntityDTO
    {
        public int ThemeId { get; set; }
        public string MessageBody { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        //public ThemeDTO Theme { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
