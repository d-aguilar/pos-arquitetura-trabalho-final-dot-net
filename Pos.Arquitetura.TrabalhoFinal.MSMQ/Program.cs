using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Arquitetura.TrabalhoFinal.MSMQ
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //SELF-HOST (repare que não estou utilizando o IIS)
            using (var host = new ServiceHost(typeof(MensageriaService)))
            {
                host.Faulted += Faulted;
                host.Open();

                Console.WriteLine("Serviço iniciado ...");

                //Se apertar qualquer tecla vai sair do console
                Console.ReadLine();

                if (host != null)
                {
                    if (host.State == CommunicationState.Faulted)
                    {
                        host.Abort();
                    }
                    host.Close();
                }
            }
        }

        static void Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("Falha no WCF");
        }
    }
}
