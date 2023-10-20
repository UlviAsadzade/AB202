using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    internal class Library
    {
        public string Name { get; set; }

        List<Book> Books = new List<Book>();

        public Library(string name)
        {
            Name = name;
        }

        public void Add(Book book)
        {
            Books.Add(book);
        }

        public List<Book> FindAllBooksByName(string name)
        {
            List<Book> list = new List<Book>();

            foreach (Book book in Books)
            {
                if (book.Name.Contains(name))
                {
                    list.Add(book);
                }
            }

            return list;
        }

        public Book FindBookByCode(string value)
        {
            foreach (Book book in Books)
            {
                if (book.Code == value)
                {
                    return book;
                }
            }

            return null;
            
        }

        public List<Book> Search(string name)
        {
            List<Book> list = new List<Book>();

            foreach (Book book in Books)
            {
                if (book.Name.Contains(name) || book.AuthorName.Contains(name))
                {
                    list.Add(book);
                }
            }

            return list;
        }

        public List<Book> FindAllBooksByPageCountRange(int max,int min)
        {
            List<Book> list = new List<Book>();

            foreach (Book book in Books)
            {
                if (book.PageCount>=min && book.PageCount<=max)
                {
                    list.Add(book);
                }
            }

            return list;
        }

        public void Remove(string value)
        {
            foreach (Book book in Books)
            {
                if (book.Code==value)
                {
                    Books.Remove(book);
                    break;
              
                }
            }

        }

        public void ShowAllBooks()
        {
            foreach (var item in Books)
            {
                Console.WriteLine(item);
            }

        }
    }
}
