using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{

    public class Root
    {
        public Application Application { get; set; }
        public List<User> User { get; set; }
        public List<Account> Account { get; set; }
        public List<Transaction> Transaction { get; set; }
    }

    public class Account
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Balance { get; set; }
        public string IsActive { get; set; }
        public string Overdraft { get; set; }
    }

    public class Application
    {
        public string AllowedUsersCount { get; set; }
        public string ActualUsersCount { get; set; }
        public string DelayMS { get; set; }
    }
    
    public class Transaction
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string DateTime { get; set; }
        public string Ammount { get; set; }
        public string BalanceAfter { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string IsActive { get; set; }
    }    
}
