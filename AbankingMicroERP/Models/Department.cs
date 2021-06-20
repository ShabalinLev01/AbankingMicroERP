using System;
using System.ComponentModel.DataAnnotations;

namespace AbankingMicroERP.Models
{
	public class Department
	{
		[Key]
		[Required]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		public string Name { get; set; }
	}
}