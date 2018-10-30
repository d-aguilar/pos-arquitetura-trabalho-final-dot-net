using Pos.Arquitetura.TrabalhoFinal.Shared;
using System;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

namespace Pos.Arquitetura.TrabalhoFinal.MSMQ
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, ReleaseServiceInstanceOnTransactionComplete = false)]
    public class MensageriaService : IMensageriaService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void ProcessarMensagemRecebida(MsmqMessage<string> mensagemRecebida)
        {
            var corpoDaMensagem = mensagemRecebida.Body;

            Console.WriteLine($"Mensagem: {corpoDaMensagem}");
            Console.WriteLine();
        }
    }
}
