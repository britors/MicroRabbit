using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.MicroServices.Banking.Domain.Commands;
using MicroRabbit.MicroServices.Banking.Domain.Handles.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MicroRabbit.MicroServices.Banking.Domain.Handles.Commands
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private IEventBus _bus;

        public TransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }


        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMQ
            _bus.Publish(new TransferCreatedEvent(request.From, request.To, request.Ammount));
            return Task.FromResult(true);
        }
    }
}
