namespace BLL.Interface.Interfaces
{
    using System.Collections.Generic;
    using BLL.Interface.Entities;

    /// <summary>
    /// An interface that describes the ability to manage accounts.
    /// </summary>
    public interface IBankManager
    {
        /// <summary>
        /// Opening an account.
        /// </summary>
        /// <param name="type">Type of account.</param>
        /// <param name="firstName">First name of the owner.</param>
        /// <param name="lastName">Last name of the owner.</param>
        /// <param name="balance">Starting balance.</param>
        /// <exception cref="BankManagerException">
        /// It is thrown out in case of problems in the service.
        /// </exception>
        /// <returns>
        /// If successful, it is true, otherwise false.
        /// </returns>
        void OpenAccount(string firstName, string lastName, decimal balance, AccountType type = AccountType.BaseAccount);

        /// <summary>
        /// Closing of the account.
        /// </summary>
        /// <param name="number"> Account number to close.</param>
        /// <exception cref="BankManagerException">
        /// It is thrown out in case of problems in the service.
        /// </exception>
        /// <returns>
        /// If successful, it is true, otherwise false.
        /// </returns>
        void CloseAccount(string number);

        /// <summary>
        /// Crediting of funds to the account.
        /// </summary>
        /// <param name="number">Account number for crediting.</param>
        /// <param name="amount">Amount to be credited.</param>
        /// <returns>
        /// <exception cref="BankManagerException">
        /// It is thrown out in case of problems in the service.
        /// </exception>
        /// If successful, it is true, otherwise false.
        /// </returns>
        void DepositMoney(string number, decimal amount);

        /// <summary>
        /// Withdrawing money from the account.
        /// </summary>
        /// <param name="number">Account number.</param>
        /// <param name="amount">Amount.</param>
        /// <exception cref="BankManagerException">
        /// It is thrown out in case of problems in the service.
        /// </exception>
        /// <returns>
        /// If successful, it is true, otherwise false.
        /// </returns>
        void WithdrawMoney(string number, decimal amount);

        /// <summary>
        /// Get information about accounts <see cref="BankAccount"/>
        /// </summary>
        /// <exception cref="BankManagerException">
        /// It is thrown out in case of problems in the service.
        /// </exception>
        /// <returns>
        /// Information about accounts <see cref="BankAccount"/>
        /// </returns>
        IEnumerable<BankAccount> GetAccounts();
    }
}
