namespace ConsolePL
{
    using System;
    using System.Collections.Generic;
    using BLL.Interface.Entities;
    using BLL.Interface.Interfaces;
    using DependencyResolver;
    using Ninject;

    public class Program
    {
        private static readonly IKernel resolver;

        static Program()
        {
            resolver = new StandardKernel();
            resolver.ConfigurateResolver();
        }

        public static void Main(string[] args)
        {
            IBankManager bankManager = resolver.Get<IBankManager>();

            try
            {
                Console.WriteLine("AddTest");
                Addtest(bankManager);

                Console.WriteLine("WithdrawMoneyTest");
                WithdrawMoneyTest(bankManager);

                Console.WriteLine("DepositMoneyTest");
                DepositMoneyTest(bankManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }

        private static void Addtest(IBankManager bankManager)
        {
            bankManager.OpenAccount("fname1", "lName", 100m, AccountType.GoldAccount);
            bankManager.OpenAccount("fname2", "lName2", 200m, AccountType.PlatinumAccount);

            ShowAccouts(bankManager.GetAccounts());
        }

        private static void WithdrawMoneyTest(IBankManager bankManager)
        {
            bankManager.WithdrawMoney("2", 4m);
            ShowAccouts(bankManager.GetAccounts());
        }

        private static void DepositMoneyTest(IBankManager bankManager)
        {
            bankManager.DepositMoney("2", 8m);
            ShowAccouts(bankManager.GetAccounts());
        }

        private static void ShowAccouts(IEnumerable<BankAccount> accounts)
        {
            foreach (var accout in accounts)
            {
                Console.WriteLine(string.Format($"First name: {accout.FirstName,-20} Balance: {accout.Balance,-15} Number: {accout.Number}"), accout);
                Console.WriteLine(string.Format($"Last name: {accout.LastName,-21} Status: {accout.Status,-17} Type: {accout.GetType()}"), accout);
            }

            Console.WriteLine(new string('-', 80));
        }

        private static void ShowAccouts(params BankAccount[] accounts)
        {
            foreach (var accout in accounts)
            {
                Console.WriteLine(string.Format($"First name: {accout.FirstName,-20} Balance: {accout.Balance,-15} Number: {accout.Number}"), accout);
                Console.WriteLine(string.Format($"Last name: {accout.LastName,-21} Status: {accout.Status,-17} Type: {accout.GetType()}"), accout);
            }

            Console.WriteLine(new string('-', 80));
        }
    }
}
