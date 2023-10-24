using Bank_App.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_App.Utilities.Models
{
    internal class Bank
    {
        public int Id { get; set; }
        static int count;

        List<Account> Accounts = new List<Account>();

        public Bank()
        {
            Id=++count;
        }

        public void CreateAccount()
        {
            Account account = new Account();
            Accounts.Add(account);

            Console.WriteLine($"Id: {account.Id},Balance:{account.Balance}");
        }

        public void DepositMoney(int id,double amount)
        {
            if (!CheckAccount(id))
            {
                throw new AccountNotFoundException("Account does not fount");
            }
            else
            {

                Account account = Accounts.Find(x => x.Id == id);

                account.Deposit(amount);


            }
        }

        public void WithdrawMoney(int id, double amount)
        {
            if (!CheckAccount(id))
            {
                throw new AccountNotFoundException("Account does not fount");
            }
            else
            {

                Account account = Accounts.Find(x => x.Id == id);

                account.Withdraw(amount);


            }
        }

        public void TransferMoney(int fromId,int toId,double amount)
        {
            if (!CheckAccount(fromId))
            {
                throw new AccountNotFoundException("Account does not fount");
            }

            if (!CheckAccount(toId))
            {
                throw new AccountNotFoundException("Account does not fount");

            }

            Account fromAccount = Accounts.Find(x=>x.Id==fromId);
            Account toAccount = Accounts.Find(x=>x.Id==toId);

            if (fromAccount == toAccount)
            {
                throw new SameAccountException("You cannot transfer to yourself");
            }
            else
            {
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);
            }

        }

        public void ShowAllAccounts()
        {
            if (Accounts.Count > 0)
            {
                Accounts.ForEach(x => Console.WriteLine(x));
            }
            else
            {
                Console.WriteLine("Our Bank is empty");
            }
        }

        public bool CheckAccount(int id)
        {
            Account account = Accounts.Find(x => x.Id == id);

            if (account == null)
            {
                return false;
            }
             
            return true;
        }
    }
}
