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
        private readonly MensagemData _mensagem;

        public Service()
        {
            _mensagem = new MensagemData();
        }

        public void DoWork()
        {
            var t = new Thread(x =>
            {
                while (GetMessages)
                {
                    var caminhoFila = ConfigurationManager.AppSettings["MessageQueuePath"];

                    using (var queue = new MessageQueue(caminhoFila))
                    {
                        if (queue.CanRead)
                            using (var ts = new TransactionScope(TransactionScopeOption.Required))
                            {
                                System.Type[] tipos = new System.Type[2];
                                Object o = new object();
                                tipos[0] = typeof(string);
                                tipos[1] = o.GetType();
                                queue.Formatter = new XmlMessageFormatter(tipos);
                                var mensagem = queue.Receive()?.Body?.ToString();

                                if (!string.IsNullOrWhiteSpace(mensagem))
                                {
                                    _mensagem.Salvar(mensagem);
                                }

                                ts.Complete();
                            }
                    }

                    Thread.Sleep(2000);
                }
            });

            t.Start();
        }

        public void Stop()
        {
            GetMessages = false;
        }

        public IEnumerable<string> ListarMensagens()
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                return _mensagem.ListarMensagens();
            }
        }

    }
}
