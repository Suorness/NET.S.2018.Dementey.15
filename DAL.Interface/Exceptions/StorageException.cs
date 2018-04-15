namespace DAL.Interface.Exceptions
{
    using System;

    /// <summary>
    /// The exception class is thrown when there is a problem with the storage.
    /// </summary>
    public class StorageException : Exception
    {
        public StorageException()
        {
        }

        public StorageException(string message) : base(message)
        {
        }

        public StorageException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
