using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab202Console.Models
{
    internal class Website
    {
        public string  Name { get; set; }

        private User[] Users = new User[0];

        public void Add(User user)
        {
            Array.Resize(ref Users, Users.Length + 1);
            Users[Users.Length - 1] = user;
            
        }

        public void ShowAllUser()
        {
            if (Users.Length > 0)
            {
                foreach (var item in Users)
                {
                    Console.WriteLine(item.UserName);
                }
            }
            else
            {
                Console.WriteLine("Saytimizda user movcud deyil");
            }
        }

        public void Search(string word)
        {
            if (Users.Length > 0)
            {
                foreach (var item in Users)
                {
                    if (item.UserName.Contains(word))
                    {
                        Console.WriteLine(item.UserName);
                    }
                   
                  
                }
            }
            else
            {
                Console.WriteLine("Saytimizda user movcud deyil");
            }
        }

        public void Remove(string username)
        {
            User[] newArr = new User[0];

            foreach (var item in Users)
            {
                if (item.UserName != username)
                {
                    Array.Resize(ref newArr, newArr.Length + 1);
                    newArr[newArr.Length - 1] = item;

                }
            }

            Users = newArr;
        }
    }
}
