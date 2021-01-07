using System;
using System.Threading;

namespace Fabrik.SimpleBus
{
    internal sealed class SendMessageRequest
    {
        public SendMessageRequest(object payload, CancellationToken cancellationToken)
            : this(payload, cancellationToken, success => { })
        {
        }

        public SendMessageRequest(object payload, CancellationToken cancellationToken, Action<bool> onSendComplete)
        {
            if (payload == null) throw new ArgumentNullException(nameof(payload));
            if (onSendComplete == null) throw new ArgumentNullException(nameof(onSendComplete));

            Payload = payload;
            CancellationToken = cancellationToken;
            OnSendComplete = onSendComplete;
        }

        public object Payload { get; }
        public CancellationToken CancellationToken { get; }
        public Action<bool> OnSendComplete { get; }
    }
}