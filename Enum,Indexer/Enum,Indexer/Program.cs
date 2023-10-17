using Enum_Indexer.Models;

namespace Enum_Indexer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Course course = new Course("Ab202");

            Person person = new Person("Ulvi Asadzade", Utilities.Position.Teacher, Utilities.Category.Programming);
            Person person2 = new Person("Ali Aglayan", Utilities.Position.Student, Utilities.Category.Programming);
            Person person3 = new Person("Amin Pashayev", Utilities.Position.Student, Utilities.Category.HelpDesk);
            Person person4 = new Person("Azer Gasimzade", Utilities.Position.Student, Utilities.Category.Design);

            course.Add(person);
            course.Add(person2);
            course.Add(person3);
            course.Add(person4);

            course.ShowAll();
            Console.Clear();

            course.GetPeopleByPosition("student");
            course.GetPeopleByPosition(2);
            Console.Clear();


        }
    }
}