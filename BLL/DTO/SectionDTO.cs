using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class SectionDTO : EntityDTO
    {
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<ThemeDTO> Themes { get; set; }
    }
}
