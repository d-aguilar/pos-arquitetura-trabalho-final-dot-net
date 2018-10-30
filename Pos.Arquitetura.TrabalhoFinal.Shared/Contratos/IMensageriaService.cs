using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

namespace Pos.Arquitetura.TrabalhoFinal.Shared.Contratos
{
    [ServiceContract]
    public interface IMensageriaService
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ProcessarMensagemRecebida(MsmqMessage<string> mensagem);
    }
}
