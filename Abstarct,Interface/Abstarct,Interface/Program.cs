using Abstarct_Interface.Extensions;
using Abstarct_Interface.Models;

namespace Abstarct_Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student stu1 = new Student("Shems", "Rehimzade");
            Student stu2 = new Student("Azer", "Gasimzade");
            Student stu3 = new Student("Amin", "Pashayev");

            Group group = new Group();

            group.Add(stu1);
            group.Add(stu2);
            group.Add(stu3);

            group.Show();

            Console.WriteLine("-------------------------");

            group.Remove(3);

            group.Show();

        }
    }

 
}