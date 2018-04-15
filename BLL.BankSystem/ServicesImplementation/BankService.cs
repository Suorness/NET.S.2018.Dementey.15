namespace BLL.BankSystem.ServicesImplementation
{
    using System.Collections.Generic;
    using System.Linq;
    using BLL.BankSystem.Mappers;
    using BLL.BankSystem.Utils;
    using BLL.Interface.Entities;
    using BLL.Interface.Exceptions;
    using BLL.Interface.Interfaces;
    using DAL.Interface.Exceptions;
    using DAL.Interface.Interfaces;

    /// <summary>
    /// Describes the ability to manage accounts.
    /// </summary>
    public class BankService : IBankManager
    {
        #region private fields
        private readonly IAccountStorage _accountStorage;
        private readonly IGeneratorNumber _numberGenerator;

        private List<BankAccount> _accounts = new List<BankAccount>();
        #endregion private fields

        /// <summary>
        /// Initializing an Instance
        /// </summary>
        /// <param name="accountStorage">Data store</param>
        public BankService(IAccountStorage accountStorage, IGeneratorNumber numberGenerator)
        {
            _accountStorage = accountStorage ?? throw new System.ArgumentNullException(nameof(accountStorage));
            _numberGenerator = numberGenerator ?? throw new System.ArgumentNullException(nameof(numberGenerator));

            try
            {
                _accounts.AddRange(_accountStorage.GetBankAccounts().Select(acc => acc.ToAccount()));
            }
            catch (StorageException e)
            {
                throw new BankManagerException("There were problems reading from the repository.", e);
            }
        }

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
        public void CloseAccount(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new System.ArgumentException(nameof(number));
            }

            BankAccount account = FindAccount(number);

            account.CloseAccount();

            SaveChanges(account);
        }

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
        public void DepositMoney(string number, decimal amount)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new System.ArgumentException(nameof(number));
            }

            BankAccount account = FindAccount(number);

            account.DepositMoney(amount);

            SaveChanges(account);
        }

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
        public void OpenAccount(string firstName, string lastName, decimal balance, AccountType type = AccountType.BaseAccount)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new System.ArgumentException(nameof(firstName));
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new System.ArgumentException(nameof(lastName));
            }

            BankAccount account = AccountCreater.CreateAccount(type, _numberGenerator.GenerateNumber(firstName, lastName, _accounts.Count), lastName, firstName, balance, 0);
            _accounts.Add(account);
            try
            {
                _accountStorage.AddAccount(account.ToAccountDal());
            }
            catch (StorageException e)
            {
                _accounts.Remove(account);
                throw new BankManagerException("An error occurred while working with the repository.", e);
            }
        }

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
        public void WithdrawMoney(string number, decimal amount)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new System.ArgumentException(nameof(number));
            }

            BankAccount account = FindAccount(number);

            account.WithdrawMoney(amount);

            SaveChanges(account);
        }

        /// <summary>
        /// Get information about accounts <see cref="BankAccount"/>
        /// </summary>
        /// <exception cref="BankManagerException">
        /// It is thrown out in case of problems in the service.
        /// </exception>
        /// <returns>
        /// Information about accounts <see cref="BankAccount"/>
        /// </returns>
        public IEnumerable<BankAccount> GetAccounts()
        {
            return _accountStorage.GetBankAccounts().Select(acc => acc.ToAccount());
        }

        private BankAccount FindAccount(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new System.ArgumentNullException(nameof(number));
            }

            BankAccount resultAccount = null;

            _accounts.Clear();
            try
            {
                _accounts.AddRange(_accountStorage.GetBankAccounts().Select(acc => acc.ToAccount()));
            }
            catch (StorageException e)
            {
                throw new BankManagerException("There were problems reading from the repository.", e);
            }

            foreach (var account in _accounts)
            {
                if (account.Number == number)
                {
                    resultAccount = account;
                    break;
                }
            }

            if (ReferenceEquals(resultAccount, null))
            {
                throw new BankManagerException("Account not found.");
            }

            return resultAccount;
        }

        private void SaveChanges(BankAccount account)
        {
            try
            {
                _accountStorage.UpdateAccount(account.ToAccountDal());
            }
            catch (StorageException e)
            {
                throw new BankManagerException("An error occurred while working with the repository.", e);
            }
        }
    }
}
