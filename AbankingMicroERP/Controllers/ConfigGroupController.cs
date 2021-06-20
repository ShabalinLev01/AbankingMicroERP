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
		private readonly AbankingContext _context;

		public ConfigGroupController(AbankingContext context)
		{
			_context = context;
		}


		public IActionResult Index()
		{
			ViewBag.Departments = _context.Departments.ToList();
			ViewBag.Languages = _context.Languages.ToList();
			return View();
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

		#region AddRegion

		public IActionResult AddDepartment()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddDepartment(Department department)
		{
			var isExist = _context.Departments
				.ToListAsync().Result
				.Any(x => string.Equals(x.Name, department.Name, StringComparison.CurrentCultureIgnoreCase));
			if (isExist)
				ModelState.AddModelError("Name", "Такой отдел уже добавлен");

			if (!ModelState.IsValid)
				return View(department);

			_context.Departments.Add(department);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		public IActionResult AddLanguage()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddLanguage(Language language)
		{
			var isExist = _context.Languages
				.ToListAsync().Result
				.Any(x => string.Equals(x.Name, language.Name, StringComparison.InvariantCultureIgnoreCase));
			if (isExist)
				ModelState.AddModelError("Name", "Такой язык уже добавлен");

			if (!ModelState.IsValid)
				return View(language);

			_context.Languages.Add(language);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		#endregion

		#region EditRegion

		public async Task<IActionResult> EditDepartment(Guid id)
		{
			var department = await _context.Departments.FindAsync(id);

			if (department == null)
				return RedirectToAction("Index", "Home");
			return View(department);
		}

		[HttpPost]
		public async Task<IActionResult> EditDepartment(Department department)
		{
			var isExist = _context.Languages
				.ToListAsync().Result
				.Any(x => string.Equals(x.Name, department.Name, StringComparison.InvariantCulture));
			if (isExist)
				ModelState.AddModelError("Name", "Такой отдел уже добавлен");

			if (!ModelState.IsValid)
				return View(department);

			var oldDepartment = await _context.Departments.FindAsync(department.Id);
			if (oldDepartment == null)
				return RedirectToAction("Index", "Home");

			oldDepartment.Name = department.Name;
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> EditLanguage(Guid id)
		{
			var language = await _context.Languages.FindAsync(id);

			if (language == null)
				return RedirectToAction("Index", "Home");
			return View(language);
		}

		[HttpPost]
		public async Task<IActionResult> EditLanguage(Language language)
		{
			var isExist = _context.Languages
				.ToListAsync().Result
				.Any(x=> string.Equals(x.Name, language.Name, StringComparison.InvariantCulture));
			if (isExist)
				ModelState.AddModelError("Name", "Такой язык уже добавлен");

			if (!ModelState.IsValid)
				return View(language);

			var oldLanguage = await _context.Languages.FindAsync(language.Id);
			if (oldLanguage == null)
				return RedirectToAction("Index", "Home");

			oldLanguage.Name = language.Name;
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		#endregion

		#region Delete Region


		[HttpDelete]
		public async Task<IActionResult> DeleteDepartment(Guid id)
		{
			var department = await _context.Departments.FindAsync(id);

			if (department == null)
				return Json(new { success = false, message = "Error. Not Found Department", status = 404 });

			await _context.Employees
				.Where(x => x.DepartmentId == department.Id)
				.ForEachAsync(x => x.DepartmentId = null);
			_context.Departments.Remove(department);
			await _context.SaveChangesAsync();

			return Json(new { success = true, message = "Ok", status = 200 });
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteLanguage(Guid id)
		{
			var language = await _context.Languages.FindAsync(id);

			if (language == null)
				return Json(new { success = false, message = "Error. Not Language User", status = 404 });

			await _context.Employees
				.Where(x => x.LanguageId == language.Id)
				.ForEachAsync(x=>x.LanguageId = null);
			_context.Languages.Remove(language);
			await _context.SaveChangesAsync();

			return Json(new { success = true, message = "Ok", status = 200 });
		}

		#endregion
	}
}
