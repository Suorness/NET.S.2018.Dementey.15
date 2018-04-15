namespace BLL.BankSystem.ServicesImplementation
{
    using BLL.Interface.Interfaces;

    /// <summary>
    /// Describes the identifier generator.
    /// </summary>
    public class GeneratorAccountNumber : IGeneratorNumber
    {
        /// <summary>
        /// Generates account numbers.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="count"></param>
        /// <exception cref="GeneratorNumberException">
        /// The exception is thrown when the problem in the service.
        /// </exception>
        /// <returns>
        /// The generated number.
        /// </returns>
        public string GenerateNumber(string firstName, string lastName, int count)
        {
            return (count + 1).ToString();
        }
    }
}
