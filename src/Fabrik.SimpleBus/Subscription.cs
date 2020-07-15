using System;
using System.Threading;
using System.Threading.Tasks;
using Fabrik.SimpleBus.Common;

namespace Fabrik.SimpleBus
{
    internal sealed class Subscription
    {
        private Subscription(Func<object, CancellationToken, Task> handler)
        {
            Ensure.Argument.NotNull(handler, "handler");
            Id = Guid.NewGuid();
            Handler = handler;
        }

        public Guid Id { get; }
        public Func<object, CancellationToken, Task> Handler { get; }

        public static Subscription Create<TMessage>(Func<TMessage, CancellationToken, Task> handler)
        {
            async Task HandlerWithCheck(object message, CancellationToken cancellationToken)
            {
                if (message is TMessage typedMessage)
                {
                    await handler.Invoke(typedMessage, cancellationToken);
                }
            }

            return new Subscription(HandlerWithCheck);
        }
    }
}