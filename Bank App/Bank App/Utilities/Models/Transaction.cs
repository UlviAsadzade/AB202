using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_App.Utilities.Models
{
    internal class Transaction
    {
        public int Id { get; set; }
        static int Count;
        public double  Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public bool TransactionType { get; set; }  // truedursa Deposit, false-dursa withdraw

        public Transaction(double amount,bool type)
        {
            Id = ++Count;
            TransactionDate= DateTime.Now;
            TransactionType= type;
            Amount = amount;
            
        }
    }
}
