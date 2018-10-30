using Pos.Arquitetura.TrabalhoFinal.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Messaging;
using System.Threading;
using System.Transactions;

namespace Pos.Arquitetura.TrabalhoFinal.WCF.Banco
{
    public class Service : IService
    {
        bool GetMessages = true;
        Mensagem _mensagem = new Mensagem();

        public Service()
        {
        }

        public void DoWork()
        {
            _mensagem.Salvar("teste");
            while (GetMessages)
            {
                var caminhoFila = ConfigurationManager.AppSettings["MessageQueuePath"];

                using (var queue = new MessageQueue(caminhoFila))
                {
                    if (queue.CanRead)
                        using (var ts = new TransactionScope(TransactionScopeOption.Required))
                            SaveMessages(queue.GetAllMessages());
                }

                Thread.Sleep(2000);
            }
        }

        public void Stop()
        {
            GetMessages = false;
        }

        private void SaveMessages(Message[] listMsg)
        {
            foreach (var msg in listMsg)
                _mensagem.Salvar(msg.Body.ToString());
        }
    }
}
