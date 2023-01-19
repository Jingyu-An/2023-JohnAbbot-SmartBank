using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Day07AzureDb
{
    public partial class SmartBankingDbContext : DbContext
    {
        public SmartBankingDbContext()
            : base("name=SmartBankingDbContext")
        {
        }

        public DbSet<Users> UserEmployees { get; set; }


        //public DbSet<Operation> Operations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
