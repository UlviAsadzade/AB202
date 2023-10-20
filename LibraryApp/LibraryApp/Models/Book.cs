using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    internal class Book
    {
        public Book(string name, string authorName, int pageCount)
        {
            Name = name;
            AuthorName = authorName;
            PageCount = pageCount;
            count++;
            Code = Name.Substring(0, 2).ToUpper() + count;
        }

        static int count = 0;

        public string  Name { get; set; }

        public string AuthorName { get; set; }

        public int PageCount { get; set; }

        public string Code { get; }

        public override string ToString()
        {
            return $"Name: {Name},AuthorName:{AuthorName},PageCount:{PageCount}, Code:{Code}";
        }
    }
}
