using Microsoft.EntityFrameworkCore;

namespace AbankingMicroERP.Models
{
	public class AbankingContext : DbContext
	{

		public virtual DbSet<Employee> Employees { get; set; }

		public virtual DbSet<Department> Departments { get; set; }

		public virtual DbSet<Language> Languages { get; set; }

		public virtual DbSet<NameTemplate> NameTemplates { get; set; }


		public AbankingContext(DbContextOptions<AbankingContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
		
		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>(entity =>
			{
				entity.HasIndex(x => x.DepartmentId);
				entity.HasIndex(x => x.LanguageId);
			});

			modelBuilder.Entity<Language>()
				.HasIndex(u => u.Name)
				.IsUnique();

			modelBuilder.Entity<Department>()
				.HasIndex(u => u.Name)
				.IsUnique();

			var language1 = new Language { Name = "C#" };
			var language2 = new Language { Name = "C++" };
			var language3 = new Language { Name = "PHP" };
			var language4 = new Language { Name = "JavaScript" };

			modelBuilder.Entity<Language>().HasData(language1, language2, language3, language4);

			var team1 = new Department { Name = "Backend" };
			var team2 = new Department { Name = "Desktop" };
			var team3 = new Department { Name = "Backend-old" };
			var team4 = new Department { Name = "Frontend" };

			modelBuilder.Entity<Department>().HasData(team1, team2, team3, team4);

			var employer1 = new Employee() { Name = "Aнна", Surname = "Пономарева", Age = 23, DepartmentId = team1.Id, LanguageId = language1.Id };
			var employer2 = new Employee() { Name = "Николай", Surname = "Денисов", Age = 41, DepartmentId = team2.Id, LanguageId = language1.Id };
			var employer3 = new Employee() { Name = "Дмитрий", Surname = "Максимов", Age = 32, DepartmentId = team3.Id, LanguageId = language1.Id };
			var employer4 = new Employee() { Name = "Евгений", Surname = "Акопян", Age = 43, DepartmentId = team4.Id, LanguageId = language1.Id };
			var employer5 = new Employee() { Name = "Елена", Surname = "Попова", Age = 21, DepartmentId = team1.Id, LanguageId = language1.Id };
			var employer6 = new Employee() { Name = "Лев", Surname = "Шабалин", Age = 19, DepartmentId = team1.Id, LanguageId = language1.Id };
			var employer7 = new Employee() { Name = "Максим", Surname = "Максимов", Age = 25, DepartmentId = team3.Id, LanguageId = language2.Id };
			var employer8 = new Employee() { Name = "Егор", Surname = "Егоров", Age = 28, DepartmentId = team4.Id, LanguageId = language3.Id };
			var employer9 = new Employee() { Name = "Никита", Surname = "Медведев", Age = 29, DepartmentId = team1.Id, LanguageId = language3.Id };
			var employer10 = new Employee() { Name = "Александр", Surname = "Наполеон", Age = 30, DepartmentId = team2.Id, LanguageId = language3.Id };
			var employer11 = new Employee() { Name = "Анатолий", Surname = "Анатольев", Age = 33, DepartmentId = team3.Id, LanguageId = language4.Id };
			var employer12 = new Employee() { Name = "Егор", Surname = "Попов", Age = 26, DepartmentId = team2.Id, LanguageId = language4.Id };
			var employer13 = new Employee() { Name = "Евегения", Surname = "Чеботько", Age = 26, DepartmentId = team1.Id, LanguageId = language4.Id };
			var employer14 = new Employee() { Name = "Стас", Surname = "Андропов", Age = 27, DepartmentId = team2.Id, LanguageId = language4.Id };
			var employer15 = new Employee() { Name = "Анастасия", Surname = "Ульянова", Age = 24, DepartmentId = team3.Id, LanguageId = language4.Id };

			modelBuilder.Entity<Employee>().HasData(
				employer1, employer2, employer3, employer4, employer5, 
				employer6, employer7, employer8, employer9, employer10, 
				employer11, employer12, employer13, employer14, employer15);

			var nameTemplate1 = new NameTemplate() { Name = "Александр" };
			var nameTemplate2 = new NameTemplate() { Name = "Дмитрий" };
			var nameTemplate3 = new NameTemplate() { Name = "Максим" };
			var nameTemplate4 = new NameTemplate() { Name = "Сергей" };
			var nameTemplate5 = new NameTemplate() { Name = "Андрей" };
			var nameTemplate6 = new NameTemplate() { Name = "Алексей" };
			var nameTemplate7 = new NameTemplate() { Name = "Артём" };
			var nameTemplate8 = new NameTemplate() { Name = "Илья" };
			var nameTemplate9 = new NameTemplate() { Name = "Кирилл" };
			var nameTemplate10 = new NameTemplate() { Name = "Михаил" };
			var nameTemplate11 = new NameTemplate() { Name = "Никита" };
			var nameTemplate12 = new NameTemplate() { Name = "Матвей" };
			var nameTemplate13 = new NameTemplate() { Name = "Роман" };
			var nameTemplate14 = new NameTemplate() { Name = "Егор" };
			var nameTemplate15 = new NameTemplate() { Name = "Анна" };
			var nameTemplate16 = new NameTemplate() { Name = "Олег" };
			var nameTemplate17 = new NameTemplate() { Name = "Анатолий" };
			var nameTemplate18 = new NameTemplate() { Name = "Ольга" };

			modelBuilder.Entity<NameTemplate>().HasData(
				nameTemplate1, nameTemplate2, nameTemplate3, nameTemplate4, nameTemplate5,
				nameTemplate6, nameTemplate7, nameTemplate8, nameTemplate9, nameTemplate10,
				nameTemplate11, nameTemplate12, nameTemplate13, nameTemplate14, nameTemplate15, 
				nameTemplate16, nameTemplate17, nameTemplate18);


		}
	}
}







