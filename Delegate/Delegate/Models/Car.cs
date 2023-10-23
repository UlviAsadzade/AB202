using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Models
{
    internal class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public int Price { get; set; }
        public int Speed { get; set; }

        public string Code { get; set; }

        static int count = 0;
        static int codeCount = 1000;

        public Car(string name, int price, int speed)
        {
            Name = name;
            Price = price;
            Speed = speed;
            count++;
            codeCount++;
            Id= count;
            Code = name.Substring(0, 2).ToUpper() + codeCount;
        }

        public override string ToString()
        {
            return $"Name:{Name}, Price:{Price},Speed:{Speed},Code:{Code},Id{Id}";
        }
    }
}
