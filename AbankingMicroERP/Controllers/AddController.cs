using System.Linq;
using System.Threading.Tasks;
using AbankingMicroERP.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbankingMicroERP.Controllers
{
	public class AddController : Controller
	{
		private readonly AbankingContext _context;

		public AddController(AbankingContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(Employee employee)
		{
			if (!ModelState.IsValid)
				return View(employee);

			await _context.Employees.AddAsync(employee);

			var isTemplate = _context.NameTemplates.Select(x=>x.Name).Contains(employee.Name);
			if (isTemplate)
				await _context.NameTemplates.AddAsync(new NameTemplate() {Name = employee.Name});

			await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}