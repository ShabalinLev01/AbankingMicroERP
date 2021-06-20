using System.Linq;
using System.Threading.Tasks;
using AbankingMicroERP.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbankingMicroERP.Controllers
{
	public class AddController : Controller
	{
		private readonly AbankingContext _gContext;

		public AddController(AbankingContext gContext)
		{
			_gContext = gContext;
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

			await _gContext.Employees.AddAsync(employee);

			var isTemplate = _gContext.NameTemplates.Select(x=>x.Name).Contains(employee.Name);
			if (isTemplate)
				await _gContext.NameTemplates.AddAsync(new NameTemplate() {Name = employee.Name});

			await _gContext.SaveChangesAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}