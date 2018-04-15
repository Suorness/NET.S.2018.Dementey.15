namespace BLL.BankSystem.Mappers
{
    using BLL.BankSystem.Utils;
    using BLL.Interface.Entities;
    using BLL.Interface.Interfaces;
    using DAL.Interface.DTO;

    /// <summary>
    /// The mapping class converts data from Dal to service
    /// </summary>
    internal static class AccountMapper
    {
        /// <summary>
        /// Converts data from the storage to service data
        /// </summary>
        /// <param name="accountDal"> Data from Dal</param>
        /// <returns>
        /// Data for the service.
        /// </returns>
        public static BankAccount ToAccount(this AccountDal accountDal)
        {
            return AccountCreater.CreateAccount(accountDal.Type, accountDal.Number, accountDal.LastName, accountDal.FirstName, accountDal.Balance, accountDal.Bonus);
        }

        /// <summary>
        /// Converts data from the service to storage data
        /// </summary>
        /// <param name="account"> Data for Dal</param>
        /// <returns></returns>
        public static AccountDal ToAccountDal(this BankAccount account)
        {
            var accountType = account.GetType();
            AccountType type = AccountType.BaseAccount;

            if (accountType == typeof(BaseAccount))
            {
                type = AccountType.BaseAccount;
            }

            if (accountType == typeof(GoldAccount))
            {
                type = AccountType.GoldAccount;
            }

            if (accountType == typeof(PlatinumAccount))
            {
                type = AccountType.PlatinumAccount;
            }

            return new AccountDal()
            {
                Type = (int)type,
                Balance = account.Balance,
                Bonus = account.Bonus,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Number = account.Number
            };
        }
    }
}
