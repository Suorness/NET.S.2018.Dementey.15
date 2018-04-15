namespace DAL.Interface.Exceptions
{
    using System;

    /// <summary>
    /// It is thrown out if the account is not found.
    /// </summary>
    public class AccountNotFoundException : StorageException
    {
        public AccountNotFoundException()
        {
        }

        public AccountNotFoundException(string message) : base(message)
        {
        }

        public AccountNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
