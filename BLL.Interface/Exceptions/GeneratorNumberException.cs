namespace BLL.Interface.Exceptions
{
    using System;

    /// <summary>
    /// The exception is thrown when the problem in the service.
    /// </summary>
    public class GeneratorNumberException : Exception
    {
        public GeneratorNumberException()
        {
        }

        public GeneratorNumberException(string message) : base(message)
        {
        }

        public GeneratorNumberException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
