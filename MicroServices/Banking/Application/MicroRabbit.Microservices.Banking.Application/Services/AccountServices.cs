using MicroRabbit.Microservices.Banking.Application.Interfaces;
using MicroRabbit.MicroServices.Banking.Domain.Interfaces;
using MicroRabbit.MicroServices.Banking.Domain.Models;
using System;
using System.Collections.Generic;

namespace MicroRabbit.Microservices.Banking.Application.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        public AccountServices(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }
    }
}
