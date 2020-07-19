using System;
using System.IO;
using core.Entities;
using core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using web.ViewModels;

namespace web.Controllers
{
    public class PortfolioitemsController : Controller
    {
        private readonly IUnitofwork<PortfolioItem> _portfolio;
        private readonly IHostingEnvironment _host;

        public PortfolioitemsController(IUnitofwork<PortfolioItem>portfolio,IHostingEnvironment host)
        {
           _portfolio = portfolio;
            _host = host;
        }
        [HttpGet]
        [Route("Portfolioitems/Index")]
        public IActionResult Index()
        {
            var items = _portfolio.Entity.GetAll();
            
            return View(items);
        }
        [HttpGet]
        [Route("Portfolioitems/New")]
       public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public IActionResult New(PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {

                    var uploads = Path.Combine(_host.WebRootPath, @"img\portfolio");
                    var fullpath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                }


                PortfolioItem portfolioitem = new PortfolioItem
                {

                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };
                _portfolio.Entity.Insert(portfolioitem);
                _portfolio.Save();
                return RedirectToAction("index", "Portfolioitems");
            }
            return View(model);
        }

        [HttpGet]
        [Route("Portfolioitems/Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _portfolio.Entity.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            
            PortfolioViewModel model = new PortfolioViewModel
            {

                Name = item.Name,
                Description = item.Description,
                ImageUrl = item.ImageUrl
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PortfolioViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {

                    var uploads = Path.Combine(_host.WebRootPath, @"img\portfolio");
                    var fullpath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                }


                var portfoiloindb = _portfolio.Entity.GetById(model.Id);
                 portfoiloindb.Name = model.Name;
                portfoiloindb.Description = model.Description;
                portfoiloindb.ImageUrl = model.File.FileName;
            
                _portfolio.Entity.Update(portfoiloindb);
                _portfolio.Save();
                return RedirectToAction("index", "Portfolioitems");
            }

            return View(model);
        }
        [HttpGet]
        [Route("Portfolioitems/Details/{id}")]
        public IActionResult Details(Guid id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var item = _portfolio.Entity.GetById(id);
            return View(item);
        }
        [HttpGet]
        [Route("Portfolioitems/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var item = _portfolio.Entity.GetById(id);
            if (item==null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost , ActionName("Delete")]
        [Route("Portfolioitems/Delete/{id}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
          
            _portfolio.Entity.Delete(id);
            _portfolio.Save();
            return RedirectToAction("index", "Portfolioitems");
        }
    }
}