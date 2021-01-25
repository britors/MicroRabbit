using MicroRabbit.Domain.Core.Events;
using System.Threading.Tasks;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventHandle<in TEvent> : IEventHandle
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandle
    {

    }
}
