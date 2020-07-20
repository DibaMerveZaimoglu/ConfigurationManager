using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigurationManager.Controllers
{
    public class SubmoduleController : Controller
    {
        private readonly ILogger<SubmoduleController> _logger;
        private readonly ConfigurationManagerContext _context;

        public SubmoduleController(ILogger<SubmoduleController> logger, ConfigurationManagerContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            var submoduleList = _context.Submodule.ToList();

            return View(submoduleList);
        }

        [HttpGet]
        public IActionResult Manage(int id = 0)
        {
            var submodule = id > 0 ? _context.Submodule.Find(id) : new Submodule();

            return View(submodule);
        }

        [HttpPost]
        public IActionResult Manage(Submodule submodule)
        {

            if (!ModelState.IsValid && submodule.Id > 0)
            {
                return View(submodule);
            }

            submodule.UpdatedDate = DateTime.Now;
            submodule.CreatedDate = submodule.Id > 0 ? submodule.CreatedDate : DateTime.Now;

            _context.Entry(submodule).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            if (_context.SaveChanges() > 0)
            {
                TempData["SuccessAlert"] = String.Format("{0} is successfully saved.", submodule.Name);
            }

            else
            {
                TempData["ErrorAlert"] = String.Format("An error occured while saving {0}.", submodule.Name);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var submodule = _context.Submodule.Find(id);

            if (submodule != null)
            {
                _context.Remove(submodule);
            }

            else
            {
                TempData["ErrorAlert"] = String.Format("Submodule not found.");
            }

            if (_context.SaveChanges() > 0)
            {
                TempData["SuccessAlert"] = String.Format("{0} is successfully deleted.", submodule.Name);
            }

            else
            {
                TempData["ErrorAlert"] = String.Format("An error occured while deleting {0}.", submodule.Name);
            }

            return RedirectToAction("List");
        }
    }
}
