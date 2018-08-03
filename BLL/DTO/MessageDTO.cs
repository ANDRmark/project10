using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    class MessageDTO : EntityDTO
    {
        public int ThemeId { get; set; }
        public string MessageBody { get; set; }

        public ThemeDTO Theme { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
