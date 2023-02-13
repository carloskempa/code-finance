using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFinance.Infra.Queue
{
    public interface IRabbitMQProvider
    {
        void PublicarMensagem(string queue, string body);
        void ConsumirMensagem(string queue, Action<string> mensagemRecebida);
    }
}
