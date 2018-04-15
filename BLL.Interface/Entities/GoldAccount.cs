namespace BLL.Interface.Entities
{
    /// <summary>
    /// Describing a Gold bank account.
    /// </summary>
    public class GoldAccount : BankAccount
    {
        public GoldAccount(
            string number,
            string lastName,
            string firstName,
            decimal balance,
            int bonus) : base(
                number,
                lastName,
                firstName,
                balance,
                bonus)
        {
        }

        protected override int ReceivingBonusOnDepositMoney(decimal amount, decimal balance)
        {
            return (int)((amount * 2 * balance) + 1);
        }

        protected override int ReceivingBonusOnWithdrawMoney(decimal amount, decimal balance)
        {
            return (int)(amount * 0.3m * balance);
        }
    }
}
