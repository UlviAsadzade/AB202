using Ab202Console.Models;

namespace Ab202Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Website site= new Website();

            string username;
            string password;
            string input;

            Console.WriteLine("Saytimiza xosgelmissiniz");
            Console.WriteLine("----------------------------------");

            do
            {
                Console.WriteLine("----------------------------------");

                Console.WriteLine("Secmek istediyiniz emeliyyati daxil edin");

                Console.WriteLine("1: Add User");
                Console.WriteLine("2: Show All User");
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
                            Console.WriteLine("Username daxil et");
                            username = Console.ReadLine();

                        } while (username.Length < 5);

                        do
                        {
                            Console.WriteLine("Password daxil et");
                            password = Console.ReadLine();

                        } while (!CheckPassword(password));

                        User user = new User(username, password);
                        site.Add(user);
                        break;

                    case "2":

                        site.ShowAllUser();
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

            } while (input!="0");

        }

        public  static bool CheckPassword(string word)
        {
            int digit = 0;
            int upper = 0;
            int lower = 0;

            if (word.Length > 5)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (Char.IsDigit(word[i]))
                    {
                        digit++;
                    }
                    else if (Char.IsUpper(word[i]))
                    {
                        upper++;
                    }
                    else if (Char.IsLower(word[i]))
                    {
                        lower++;
                    }

                    if (digit > 0 && lower > 0 && upper > 0)
                    {
                        return true;
                    }
                }

            }

            return false;

        }


    }
}