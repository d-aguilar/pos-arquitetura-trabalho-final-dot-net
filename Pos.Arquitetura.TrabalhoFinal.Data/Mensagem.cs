using System.Configuration;
using System.Data.SqlClient;

namespace Pos.Arquitetura.TrabalhoFinal.Data
{
    public class MensagemData
    {
        public void Salvar(string msg)
        {
            string connection = ConfigurationManager.ConnectionStrings["TrabalhoFinalDB"].ConnectionString;
            using (SqlConnection oConn = new SqlConnection(connection))
            {
                oConn.Open();
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO Mensagens VALUES ('{msg}')", oConn))                
                    cmd.ExecuteNonQuery();                
            }                       
        }
    }
}
