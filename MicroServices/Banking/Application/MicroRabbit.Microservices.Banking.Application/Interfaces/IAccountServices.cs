using MicroRabbit.MicroServices.Banking.Domain.Models;
using System.Collections.Generic;

namespace MicroRabbit.Microservices.Banking.Application.Interfaces
{
    public interface IAccountServices
    {
        IEnumerable<Account> GetAccounts();
    }
}
