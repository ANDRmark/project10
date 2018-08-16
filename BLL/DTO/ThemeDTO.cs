using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ThemeDTO : EntityDTO
    {
        public string Title { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        //public virtual ICollection<MessageDTO> Messages { get; set; }

        public DateTime CreateDate { get; set; }

        public int SectionId { get; set; }
        public SectionDTO Section { get; set; }
    }
}
