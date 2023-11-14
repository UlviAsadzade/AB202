using Microsoft.AspNetCore.Mvc;
using MVCIntro.Models;

namespace MVCIntro.Controllers
{
	public class HomeController : Controller
	{
		List<Student> Students = new List<Student>
		{
			new Student(1,"Azer","Gasimzade"),
			new Student(2,"Shams","Rehimzade"),
			new Student(3,"Fidan","Behbudova"),
			new Student(4,"Nicat","Mecidov"),
		};

		public IActionResult Index()
		{
			ViewBag.Students = Students;

			return View();
		}

		public IActionResult About(int id)
		{
			Student student = Students.FirstOrDefault(x => x.Id == id);

			if(student is not null)
			{

				ViewBag.Stu = student;
			}

			return View();
		}
	}
}
