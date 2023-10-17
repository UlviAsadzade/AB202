using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstarct_Interface.Models
{
    internal class Student
    {
        static int Count;
        static int CodeCount = 100;

        public string  Code { get; set; }

        public Student(string name, string surname)
        {
            Name = name;
            Surname = surname;
            Count++;
            Id = Count;
            CodeCount++;
            Code = Name.Substring(0, 2).ToUpper() + CodeCount;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string GetInfo()
        {
            return $"Id: {Id}, Name: {Name}, Surname:{Surname}";
        }

        public override string ToString()
        {
            return GetInfo();
            
        }


    }
}
