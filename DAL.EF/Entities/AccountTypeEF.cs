namespace DAL.EF.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class AccountTypeEF
    {
        [Key]
        public int Id { get; set; }

        public int Type { get; set; }
    }
}
