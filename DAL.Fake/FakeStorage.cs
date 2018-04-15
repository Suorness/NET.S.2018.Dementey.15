namespace DAL.Fake
{
    using System.Collections.Generic;
    using System.Linq;
    using DAL.Interface.DTO;
    using DAL.Interface.Exceptions;
    using DAL.Interface.Interfaces;

    /// <summary>
    /// Fake data storage based on the list.
    /// </summary>
    public class FakeStorage : IAccountStorage
    {
        #region private fields
        private List<AccountDal> _accounts;
        #endregion private fields

        public FakeStorage()
        {
            _accounts = new List<AccountDal>();
        }

        /// <summary>
        /// Adds a new account to the repository.
        /// </summary>
        /// <param name="account">New account.</param>
        /// <exception cref="StorageException">
        /// It is thrown in the event of a storage error.
        /// </exception>
        public void AddAccount(AccountDal account)
        {            
            if (!_accounts.Contains(account))
            {
                _accounts.Add(account);
            }
            else
            {
                throw new StorageException("Account already exists.");
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
            return _accounts.ToArray();
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
            if (_accounts.Contains(account))
            {
                _accounts.Remove(account);
            }
            else
            {
                throw new AccountNotFoundException("Account not found.");
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
            var item = _accounts.Where(a => a.Number == account.Number).First();
            _accounts.Remove(item);
            _accounts.Add(account);
        }
    }
}
