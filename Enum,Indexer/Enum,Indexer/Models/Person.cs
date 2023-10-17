using Enum_Indexer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum_Indexer.Models
{
    internal class Person
    {
        public Person(string fullname, Position position, Category category)
        {
            Fullname = fullname;
            Position = position;
            Category = category;
        }

        public string Fullname { get; set; }

        public Position  Position { get; set; }
        public Category Category { get; set; }

        public override string ToString()
        {
            return $"Fullname: {Fullname}, Position:{Position}, Category:{Category}";
        }
    }
}
