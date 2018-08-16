using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Section : Entity
    {
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<Theme> Themes { get; set; }
    }
}
