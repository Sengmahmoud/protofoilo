using System;
using System.Collections.Generic;
using System.Text;

namespace core.Entities
{
  public class Adress : EntityBase
    {
        public string City { get; set; }
        public string Street { get; set; }
        public byte Number { get; set; }
    }
}
