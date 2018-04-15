namespace BLL.Interface.Entities
{
    using System;

    /// <summary>
    /// Abstraction describing a bank account.
    /// </summary>
    public abstract class BankAccount : IEquatable<BankAccount>, IComparable, IComparable<BankAccount>
    {
        #region private fields
        private string _number;
        private string _lastName;
        private string _firstName;
        #endregion private fields

        protected BankAccount(string number, string lastName, string firstName, decimal balance, int bonus)
        {
            Number = number;
            LastName = lastName;
            FirstName = firstName;
            Balance = balance;
            Bonus = bonus;
        }

        #region property

        /// <summary>
        /// Bank account number.
        /// </summary>
        public string Number
        {
            get
            {
                return _number;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(value)}Parameter is null or an empty string or string consisting of delimiter characters. ");
                }

                _number = value;
            }
        }

        /// <summary>
        /// Last name of the owner of the bank account.
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(value)}Parameter is null or an empty string or string consisting of delimiter characters. ");
                }

                _lastName = value;
            }
        }

        /// <summary>
        /// First name of the owner of the bank account.
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(value)}Parameter is null or an empty string or string consisting of delimiter characters. ");
                }

                _firstName = value;
            }
        }

        /// <summary>
        /// Account balance.
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// Bonus points.
        /// </summary>
        public int Bonus { get; private set; }

        /// <summary>
        /// Account status.
        /// </summary>
        /// True - closed
        /// False - opened
        public bool Status { get; private set; }

        #endregion property

        #region public methods
        /// <summary>
        /// Comparison of two operands for equality.
        /// </summary>
        /// <param name="accountA">The first operand.</param>
        /// <param name="accountB">The second operand.</param>
        /// <returns>
        /// True if equivalents, false otherwise.
        /// </returns>
        public static bool operator ==(BankAccount accountA, BankAccount accountB)
        {
            if (ReferenceEquals(accountA, accountB))
            {
                return true;
            }

            if (ReferenceEquals(null, accountA))
            {
                return false;
            }

            return accountA.Equals(accountB);
        }

        /// <summary>
        /// Comparison of two operands by inequality.
        /// </summary>
        /// <param name="accountA">The first operand.</param>
        /// <param name="accountB">The second operand.</param>
        /// <returns>
        /// True if inequality, false otherwise.
        /// </returns>
        public static bool operator !=(BankAccount accountA, BankAccount accountB)
        {
            return !(accountA == accountB);
        }

        /// <summary>
        /// Replenishment of bank account.
        /// </summary>
        /// <param name="amount">The amount of replenishment of the account.</param>
        /// <exception cref="ArgumentNullException">
        /// An exception is thrown when the replenishment amount is less than zero.
        /// </exception>
        public void DepositMoney(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException($"{nameof(amount)} The value must be greater than zero.");
            }

            if (Status)
            {
                throw new InvalidOperationException("This account is closed.");
            }

            Balance = Balance + amount;
            Bonus = Bonus + ReceivingBonusOnDepositMoney(amount, Balance);
        }

        /// <summary>
        /// Withdrawal of money from a bank account.
        /// </summary>
        /// <param name="amount">An exception is thrown when the sum is less than zero.</param>
        /// <exception cref="ArgumentNullException">
        /// An exception is thrown when the replenishment amount is less than zero.
        /// </exception>
        public void WithdrawMoney(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException($"{nameof(amount)} The value must be greater than zero.");
            }

            if (Status)
            {
                throw new InvalidOperationException("This account is closed.");
            }

            Balance = Balance - amount;
            Bonus = Bonus - ReceivingBonusOnWithdrawMoney(amount, Balance);
        }

        /// <summary>
        /// Closing of the account.
        /// </summary>
        public void CloseAccount()
        {
            Balance = 0;
            Bonus = 0;
            Status = true;
        }

        /// <summary>
        /// Returns the hash code value.
        /// </summary>
        /// <returns>
        /// Hash code value.
        /// </returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of an object.
        /// </summary>
        /// <returns>
        /// String representation of an BankAccount <see cref="BankAccount"/>.
        /// </returns>
        public override string ToString()
        {
            return Number;
        }

        /// <summary>
        /// A comparison of the two BankAccount <see cref="BankAccount"/> is case-sensitive.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <exception cref="ArgumentException">
        /// <param name="obj"> is not the same type as this instance.
        /// </exception>
        /// <returns>
        /// Less than zero. This instance precedes other in the sort order. 
        /// Zero. This instance occurs in the same position in the
        /// sort order as other.
        /// Greater than zero. This instance follows other in the sort order.
        /// </returns>
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return 1;
            }

            if (ReferenceEquals(this, obj))
            {
                return 0;
            }

            if (obj.GetType() != GetType())
            {
                throw new ArgumentException($"{nameof(obj)} is not the same type as this instance.", nameof(obj));
            }

            return Compare(obj as BankAccount);
        }

        /// <summary>
        /// A comparison of the two BankAccount <see cref="BankAccount"/> is case-sensitive.
        /// </summary>
        /// <param name="other">The object to compare.</param>
        /// <returns>
        /// Less than zero. This instance precedes other in the sort order. 
        /// Zero. This instance occurs in the same position in the
        /// sort order as other.
        /// Greater than zero. This instance follows other in the sort order.
        /// </returns>
        public int CompareTo(BankAccount other)
        {
            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            return Compare(other);
        }

        /// <summary>
        /// Verifies for equality this copy of the BankAccount <see cref="BankAccount"/> with <paramref name="value"/>.
        /// </summary>
        /// <param name="value"> Value for comparison</param>
        /// <returns>
        /// True if objects are equivalent, false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return IsEqual(obj as BankAccount);
        }

        /// <summary>
        /// Verifies for equality this copy of the BankAccount <see cref="BankAccount"/> with <paramref name="value"/>.
        /// </summary>
        /// <param name="other"> Value for comparison</param>
        /// <returns>
        /// True if objects are equivalent, false otherwise.
        /// </returns>
        public bool Equals(BankAccount other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return IsEqual(other);
        }
        #endregion public methods

        #region protected methods
        protected abstract int ReceivingBonusOnDepositMoney(decimal amount, decimal balance);

        protected abstract int ReceivingBonusOnWithdrawMoney(decimal amount, decimal balance);
        #endregion protected methods

        #region private methods
        private bool IsEqual(BankAccount account)
        {
            return string.Equals(Number, account.Number, StringComparison.Ordinal);
        }

        private int Compare(BankAccount account)
        {
            return string.Compare(Number, account.Number, ignoreCase: false);
        }
        #endregion private methods
    }
}
