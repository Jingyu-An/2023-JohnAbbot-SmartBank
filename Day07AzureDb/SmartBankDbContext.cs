using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Day07AzureDb
{
    public partial class SmartBankDbContext : DbContext
    {
        public SmartBankDbContext()
            : base("name=SmartBankDbContext")
        {
        }

        public DbSet<Users> UserEmployees { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
