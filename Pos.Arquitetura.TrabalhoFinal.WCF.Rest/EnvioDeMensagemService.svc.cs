using System;
using System.Collections.Generic;
using System.Configuration;
using System.Messaging;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Transactions;

namespace Pos.Arquitetura.TrabalhoFinal.WCF.Rest
{

    public class EnvioDeMensagemService : IEnvioDeMensagemService
    {

        private readonly string _ipMessageQueue = ConfigurationManager.AppSettings["IPMessageQueue"];
        private readonly string _nomeDaFila = ConfigurationManager.AppSettings["NomeDaFila"];
        public void EnviarMensagemParaFila(string mensagem)
        {
            CriaFilaSeNaoExiste();

            var caminhoDaFila = RetornaCaminhoDaFila();

            using (var queue = new MessageQueue(caminhoDaFila))
            {
                var msg = new System.Messaging.Message { Body = mensagem };

                using (var ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    queue.Send(msg, MessageQueueTransactionType.Automatic);
                    ts.Complete();
                }
            }
        }

        private void CriaFilaSeNaoExiste()
        {
            var nomeDaFila = "." + _nomeDaFila;
            if (!MessageQueue.Exists(nomeDaFila))
            {
                MessageQueue.Create(nomeDaFila, true);
            }
        }
        private string RetornaCaminhoDaFila()
        {
            return _ipMessageQueue + _nomeDaFila;
        }

    }
}
