using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GestionPharmacie
{
    public class Client
    {
        private static readonly string connectionString =
            "data source = DESKTOP-DDPB0HH\\GI2_1;initial catalog=GestionPharmacie;" +
            "integrated security=True;TrustServerCertificate=True";

        public int UtilisateurID { get; set; }
        public string NumeroClient { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime DateInscription { get; set; }

        public static DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT UtilisateurID, NumeroClient, Nom, Prenom, Telephone, Email,
                                      DateCreation AS DateInscription
                               FROM Utilisateurs
                               WHERE Role = 'Client'";

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
                string sql = @"SELECT UtilisateurID, NumeroClient, Nom, Prenom, Telephone, Email,
                                      DateCreation AS DateInscription
                               FROM Utilisateurs
                               WHERE Role = 'Client' AND
                                     (NumeroClient LIKE @txt
                                      OR Nom LIKE @txt
                                      OR Prenom LIKE @txt
                                      OR Telephone LIKE @txt
                                      OR Email LIKE @txt)";

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
                string sql = @"UPDATE Utilisateurs
                               SET NumeroClient = @NumeroClient,
                                   Nom = @Nom,
                                   Prenom = @Prenom,
                                   Telephone = @Telephone,
                                   Email = @Email
                               WHERE UtilisateurID = @Id";

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NumeroClient", client.NumeroClient ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Nom", client.Nom ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Prenom", client.Prenom ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Telephone", client.Telephone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", client.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", client.UtilisateurID);

                cmd.ExecuteNonQuery();
                return "Client modifié avec succès.";
            }
        }

        public static string DeleteClient(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"DELETE FROM Utilisateurs WHERE UtilisateurID = @Id";

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

                return "Client supprimé avec succès.";
            }
        }

        public static bool AddClient(string numeroClient, string nom, string prenom, string cin,
                                     string telephone, string email, DateTime dateNaissance, string ville)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO Utilisateurs 
                                   (NumeroClient, NomUtilisateur, MotDePasse, Nom, Prenom, CIN, Telephone, Email, DateNaissance, Ville, Role, Actif, DateCreation, Adresse, PhotoPath, DerniereConnexion)
                                   VALUES
                                   (@NumeroClient, @NomUtilisateur, @MotDePasse, @Nom, @Prenom, @CIN, @Telephone, @Email, @DateNaissance, @Ville, 'Client', 1, GETDATE(), @Adresse, @PhotoPath, NULL)";

                    using SqlCommand cmd = new SqlCommand(sql, conn);
                    string username = string.IsNullOrWhiteSpace(email) ? numeroClient : email;
                    string defaultPassword = "Client123!"; // TODO: allow admin to set or reset later

                    cmd.Parameters.AddWithValue("@NumeroClient", numeroClient ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NomUtilisateur", username ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MotDePasse", defaultPassword);
                    cmd.Parameters.AddWithValue("@Nom", nom ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Prenom", prenom ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CIN", cin ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telephone", telephone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DateNaissance", dateNaissance);
                    cmd.Parameters.AddWithValue("@Ville", ville ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Adresse", DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhotoPath", DBNull.Value);

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


