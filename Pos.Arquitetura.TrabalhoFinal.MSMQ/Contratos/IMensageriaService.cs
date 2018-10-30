using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Arquitetura.TrabalhoFinal.MSMQ.Contratos
{
    [ServiceContract]
    public interface IMensageriaService
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ProcessarMensagemRecebida(MsmqMessage<string> mensagem);
    }
}
