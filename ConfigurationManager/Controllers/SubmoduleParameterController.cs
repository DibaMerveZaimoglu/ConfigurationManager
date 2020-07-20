using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConfigurationManager.Controllers
{
    public class SubmoduleParameterController : Controller
    {
        private readonly ILogger<SubmoduleParameterController> _logger;
        private readonly ConfigurationManagerContext _context;

        public SubmoduleParameterController(ILogger<SubmoduleParameterController> logger, ConfigurationManagerContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult List(int submoduleId)
        {
            var submoduleWithParameterList = _context.Submodule.Include(i => i.SubmoduleParameters).FirstOrDefault(f => f.Id == submoduleId);

            return View(submoduleWithParameterList);
        }

        [HttpGet]
        public IActionResult Manage(int submoduleId, int id = 0)
        {
            var submoduleParameter = id > 0 ? _context.SubmoduleParameter.Find(id) : new SubmoduleParameter() { SubmoduleId = submoduleId };

            ViewBag.ParameterValueTypes = ParameterValueType.GetAll();

            return View(submoduleParameter);
        }

        [HttpPost]
        public IActionResult Manage(SubmoduleParameter submoduleParameter)
        {

            switch (submoduleParameter.Type)
            {
                case ParameterValueType.Bool:
                    if (!bool.TryParse(submoduleParameter.Value, out bool boolValue))
                    {
                        ModelState.AddModelError("Value", string.Format("{0} is not a valid boolean value.", submoduleParameter.Value));
                    }
                    break;
                case ParameterValueType.DateTime:
                    if (!DateTime.TryParse(submoduleParameter.Value, out DateTime dateTimeValue))
                    {
                        ModelState.AddModelError("Value", string.Format("{0} is not a valid datetime value.", submoduleParameter.Value));
                    }
                    break;
                case ParameterValueType.Decimal:
                    if (!decimal.TryParse(submoduleParameter.Value, out decimal decimalValue))
                    {
                        ModelState.AddModelError("Value", string.Format("{0} is not a valid decimal value.", submoduleParameter.Value));
                    }
                    break;
                case ParameterValueType.Integer:
                    if (!int.TryParse(submoduleParameter.Value, out int integerValue))
                    {
                        ModelState.AddModelError("Value", string.Format("{0} is not a valid integer value.", submoduleParameter.Value));
                    }
                    break;
                default:
                    ModelState.AddModelError("Error", "A valid value type required.");
                    break;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ParameterValueTypes = ParameterValueType.GetAll();

                return View(submoduleParameter);
            }

            var persistentSubmoduleParameter = submoduleParameter.Id > 0 ? _context.SubmoduleParameter.Find(submoduleParameter.Id) : new SubmoduleParameter();

            persistentSubmoduleParameter.Name = submoduleParameter.Name;
            persistentSubmoduleParameter.Description = submoduleParameter.Description;
            persistentSubmoduleParameter.IsActive = submoduleParameter.IsActive;
            persistentSubmoduleParameter.Type = submoduleParameter.Type;
            persistentSubmoduleParameter.Value = submoduleParameter.Value;
            persistentSubmoduleParameter.UpdatedDate = DateTime.Now;
            persistentSubmoduleParameter.CreatedDate = submoduleParameter.Id > 0 ? submoduleParameter.CreatedDate : DateTime.Now;

            if (persistentSubmoduleParameter.Id == 0)
            {
                _context.Entry(submoduleParameter).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }

            if (_context.SaveChanges() > 0)
            {
                TempData["SuccessAlert"] = String.Format("{0} is successfully saved.", submoduleParameter.Name);
            }

            else
            {
                TempData["ErrorAlert"] = String.Format("An error occured while saving {0}.", submoduleParameter.Name);
            }

            return RedirectToAction("List", new { submoduleId = submoduleParameter.SubmoduleId });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var submoduleParameter = _context.SubmoduleParameter.Find(id);

            if (submoduleParameter != null)
            {
                _context.Remove(submoduleParameter);
            }

            else
            {
                TempData["ErrorAlert"] = String.Format("Submodule parameter not found.");
            }

            if (_context.SaveChanges() > 0)
            {
                TempData["SuccessAlert"] = String.Format("{0} is successfully deleted.", submoduleParameter.Name);
            }

            else
            {
                TempData["ErrorAlert"] = String.Format("An error occured while deleting {0}.", submoduleParameter.Name);
            }

            return RedirectToAction("List", new { submoduleId = submoduleParameter.SubmoduleId });
        }
    }
}
