using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GestionPharmacie
{
    public class Client
    {
        private static readonly string connectionString = "data source = LAPTOP-G7L9QSSV;initial catalog=AppPharmacie;" +
            "integrated security=True;TrustServerCertificate=True";

        public int ClientID { get; set; }
        public string NumeroClient { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public DateTime DateInscription { get; set; }

        public static DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT ClientID, Nom, Prenom, Telephone,
                                      DateCreation AS DateInscription
                               FROM Clients;";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable Search(string text)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT ClientID, Nom, Prenom, Telephone,
                                      DateCreation AS DateInscription
                               FROM Clients
                               WHERE (Nom LIKE @txt
                                      OR Prenom LIKE @txt
                                      OR Telephone LIKE @txt)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@txt", "%" + text + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static string UpdateClient(Client client)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Clients
                               SET Nom = @Nom,
                                   Prenom = @Prenom,
                                   Telephone = @Telephone
                               WHERE ClientID = @Id";

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nom", client.Nom ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Prenom", client.Prenom ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Telephone", client.Telephone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", client.ClientID);

                cmd.ExecuteNonQuery();
                return "Client modifié avec succès.";
            }
        }

        public static string DeleteClient(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"DELETE FROM Clients WHERE ClientID = @Id";

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

                return "Client supprimé avec succès.";
            }
        }

        public static bool AddClient(string nom, string prenom, string telephone)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO Clients 
                                   (Nom, Prenom, Telephone, Actif)
                                   VALUES
                                   (@Nom, @Prenom, @Telephone, 1)";

                    using SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@Nom", nom ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Prenom", prenom ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telephone", telephone ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}


