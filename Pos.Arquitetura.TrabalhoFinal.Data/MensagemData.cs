using System.Collections.Generic;
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

        public IEnumerable<string> ListarMensagens()
        {
            string connection = ConfigurationManager.ConnectionStrings["TrabalhoFinalDB"].ConnectionString;
            using (SqlConnection oConn = new SqlConnection(connection))
            {
                oConn.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Mensagens", oConn))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return dr[0].ToString();
                    }
                }
            }
        }
    }
}
