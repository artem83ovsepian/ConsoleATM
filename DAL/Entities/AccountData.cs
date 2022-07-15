namespace DAL.Entities
{
    public class AccountData
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Balance { get; set; }

        public int IsActive { get; set; }

        public decimal OverDraft { get; set; }
    }
}
