using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_App.Utilities.Models.Interfaces
{
    internal interface IAccount
    {
        public int Id { get; set; }

        public double Balance { get; set; }

        public void Deposit(double amount);
        public void Withdraw(double amount);
    }
}
