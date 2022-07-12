namespace BAL.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int CurrentAccountId { get; set; }

        public int IsActive { get; set; }
    }
}
