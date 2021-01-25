using MicroRabbit.Microservices.Banking.Application.Interfaces;
using MicroRabbit.MicroServices.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MicroRabbit.Microservices.Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        public BankingController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        
        // Get api/banking
        [HttpGet()]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return Ok(_accountServices.GetAccounts());
        }
    }
}
