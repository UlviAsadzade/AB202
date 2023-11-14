namespace MVCIntro.Models
{
	public class Student
	{
		public Student(int ıd, string name, string surname)
		{
			Id = ıd;
			Name = name;
			Surname = surname;
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
	}
}
