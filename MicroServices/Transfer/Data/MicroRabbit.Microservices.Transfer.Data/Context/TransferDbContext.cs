using Microsoft.EntityFrameworkCore;


namespace MicroRabbit.MicroServices.Transfer.Data.Context
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions options) : base(options) { }

    }
}
