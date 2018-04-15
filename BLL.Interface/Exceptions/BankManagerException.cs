namespace BLL.Interface.Exceptions
{
    using System;

    /// <summary>
    /// The exception class is thrown when there is a problem with the manager service.
    /// </summary>
    public class BankManagerException : Exception
    {
        public BankManagerException()
        {
        }

        public BankManagerException(string message) : base(message)
        {
        }

        public BankManagerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}