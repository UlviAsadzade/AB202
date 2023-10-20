using LibraryApp.Models;

namespace LibraryApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library("Ali");
            string input;
            string name;
            string authorname;
            int page;
            string pageCount;



            Console.WriteLine("Kitabxanamiza xosgelmissiniz");
            Console.WriteLine("----------------------------------");

            do
            {
                Console.WriteLine("----------------------------------");

                Console.WriteLine("Secmek istediyiniz emeliyyati daxil edin");

                Console.WriteLine("1: Add Book");
                Console.WriteLine("2: Show All Book");
                Console.WriteLine("3: Search");
                Console.WriteLine("4: Remove");
                Console.WriteLine("0: Exit");
                Console.WriteLine("----------------------------------");

                input = Console.ReadLine();

                switch (input)
                {
                    case "1":

                        do
                        {
                            Console.WriteLine("Kitabin adini daxil edin");
                            name = Console.ReadLine();

                        } while (name.Length < 2);

                        do
                        {
                            Console.WriteLine("Yazici adini daxil et");
                            authorname = Console.ReadLine();

                        } while (authorname.Length<5);
                        do
                        {
                            Console.WriteLine("sehife sayini daxil et");
                             pageCount = Console.ReadLine();

                        } while (!int.TryParse(pageCount,out page));


                        library.Add(new Book(name,authorname,page));
                        
                        break;

                    case "2":

                        library.ShowAllBooks();
                        break;

                    case "3":
                        Console.WriteLine("axtarmaq istediyiniz sozu daxil edin");
                        string word = Console.ReadLine();
                        site.Search(word);
                        break;

                    case "4":
                        Console.WriteLine("Silmek istediyiniz userin username daxil edin");
                        string uname = Console.ReadLine();
                        site.Remove(uname);
                        break;

                    default:
                        break;
                }

            } while (input != "0");
        }
    }
}