﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Theme : Entity
    {
        public string Title { get; set; }

        public ICollection<Message> Messages { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
