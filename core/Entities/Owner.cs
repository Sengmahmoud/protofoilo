using System;
using System.Collections.Generic;
using System.Text;

namespace core.Entities
{
   public class Owner : EntityBase
    {
        public string FullName { get; set; }
        public string Profile { get; set; }
        public string Avtar { get; set; }
        public Adress Address { get; set; }
    }
}
