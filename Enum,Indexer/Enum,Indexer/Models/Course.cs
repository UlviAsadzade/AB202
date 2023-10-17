using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum_Indexer.Models
{
    internal class Course
    {
        public string Name { get; set; }

        Person[] People = new Person[0];

        public Course(string name)
        {
            Name = name;
        }

        public Person this[int index]
        {
            get
            {
               if(index < People.Length)
                {
                    return People[index];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if(index<People.Length)
                {
                    People[index] = value;
                }
            }
        }

        public void Add(Person person)
        {
            Array.Resize(ref People, People.Length+1);
            People[People.Length-1] = person;
        }

        public void GetPeopleByPosition(string pos)
        {
            foreach (var item in People)
            {
                if (item.Position.ToString().ToLower() == pos.ToLower())
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void GetPeopleByPosition(int pos)
        {
            foreach (var item in People)
            {
                if ((int)item.Position == pos)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void GetPeopleByCategory(string cat)
        {
            foreach (var item in People)
            {
                if (item.Position.ToString().ToLower() == cat.ToLower())
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void GetPeopleByCategory(int cat)
        {
            foreach (var item in People)
            {
                if ((int)item.Position == cat)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void ShowAll()
        {
            foreach (var item in People)
            {
                Console.WriteLine(item);
                Console.WriteLine("--------");
            }
        }
    }
}
