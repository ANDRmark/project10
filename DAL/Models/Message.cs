using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Message : Entity
    {
        public int ThemeId { get; set; }
        public string MessageBody { get; set; }

        public Theme Theme { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
