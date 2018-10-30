using System.Configuration;
using System.Data.SqlClient;

namespace Pos.Arquitetura.TrabalhoFinal.Data
{
    public class Mensagem
    {
        public void Salvar(string msg)
        {
            string connection = ConfigurationManager.AppSettings["connectionString"];
            using (SqlConnection oConn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO Table VALUES ('{msg}')", oConn))                
                    cmd.ExecuteNonQuery();                
            }                       
        }
    }
}
