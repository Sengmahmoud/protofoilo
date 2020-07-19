using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using core.Entities;
using core.Interface;
using Infrastructure.Unitofwork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using web.ViewModels;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitofwork<Owner> _Owner;
        private readonly IUnitofwork<PortfolioItem> _portfolioitems;
        private readonly IHostingEnvironment _host;
        public HomeController(IUnitofwork<Owner> unitofwork , 
            IUnitofwork<PortfolioItem> portfolioitems , IHostingEnvironment host)
        {
            _Owner = unitofwork;
            _portfolioitems = portfolioitems;
            _host = host;
        }

        public IActionResult Index()
        {
          var owner = _Owner.Entity.GetAll().First();
            var portolioitems = _portfolioitems.Entity.GetAll();
            var homeviewmode = new HomeViewModel
            {
                Owner = owner,
                PortfolioItems=portolioitems
                
              };
            return View(homeviewmode);
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        [Route("Home/Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _Owner.Entity.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            WonerViewModel model = new WonerViewModel
            {

                FullName = item.FullName,
                Profile = item.Profile,
                Avtar = item.Avtar
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(WonerViewModel owner)
        {

            if (ModelState.IsValid)
            {
                if (owner.File != null)
                {

                    var uploads = Path.Combine(_host.WebRootPath, @"img");
                    var fullpath = Path.Combine(uploads, owner.File.FileName);
                    owner.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                }


                var ownerindb = _Owner.Entity.GetById(owner.Id);
                ownerindb.FullName = owner.FullName;
                ownerindb.Profile = owner.Profile;
                ownerindb.Avtar = owner.File.FileName;

                _Owner.Entity.Update(ownerindb);
                _Owner.Save();
                return RedirectToAction("index", "Home");
            }

            return View(owner);
        }
    }
}