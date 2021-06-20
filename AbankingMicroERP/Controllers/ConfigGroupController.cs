using System;
using System.Linq;
using System.Threading.Tasks;
using AbankingMicroERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbankingMicroERP.Controllers
{
	public class ConfigGroupController : Controller
	{
		private readonly AbankingContext _gContext;

		public ConfigGroupController(AbankingContext gContext)
		{
			_gContext = gContext;
		}


		public IActionResult Index()
		{
			ViewBag.Departments = _gContext.Departments.ToList();
			ViewBag.Languages = _gContext.Languages.ToList();
			return View();
		}

		[HttpGet]
		public async Task<JsonResult> GetDepartments()
		{
			var departments = await _gContext.Departments.ToListAsync();
			return Json(departments);
		}


		[HttpGet]
		public async Task<JsonResult> GetLanguages()
		{
			var languages = await _gContext.Languages.ToListAsync();
			return Json(languages);
		}

		[HttpPost]
		public async Task<IActionResult> AddDepartment(Department department)
		{
			if (!ModelState.IsValid)
				return View(department);

			await _gContext.Departments.AddAsync(department);
			await _gContext.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> AddLanguage(Language language)
		{
			if (!ModelState.IsValid)
				return View(language);

			await _gContext.Languages.AddAsync(language);
			await _gContext.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> EditDepartment(Department department)
		{
			if (!ModelState.IsValid)
				return View("EditDepartment", department);

			var oldEmployer = await _gContext.Employees.FindAsync(department.Id);
			if (oldEmployer == null)
				return RedirectToAction("Index", "Home");

			oldEmployer.Name = department.Name;
			await _gContext.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> EditLanguage(Department department)
		{
			if (!ModelState.IsValid)
				return View("EditDepartment", department);

			var oldEmployer = await _gContext.Employees.FindAsync(department.Id);
			if (oldEmployer == null)
				return RedirectToAction("Index", "Home");

			oldEmployer.Name = department.Name;
			await _gContext.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Index(Employee employee)
		{
			if (!ModelState.IsValid)
				return View(employee);

			var oldEmployer = await _gContext.Employees.FindAsync(employee.Id);
			if (oldEmployer == null)
				return RedirectToAction("Index", "Home");

			oldEmployer.Name = employee.Name;
			oldEmployer.Surname = employee.Surname;
			oldEmployer.Age = employee.Age;
			oldEmployer.DepartmentId = employee.DepartmentId;
			oldEmployer.LanguageId = employee.LanguageId;
			await _gContext.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteDepartment(Guid id)
		{
			var department = await _gContext.Departments.FindAsync(id);

			if (department == null)
				return Json(new { success = false, message = "Error. Not Found Department", status = 404 });

			await _gContext.Employees.Where(x => x.DepartmentId == department.Id).ForEachAsync(x => x.DepartmentId = null);
			_gContext.Departments.Remove(department);
			await _gContext.SaveChangesAsync();
			return Json(new { success = true, message = "Ok", status = 200 });
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteLanguage(Guid id)
		{
			var language = await _gContext.Languages.FindAsync(id);

			if (language == null)
				return Json(new { success = false, message = "Error. Not Language User", status = 404 });

			await _gContext.Employees.Where(x => x.LanguageId == language.Id).ForEachAsync(x=>x.LanguageId = null);
			_gContext.Languages.Remove(language);
			await _gContext.SaveChangesAsync();
			return Json(new { success = true, message = "Ok", status = 200 });
		}
	}
}
