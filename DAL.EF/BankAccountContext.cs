namespace DAL.EF
{
    using System.Data.Entity;
    using DAL.EF.Entities;

    public class BankAccountContext : DbContext
    {
        public BankAccountContext() : base("Accounts")
        {
            Database.SetInitializer<BankAccountContext>(new DropCreateDatabaseAlways<BankAccountContext>());
        }

        public DbSet<AccountEF> Accounts { get; set; }

        public DbSet<AccountTypeEF> AccountTypes { get; set; }
    }
}
