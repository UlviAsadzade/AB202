using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Models
{
    internal class Gallery
    {
        List<Car> Cars = new List<Car>();


        public void ShowAllCars()
        {
            if(Cars.Count > 0)
            {
                Cars.ForEach(car => Console.WriteLine(car));
            }
            else
            {
                Console.WriteLine("Kasibiq masin yoxdu");
            }
        }

        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public Car FindCarById(int id)
        {
            return Cars.FirstOrDefault(x => x.Id == id);
        }

        public Car FindCarByCarCode(string code)
        {
            return Cars.FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
        }

        public void RemoveCar(int id)
        {
            Cars.Remove(Cars.FirstOrDefault(x=>x.Id == id));
        }

        public List<Car> FindCarsBySpeedInterval(int max,int min)
        {
            return Cars.FindAll(x => x.Speed >= min && x.Speed <= max);
        }

        public void SumofAllCarsPrice()
        {
            Console.WriteLine(Cars.Sum(x=>x.Price));
        }

        public void ExpensiveCar()
        {
            Console.WriteLine(Cars.Max(x => x.Price));
        }

        public Car CheapCar()
        {
            int min = Cars.Min(x => x.Price);

            return Cars.Find(x => x.Price ==min);
        }
    }
}
