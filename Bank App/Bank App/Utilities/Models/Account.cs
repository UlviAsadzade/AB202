using Bank_App.Utilities.Exceptions;
using Bank_App.Utilities.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_App.Utilities.Models
{
    internal class Account:IAccount
    {
        public int Id { get; set; }
        public double Balance { get; set; }

        List<Transaction> Transactions = new List<Transaction>();
        static int Count;

        public Account()
        {
            Id = ++Count;
            Balance= 0;
        }

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Transactions.Add(new Transaction(amount, true));
            }
            else
            {
                throw new InvalidAmountException("Incorrect amount");
            }
        }

        public void Withdraw(double amount)
        {
            if(amount > 0)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                    Transactions.Add(new Transaction(amount, false));


                }
                else
                {
                    throw new InsufficientFundsException("Your balance less than amount");
                }
            }
            else
            {
                throw new InvalidAmountException("Incorrect amount");
            }
        }

        public override string ToString()
        {
            return $"Id: {Id},Balance: {Balance}";
        }
    }
}
