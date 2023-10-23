using Delegate.Models;

namespace Delegate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gallery gallery = new Gallery();
            Car car = new Car("Mustang", 1000000, 400);
            gallery.AddCar(car);

            gallery.AddCar(new Car("zapi", 50, 40));

            gallery.ShowAllCars();

            Console.WriteLine("----------------------");

            Console.WriteLine( "Idye gore masin tap");

            Console.WriteLine(gallery.FindCarByCarCode("mu1001"));

            gallery.ShowAllCars();

            Console.WriteLine("----------------------");
            Console.WriteLine("suret araligi");

            List<Car> list = gallery.FindCarsBySpeedInterval(500, 100);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}