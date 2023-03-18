using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeFinance.Infra.Queue.Abstractions
{
    public interface IMQProvider<TMessage> : IDisposable
    {
        void PublicarMensagem(TMessage body);
        Task ConsumirMensagem(Func<TMessage, bool> msgHandler, CancellationToken cancellationToken);
        void Abort();
        bool ConnectionIsOpen();
    }
}
