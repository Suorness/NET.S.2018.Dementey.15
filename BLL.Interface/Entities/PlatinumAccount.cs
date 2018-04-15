namespace BLL.Interface.Entities
{
    /// <summary>
    /// Describing a Platinum bank account.
    /// </summary>
    public class PlatinumAccount : BankAccount
    {
        public PlatinumAccount(
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
            return (int)((amount * balance) + 1);
        }

        protected override int ReceivingBonusOnWithdrawMoney(decimal amount, decimal balance)
        {
            return (int)(amount * 0.1m * balance);
        }
    }
}
