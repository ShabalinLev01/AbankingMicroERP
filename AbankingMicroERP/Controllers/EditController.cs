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

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context"></param>
		public EditController(AbankingContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Return view with filled form
		/// </summary>
		/// <param name="id">Employee's id</param>
		/// <returns></returns>
		public async Task<IActionResult> Index(Guid id)
		{
			var employer = await _context.Employees.FindAsync(id); 

			if (employer == null)
				return RedirectToAction("Index", "Home");
			return View(employer);
		}

		/// <summary>
		/// Get Edited Employee
		/// !NOTICE
		/// I think that it's method should be HttpPatch/Put, but we need use AJAX Request instead of Html Form.
		/// In this case (Test Task) I prefer use Html Form
		/// </summary>
		/// <param name="employee">Edited employee</param>
		/// <returns></returns>
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
	}
}