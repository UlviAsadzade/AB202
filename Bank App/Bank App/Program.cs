using Bank_App.Utilities.Exceptions;
using Bank_App.Utilities.Models;

namespace Bank_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            string input;

            Console.WriteLine("Welcome to Bank");


            do
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("1 - Create account");
                Console.WriteLine("2 - Show All Accounts");
                Console.WriteLine("3 - Deposit");
                Console.WriteLine("4 - Withdraw");
                Console.WriteLine("5 - Transfer");
                Console.WriteLine("0 - Exit");

                Console.WriteLine("Please choose option");
                Console.WriteLine("---------------------------");


                input = Console.ReadLine();

                switch (input)
                {
                    case "1": 
                        Console.Clear();
                        bank.CreateAccount();
                        
                        break;
                        
                    case "2":
                        Console.Clear();

                        bank.ShowAllAccounts();
                        break;

                    case "3":
                        Console.Clear();


                        try
                        {
                            bank.DepositMoney(GetId(), GetAmount());
                        }
                        catch (AccountNotFoundException e)
                        {

                            Console.WriteLine(e.Message);

                        }
                        catch (InvalidAmountException e)
                        {

                            Console.WriteLine(e.Message);

                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case "4":
                        Console.Clear();

                        try
                        {
                            bank.WithdrawMoney(GetId(), GetAmount());
                        }
                        catch(InsufficientFundsException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (AccountNotFoundException e)
                        {

                            Console.WriteLine(e.Message);

                        }
                        catch (InvalidAmountException e)
                        {

                            Console.WriteLine(e.Message);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case "5":
                        Console.Clear();

                        Console.WriteLine("You will see two id for from account and to account, please dont mix");
                        try
                        {
                            bank.TransferMoney(GetId(),GetId(), GetAmount());
                        }
                        catch (InsufficientFundsException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (AccountNotFoundException e)
                        {

                            Console.WriteLine(e.Message);

                        }
                        catch (InvalidAmountException e)
                        {

                            Console.WriteLine(e.Message);

                        }
                        catch(SameAccountException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;


                    default:
                        break;
                }

            } while (input != "0");
        }

        static int GetId()
        {
            int id;
            string idStr;
            do
            {
                Console.WriteLine("Please insert Id");
                idStr = Console.ReadLine();


            } while (!int.TryParse(idStr,out id));

            return id;
        }

        static double GetAmount()
        {
            double amount;
            string idStr;
            do
            {
                Console.WriteLine("Please insert amount");
                idStr = Console.ReadLine();


            } while (!double.TryParse(idStr, out amount));

            return amount;
        }
    }
}