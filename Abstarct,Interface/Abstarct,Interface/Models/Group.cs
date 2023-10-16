using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstarct_Interface.Models
{
    internal class Group
    {
        public string  Name { get; set; }

        Student[] Students = new Student[0];

        public void Add(Student student)
        {
            Array.Resize(ref Students, Students.Length+1);
            Students[Students.Length-1]=student;

        }

        public void Remove(int id)
        {
            Student[] newArr = new Student[0];

            for (int i = 0; i < Students.Length; i++)
            {
                if (Students[i].Id != id)
                {
                    Array.Resize(ref newArr, newArr.Length+1);
                    newArr[newArr.Length - 1] = Students[i];
                }
            }

            Students= newArr;
        }

        public void Show()
        {
            foreach (var item in Students)
            {
                Console.WriteLine(item);
            }
        }
    }
}
