using System;
using System.Threading;
using Fabrik.SimpleBus.Common;

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
            Ensure.Argument.NotNull(payload, "payload");
            Ensure.Argument.NotNull(cancellationToken, "cancellationToken");
            Ensure.Argument.NotNull(onSendComplete, "onSendComplete");

            Payload = payload;
            CancellationToken = cancellationToken;
            OnSendComplete = onSendComplete;
        }

        public object Payload { get; }
        public CancellationToken CancellationToken { get; }
        public Action<bool> OnSendComplete { get; }
    }
}