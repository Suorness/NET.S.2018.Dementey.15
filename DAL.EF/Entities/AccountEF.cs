namespace DAL.EF.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AccountEF
    {
        [Key]
        public string AccountId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public decimal Balance { get; set; }

        public int Bonus { get; set; }
        
        public int AccountTypeId { get; set; }

        [ForeignKey("AccountTypeId")]
        public virtual AccountTypeEF AccountType { get; set; }
    }
}
