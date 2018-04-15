namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Describes the identifier generator.
    /// </summary>
    public interface IGeneratorNumber
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
        string GenerateNumber(string firstName, string lastName, int count);
    }
}
