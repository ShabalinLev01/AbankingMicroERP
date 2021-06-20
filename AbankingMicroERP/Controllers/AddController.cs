using System.Linq;
using System.Threading.Tasks;
using AbankingMicroERP.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbankingMicroERP.Controllers
{
	public class AddController : Controller
	{
		private readonly AbankingContext _context;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context"></param>
		public AddController(AbankingContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get Form for add employee
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Add Employee to DB
		/// </summary>
		/// <param name="employee"></param>
		/// <returns></returns>
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