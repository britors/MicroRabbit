using MicroRabbit.MicroServices.Banking.Data.Context;
using MicroRabbit.MicroServices.Banking.Domain.Interfaces;
using MicroRabbit.MicroServices.Banking.Domain.Models;
using System.Collections.Generic;

namespace MicroRabbit.MicroServices.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _ctx;

        public AccountRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _ctx.Accounts;    
        }
    }
}
