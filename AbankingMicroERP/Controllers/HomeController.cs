using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AbankingMicroERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbankingMicroERP.Controllers
{
	public class HomeController : Controller
	{
		private readonly AbankingContext _context;

		/// <summary>
		/// Constructor of HomeController
		/// </summary>
		/// <param name="context"></param>
		public HomeController(AbankingContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Return View with list employee
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			var employees = _context.Employees
				.Include(x =>x.Department)
				.Include(x => x.Language)
				.ToList();

			return View(employees);
		}

		/// <summary>
		/// ErrorPage
		/// </summary>
		/// <returns></returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel 
				{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		/// <summary>
		/// Delete Employer
		/// </summary>
		/// <param name="id">Employer's id</param>
		/// <returns>Status</returns>
		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			var employer = await _context.Employees.FindAsync(id);

			if (employer == null)
				return Json(new { success = false, message = "Error. Not Found User", status = 404 });

			_context.Employees.Remove(employer);
			await _context.SaveChangesAsync();
			return Json(new { success = true, message = "Ok", status = 200 });
		}

		/// <summary>
		/// Get Names for Autocomplete
		/// </summary>
		/// <param name="prefix">Prefix of name</param>
		/// <returns>Matched names</returns>
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
	}
}