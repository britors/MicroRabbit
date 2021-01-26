using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using MicroRabbit.Microservices.Banking.Application.Interfaces;
using MicroRabbit.Microservices.Banking.Application.Services;
using MicroRabbit.MicroServices.Banking.Data.Context;
using MicroRabbit.MicroServices.Banking.Data.Repository;
using MicroRabbit.MicroServices.Banking.Domain.Commands;
using MicroRabbit.MicroServices.Banking.Domain.Handles.Commands;
using MicroRabbit.MicroServices.Banking.Domain.Interfaces;
using MicroRabbit.MicroServices.Transfer.Data.Context;
using Microsoft.Extensions.DependencyInjection;


namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region core
            // Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>();
            #endregion

            #region Microservices.Banking
            //Application Services
            services.AddTransient<IAccountServices, AccountServices>();
            //Dommain
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();
            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
            #endregion

            #region Microservices.Banking
            services.AddTransient<TransferDbContext>();
            #endregion

        }
    }
}
