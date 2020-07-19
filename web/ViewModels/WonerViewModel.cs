using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace web.ViewModels
{
    public class WonerViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Profile { get; set; }
        public string Avtar { get; set; }
        public IFormFile File { get; set; }
    }
}
