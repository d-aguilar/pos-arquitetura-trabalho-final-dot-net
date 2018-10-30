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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EnvioDeMensagemService : IEnvioDeMensagemService
    {
        public void EnviarMensagemParaFila(string mensagem)
        {
            var caminhoFila = ConfigurationManager.AppSettings["MessageQueuePath"];

            using (var queue = new MessageQueue(caminhoFila))
            {
                var msg = new System.Messaging.Message { Body = mensagem };

                using (var ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    queue.Send(msg, MessageQueueTransactionType.Automatic);
                    ts.Complete();
                }
            }
        }
    }
}
