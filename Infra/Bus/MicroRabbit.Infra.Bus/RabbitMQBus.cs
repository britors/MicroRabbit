using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Infra.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handles;
        private readonly List<Type> _eventTypes;

        public RabbitMQBus(IMediator mediator)
        {
            _mediator = mediator;
            _handles = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using var chanel = connection.CreateModel();
            var eventName = @event.GetType().Name;
            chanel.QueueDeclare(eventName, false, false, false, null);
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            chanel.BasicPublish("", eventName, null, body);
        }


        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandle
        {
            var eventName = typeof(T).Name;
            var handleType = typeof(TH);
            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));

            if (!_handles.ContainsKey(eventName))
                _handles.Add(eventName, new List<Type>());

            if (_handles[eventName].Any(s => s.GetType() == handleType))
                throw new ArgumentException($"Handle Type {handleType.Name} already is registered for '{eventName}'", nameof(handleType));

            _handles[eventName].Add(handleType);

            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            using var chanel = connection.CreateModel();
            var eventName = typeof(T).Name;
            chanel.QueueDeclare(eventName, false, false, false, null);
            var consumer = new AsyncEventingBasicConsumer(chanel);
            consumer.Received += Consumer_Reveived;

            chanel.BasicConsume(eventName, true, consumer);

        }

        private async Task Consumer_Reveived(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handles.ContainsKey(eventName))
            {
                var subscriptions = _handles[eventName];

                foreach (var subscription in subscriptions)
                {
                    var handler = Activator.CreateInstance(subscription);
                    if (handler == null) continue;
                    var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IEventHandle<>).MakeGenericType(eventType);
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}
