using System;
using System.Linq;
using System.Threading.Tasks;
using AbankingMicroERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbankingMicroERP.Controllers
{
	public class EditController : Controller
	{
		private readonly AbankingContext _context;

		public EditController(AbankingContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(Guid id)
		{
			var employer = await _context.Employees.FindAsync(id); 

			if (employer == null)
				return RedirectToAction("Index", "Home");
			return View(employer);
		}

		[HttpPost]
		public async Task<IActionResult> Index(Employee employee)
		{
			if (!ModelState.IsValid)
				return View(employee);

			var oldEmployer = await _context.Employees
				.FindAsync(employee.Id);
			if (oldEmployer == null)
				return RedirectToAction("Index", "Home");

			oldEmployer.Name = employee.Name;
			oldEmployer.Surname = employee.Surname;
			oldEmployer.Age = employee.Age;
			oldEmployer.DepartmentId = employee.DepartmentId;
			oldEmployer.LanguageId = employee.LanguageId;
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<JsonResult> GetNames(string prefix)
		{
			var namesTemplateList = await _context.NameTemplates.ToListAsync();

			if (!namesTemplateList.Any(x => x.Name.StartsWith(prefix)))
				return Json(null);

			var matchedNames = namesTemplateList
				.Where(x => x.Name.StartsWith(prefix))
				.Select(x => x.Name)
				.ToList();
			return Json(matchedNames);
		}

		[HttpGet]
		public async Task<JsonResult> GetDepartments()
		{
			var departments = await _context.Departments.ToListAsync();
			return Json(departments);
		}


		[HttpGet]
		public async Task<JsonResult> GetLanguages()
		{
			var languages = await _context.Languages.ToListAsync();
			return Json(languages);
		}
	}
}