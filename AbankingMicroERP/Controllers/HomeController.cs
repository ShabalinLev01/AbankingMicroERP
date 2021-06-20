using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AbankingMicroERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AbankingMicroERP.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AbankingContext _context;

		public HomeController(ILogger<HomeController> logger, AbankingContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var employees = _context.Employees.AsQueryable()
				.Include(x =>x.Department)
				.Include(x => x.Language)
				.ToList();

			return View(employees);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel 
				{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

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