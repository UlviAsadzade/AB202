using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab202Console.Models
{
    internal class User
    {
		private string _userName;

		public string UserName
		{
			get { return _userName; }
			set { if (CheckName(value)) _userName = value; }
		}

		private string _password;

		public string Password
		{
			get { return _password; }
			set
			{
				if (CheckPassword(value))
				{
					_password = value;
				}
			}
		}

		public User(string username,string password)
		{
			Password= password;
			UserName= username;
		}

		bool CheckName(string word)
		{
			if (word.Length < 5)
			{
				return false; ;
			}

			return true;
		}

		bool CheckPassword(string word)
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

					if(digit>0 && lower>0 && upper>0)
					{
						return true;
					}
				}

			}

            return false;

        }



    }
}
