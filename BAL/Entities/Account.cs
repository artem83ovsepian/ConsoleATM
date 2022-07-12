namespace BAL.Entities
{
    public class Account
    {
        public int Id { set; get; }

        public int UserId { set; get; }

        public decimal Balance { set; get; }

        public int IsActive { set; get; }

        public decimal OverDraft { set; get; }
    }
}