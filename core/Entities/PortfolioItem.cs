using System;
using System.Collections.Generic;
using System.Text;

namespace core.Entities
{
   public class PortfolioItem :EntityBase
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
