using MicroRabbit.MicroServices.Banking.Domain.Models;
using System.Collections.Generic;

namespace MicroRabbit.MicroServices.Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
