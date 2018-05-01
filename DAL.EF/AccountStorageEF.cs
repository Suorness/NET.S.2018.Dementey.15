namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DAL.EF.Entities;
    using DAL.EF.Mappers;
    using DAL.Interface.DTO;
    using DAL.Interface.Exceptions;
    using DAL.Interface.Interfaces;

    /// <summary>
    /// Data storage based on Enity Framework.
    /// </summary>
    public class AccountStorageEF : IAccountStorage
    {
        /// <summary>
        /// Adds a new account to the repository.
        /// </summary>
        /// <param name="account">New account.</param>
        /// <exception cref="StorageException">
        /// It is thrown in the event of a storage error.
        /// </exception>
        public void AddAccount(AccountDal account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var context = new BankAccountContext())
            {
                context.Accounts.Add(account.ToAccountEF());
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all BankAccount <see cref="AccountDal"/> from the repository.
        /// </summary>
        /// <returns>
        /// BankAccount  <see cref="BankAccount"/> from the repository.
        /// </returns>
        /// <exception cref="StorageException">
        /// It is thrown out in case of storage problems.
        /// </exception>
        public IEnumerable<AccountDal> GetBankAccounts()
        {
            var list = new List<AccountEF>();
            using (var context = new BankAccountContext())
            {
                list.AddRange(context.Accounts
                    .Include(acc => acc.AccountType));
            }

            return list
                .Select(ac => ac.ToAccountDalFromEF())
                .ToArray();
        }

        /// <summary>
        /// A method that removes a account <paramref name="account"/> from the repository.
        /// </summary>
        /// <param name="account">
        /// The Account for removal.
        /// </param>
        /// <exception cref="StorageException">
        /// It is thrown out in case of storage problems.
        /// </exception>
        public void RemoveAccount(AccountDal account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var context = new BankAccountContext())
            {
                var accountForRemove = FindAccount(account, context);

                if (ReferenceEquals(accountForRemove, null))
                {
                    throw new AccountNotFoundException();
                }

                context.Accounts.Remove(accountForRemove);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the account information in the repository.
        /// </summary>
        /// <param name="account">Account to update.</param>
        /// <exception cref="StorageException">
        /// It is thrown out in case of storage problems.
        /// </exception>
        public void UpdateAccount(AccountDal account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var context = new BankAccountContext())
            {
                var accountForUpdate = FindAccount(account, context);

                if (ReferenceEquals(accountForUpdate, null))
                {
                    throw new AccountNotFoundException();
                }

                accountForUpdate.Balance = account.Balance;
                accountForUpdate.Bonus = account.Bonus;

                context.SaveChanges();
            }
        }

        private AccountEF FindAccount(AccountDal account, BankAccountContext context)
        {
            return context.Accounts
                 .Include(acc => acc.AccountType)
                 .FirstOrDefault(acc => account.Number == acc.AccountId);
        }
    }
}
