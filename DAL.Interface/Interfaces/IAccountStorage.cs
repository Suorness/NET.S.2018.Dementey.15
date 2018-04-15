namespace DAL.Interface.Interfaces
{
    using System.Collections.Generic;
    using DAL.Interface.DTO;

    /// <summary>
    /// The interface describing the interaction with the repository.
    /// </summary>
    public interface IAccountStorage
    {
        /// <summary>
        /// Adds a new account to the repository.
        /// </summary>
        /// <param name="account">New account.</param>
        /// <exception cref="StorageException">
        /// It is thrown in the event of a storage error.
        /// </exception>
        void AddAccount(AccountDal account);

        /// <summary>
        /// A method that removes a account <paramref name="account"/> from the repository.
        /// </summary>
        /// <param name="account">
        /// The Account for removal.
        /// </param>
        /// <exception cref="StorageException">
        /// It is thrown out in case of storage problems.
        /// </exception>
        void RemoveAccount(AccountDal account);

        /// <summary>
        /// Returns all BankAccount <see cref="AccountDal"/> from the repository.
        /// </summary>
        /// <returns>
        /// BankAccount  <see cref="BankAccount"/> from the repository.
        /// </returns>
        /// <exception cref="StorageException">
        /// It is thrown out in case of storage problems.
        /// </exception>
        IEnumerable<AccountDal> GetBankAccounts();

        /// <summary>
        /// Updates the account information in the repository.
        /// </summary>
        /// <param name="account">Account to update.</param>
        /// <exception cref="StorageException">
        /// It is thrown out in case of storage problems.
        /// </exception>
        void UpdateAccount(AccountDal account);
    }
}
