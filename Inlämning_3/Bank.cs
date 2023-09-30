using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ImperativeToObjectOriented
{
    public class Account
    {
        public string Name;
        public decimal Balance;

        public void Deposit(decimal amount)
        {
            Balance += amount;
            Console.WriteLine(amount + " kr deposited into " + Name);
        }
        public void Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine(amount + " kr withdrawn from " + Name);
            }
            else
            {
                Console.WriteLine("Balance too low to withdraw");
            }
        }
        public void Transfer(decimal amount) //Osäker
        {
            if (Balance >= amount)
            {
                Withdraw(amount);
                Deposit(amount);
                Console.WriteLine(amount + " kr transfered from " + Name + " to " + Name);
            }
            else
            {
                Console.WriteLine("Balance too low to transfer");
            }
        }
        public void BuyShare(Share share, int amount)
        {
            decimal totalPrice = share.Price * amount;
            if (Balance >= totalPrice)
            {
                Withdraw(totalPrice);
                share.Amount += amount;
                Console.WriteLine("Bought " + amount + " shares of " + share.Company + " with account " + Name);
            }
            else
            {
                Console.WriteLine("Balance too low to buy shares");
            }
        }
        public void SellShare(Share share, int amount)
        {
            if (amount <= share.Amount)
            {
                decimal totalPrice = share.Price * amount;
                Deposit(totalPrice);
                share.Amount -= amount;
                Console.WriteLine("Sold " + amount + " shares of " + share.Company + " with account " + Name);
            }
            else
            {
                Console.WriteLine("Number of shares too low to sell");
            }
        }
    }

    public class Share
    {
        public string Company;
        public int Amount;
        public decimal Price;
    }

    public class Bank
    {
        // These two variables contain the user's accounts and shares.
        // They are static and so will be available automatically to all methods in this class.
        public static Account[] accounts =
        {
            new Account { Name = "Spar", Balance = 90000 },
            new Account { Name = "Kort", Balance = 5000 },
            new Account { Name = "Resor", Balance = 22000 }
        };

        public static Share[] shares =
        {
            new Share { Company = "Ericsson", Price = 72, Amount = 20 },
            new Share { Company = "H&M", Price = 129, Amount = 50 },
            new Share { Company = "AstraZeneca", Price = 713, Amount = 5 }
        };
        public static void ShowUserInfo()  //Oklar
        {
            Console.WriteLine("Your accounts:");
            foreach (Account account in accounts)
            {
                Console.WriteLine("- " + account.Name + " (" + account.Balance + " kr)");
            }
            Console.WriteLine();

            Console.WriteLine("Your shares:");
            foreach (Share share in shares)
            {
                Console.WriteLine("- " + share.Company + " (" + share.Amount + " at " + share.Price + " kr)");
            }
        }

        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            bool done = false;
            while (!done)
            {
                ShowUserInfo();
                Console.WriteLine();

                int option = ShowMenu("What do you want to do?", new[]
                {
                    "Deposit",
                    "Withdraw",
                    "Transfer",
                    "Buy shares",
                    "Sell shares",
                    "Exit"
                });
                Console.Clear();

                // Call one of the "Page" methods based on which option the user picks.
                if (option == 0)
                {
                    DepositPage();
                }
                else if (option == 1)
                {
                    WithdrawPage();
                }
                else if (option == 2)
                {
                    TransferPage();
                }
                else if (option == 3)
                {
                    BuySharePage();
                }
                else if (option == 4)
                {
                    SellSharePage();
                }
                else if (option == 5)
                {
                    done = true;
                }

                Console.WriteLine();
            }
        }

        

        public static void DepositPage()
        {
            int accountIndex = ShowAccountMenu("Select account to deposit into:");
            Account account = accounts[accountIndex];
            Console.WriteLine();

            Console.Write("Select amount to deposit: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Clear();
            account.Deposit(amount);
        }

        public static void WithdrawPage()
        {
            int accountIndex = ShowAccountMenu("Select account to withdraw from:");
            Account account = accounts[accountIndex];
            Console.WriteLine();

            Console.Write("Select amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Clear();
            account.Withdraw(amount);
        }

        public static void TransferPage()
        {
            int fromIndex = ShowAccountMenu("Select account to transfer from:");
            Account fromAccount = accounts[fromIndex];
            Console.WriteLine();

            int toIndex = ShowAccountMenu("Select account to transfer to:");
            Account toAccount = accounts[toIndex];
            Console.WriteLine();

            Console.Write("Select amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Clear();
            fromAccount.Transfer(amount);
            toAccount.Transfer(amount);
        }

        public static void BuySharePage()
        {
            int shareIndex = ShowShareMenu("Select share to buy:");
            Share share = shares[shareIndex];
            Console.WriteLine();

            Console.Write("Select amount to buy: ");
            int shareAmount = int.Parse(Console.ReadLine());

            int accountIndex = ShowAccountMenu("Select account to buy with:");
            Account account = accounts[accountIndex];

            Console.Clear();
            account.BuyShare(share, shareAmount);
        }

        public static void SellSharePage()
        {
            int shareIndex = ShowShareMenu("Select share to sell:");
            Share share = shares[shareIndex];
            Console.WriteLine();

            Console.Write("Select amount to sell: ");
            int shareAmount = int.Parse(Console.ReadLine());
            Console.WriteLine();

            int accountIndex = ShowAccountMenu("Select account to deposit into:");
            Account account = accounts[accountIndex];

            Console.Clear();
            account.SellShare(share, shareAmount);
        }

        public static int ShowAccountMenu(string prompt)
        {
            List<string> options = new List<string>();
            foreach (Account account in accounts)
            {
                options.Add(account.Name + " (" + account.Balance + " kr)");
            }

            return ShowMenu(prompt, options);
        }

        public static int ShowShareMenu(string prompt)
        {
            List<string> options = new List<string>();
            foreach (Share share in shares)
            {
                options.Add(share.Company + " (" + share.Amount + " at " + share.Price + " kr)");
            }

            return ShowMenu(prompt, options);
        }

        

        

        

        

        

        public static int ShowMenu(string prompt, IEnumerable<string> options)
        {
            if (options == null || options.Count() == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty list of options.");
            }

            Console.WriteLine(prompt);

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            // Calculate the width of the widest option so we can make them all the same width later.
            int width = options.MaxBy(option => option.Length).Length;

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count(); i++)
            {
                // Start by highlighting the first option.
                if (i == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options.ElementAt(i);
                // Pad every option to make them the same width, so the highlight is equally wide everywhere.
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                // First restore the previously selected option so it's not highlighted anymore.
                Console.CursorTop = top + selected;
                string oldOption = options.ElementAt(selected);
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                // Then find the new selected option.
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count() - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                // Finally highlight the new selected option.
                Console.CursorTop = top + selected;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options.ElementAt(selected);
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                // Place the cursor one step above the new selected option so that we can scroll and also see the option above.
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            // Afterwards, place the cursor below the menu so we can see whatever comes next.
            Console.CursorTop = top + options.Count();

            // Show the cursor again and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }
    }

    //[TestClass]
    //public class BankTest
    //{
    //    [TestMethod]
    //    public void DepositTest()
    //    {
    //        Account account = new Account { Name = "Kort", Balance = 500 };
    //        Bank.Deposit(account, 200);
    //        Assert.AreEqual(700, account.Balance);
    //    }

    //    [TestMethod]
    //    public void WithdrawTest()
    //    {
    //        Account account = new Account { Name = "Kort", Balance = 500 };
    //        Bank.Withdraw(account, 200);
    //        Assert.AreEqual(300, account.Balance);
    //    }

    //    [TestMethod]
    //    public void TransferTest()
    //    {
    //        Account fromAccount = new Account { Name = "Spar", Balance = 5000 };
    //        Account toAccount = new Account { Name = "Kort", Balance = 1000 };
    //        Bank.Transfer(fromAccount, toAccount, 200);
    //        Assert.AreEqual(4800, fromAccount.Balance);
    //        Assert.AreEqual(1200, toAccount.Balance);
    //    }
    //}
}