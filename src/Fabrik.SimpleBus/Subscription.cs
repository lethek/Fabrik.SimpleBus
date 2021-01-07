using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fabrik.SimpleBus
{
    internal sealed class Subscription
    {
        private Subscription(Func<object, CancellationToken, Task> handler)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
            Id = Guid.NewGuid();
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