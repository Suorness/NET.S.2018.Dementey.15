namespace DAL.EF.Mappers
{
    using DAL.EF.Entities;
    using DAL.Interface.DTO;

    public static class AccountMapper
    {
        public static AccountDal ToAccountDalFromEF(this AccountEF account)
        {
            return new AccountDal
            {
                Balance = account.Balance,
                Bonus = account.Bonus,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Number = account.AccountId,
                Type = account.AccountType.Type
            };
        }

        public static AccountEF ToAccountEF(this AccountDal account)
        {
            return new AccountEF
            {
                Balance = account.Balance,
                Bonus = account.Bonus,
                FirstName = account.FirstName,
                LastName = account.LastName,
                AccountType = new AccountTypeEF
                {
                    Type = account.Type
                },
                AccountId = account.Number                                
            };
        }
    }
}
