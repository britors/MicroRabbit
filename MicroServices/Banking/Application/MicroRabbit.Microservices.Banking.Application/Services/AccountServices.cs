using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Microservices.Banking.Application.Interfaces;
using MicroRabbit.Microservices.Banking.Application.Models;
using MicroRabbit.MicroServices.Banking.Domain.Commands;
using MicroRabbit.MicroServices.Banking.Domain.Interfaces;
using MicroRabbit.MicroServices.Banking.Domain.Models;
using System;
using System.Collections.Generic;

namespace MicroRabbit.Microservices.Banking.Application.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _bus;

        public AccountServices(IAccountRepository accountRepository, IEventBus bus)
        {
            _accountRepository = accountRepository;
            _bus = bus;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransformCommand = new CreateTransferCommand(
                accountTransfer.FromAccount,
                accountTransfer.ToAccount,
                accountTransfer.Ammount
                );

            _bus.SendCommand(createTransformCommand);
        }
    }
}
