namespace BLL.Interface.Exceptions
{
    using System;

    /// <summary>
    /// The exception class is thrown when there is a problem with the factory service.
    /// </summary>
    public class AccountCreatorException : BankManagerException
    {
        public AccountCreatorException()
        {
        }

        public AccountCreatorException(string message) : base(message)
        {
        }

        public AccountCreatorException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}