using MicroRabbit.MicroServices.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace MicroRabbit.MicroServices.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
    }
}
