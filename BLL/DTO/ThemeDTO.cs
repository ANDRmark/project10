using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ThemeDTO : EntityDTO
    {
        public string Title { get; set; }

        public virtual ICollection<MessageDTO> Messages { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
