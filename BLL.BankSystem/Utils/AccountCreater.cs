namespace BLL.BankSystem.Utils
{
    using BLL.Interface.Entities;
    using BLL.Interface.Exceptions;
    using BLL.Interface.Interfaces;

    /// <summary>
    /// An auxiliary entity for creating objects.
    /// </summary>
    public static class AccountCreater
    {
        /// <summary>
        /// Creates an instance of the object type <paramref name="type"/>
        /// </summary>
        /// <param name="type">Object type <see cref="AccountType"/></param>
        /// <param name="number">Account number.</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="firstName">First Name</param>
        /// <param name="balance">Balance</param>
        /// <param name="bonus">Bonus</param>
        /// <exception cref="AccountCreatorException">
        /// It is thrown in the case of an unknown object type.
        /// </exception>
        /// <returns>
        /// A new type entity <see cref="BankAccount"/>
        /// </returns>
        public static BankAccount CreateAccount(int type, string number, string lastName, string firstName, decimal balance, int bonus)
        {
            if (number == null)
            {
                throw new System.ArgumentNullException(nameof(number));
            }

            if (lastName == null)
            {
                throw new System.ArgumentNullException(nameof(lastName));
            }

            if (firstName == null)
            {
                throw new System.ArgumentNullException(nameof(firstName));
            }

            BankAccount account;

            switch ((AccountType)type)
            {
                case AccountType.BaseAccount:
                    account = new BaseAccount(number, lastName, firstName, balance, bonus);
                    break;
                case AccountType.GoldAccount:
                    account = new GoldAccount(number, lastName, firstName, balance, bonus);
                    break;
                case AccountType.PlatinumAccount:
                    account = new PlatinumAccount(number, lastName, firstName, balance, bonus);
                    break;
                default:
                    throw new AccountCreatorException($"{nameof(type)}Unknown account type.");
            }

            return account;
        }

        /// <summary>
        /// Creates an instance of the object type <paramref name="type"/>
        /// </summary>
        /// <param name="type">Object type <see cref="AccountType"/></param>
        /// <param name="number">Account number.</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="firstName">First Name</param>
        /// <param name="balance">Balance</param>
        /// <param name="bonus">Bonus</param>
        /// <exception cref="AccountCreatorException">
        /// It is thrown in the case of an unknown object type.
        /// </exception>
        /// <returns>
        /// A new type entity <see cref="BankAccount"/>
        /// </returns>
        public static BankAccount CreateAccount(AccountType type, string number, string lastName, string firstName, decimal balance, int bonus)
        {
            return CreateAccount((int)type, number, lastName, firstName, balance, bonus);
        }
    }
}
