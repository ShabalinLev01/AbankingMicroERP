using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbankingMicroERP.Models
{
    public class Employee
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Имя не заполнено")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Фамилия не заполнена")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Возраст не указан")]
        [Range(12, int.MaxValue, ErrorMessage = "Возраст должен быть больше, чем {1}")]
        public int Age { get; set; }
        
        public Guid? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        
        public Guid? LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
    }
}