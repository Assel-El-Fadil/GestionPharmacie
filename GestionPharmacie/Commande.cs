using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GestionPharmacie
{
    public class Commande
    {
        private static readonly string connectionString =
            "data source = DESKTOP-DDPB0HH\\GI2_1;initial catalog=GestionPharmacie;" +
            "integrated security=True;TrustServerCertificate=True";

        public int CommandeID { get; set; }
        public string NumeroCommande { get; set; }
        public DateTime DateCommande { get; set; }
        public string Client { get; set; }
        public decimal Montant { get; set; }
        public string Statut { get; set; }
        public string Paiement { get; set; }

        public static DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT c.CommandeID, 
                                      c.NumeroCommande,
                                      c.DateCommande AS Date,
                                      (u.Nom + ' ' + u.Prenom) AS Client,
                                      c.MontantApresRemise AS Montant,
                                      c.Statut,
                                      c.ModePaiement AS Paiement
                               FROM Commandes c
                               INNER JOIN Utilisateurs u ON c.UtilisateurClientID = u.UtilisateurID
                               ORDER BY c.DateCommande DESC";

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
                string sql = @"SELECT c.CommandeID, 
                                      c.NumeroCommande,
                                      c.DateCommande AS Date,
                                      (u.Nom + ' ' + u.Prenom) AS Client,
                                      c.MontantApresRemise AS Montant,
                                      c.Statut,
                                      c.ModePaiement AS Paiement
                               FROM Commandes c
                               INNER JOIN Utilisateurs u ON c.UtilisateurClientID = u.UtilisateurID
                               WHERE c.NumeroCommande LIKE @txt
                                  OR u.Nom LIKE @txt
                                  OR u.Prenom LIKE @txt
                                  OR (u.Nom + ' ' + u.Prenom) LIKE @txt
                               ORDER BY c.DateCommande DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@txt", "%" + text + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable FilterByStatus(string statut)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT c.CommandeID, 
                                      c.NumeroCommande,
                                      c.DateCommande AS Date,
                                      (u.Nom + ' ' + u.Prenom) AS Client,
                                      c.MontantApresRemise AS Montant,
                                      c.Statut,
                                      c.ModePaiement AS Paiement
                               FROM Commandes c
                               INNER JOIN Utilisateurs u ON c.UtilisateurClientID = u.UtilisateurID
                               WHERE c.Statut = @Statut
                               ORDER BY c.DateCommande DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Statut", statut);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetCommandeDetails(int commandeID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT c.CommandeID, 
                                      c.NumeroCommande,
                                      c.DateCommande,
                                      (u.Nom + ' ' + u.Prenom) AS Client,
                                      c.MontantApresRemise AS MontantTotal,
                                      c.Statut,
                                      c.ModePaiement AS Paiement
                               FROM Commandes c
                               INNER JOIN Utilisateurs u ON c.UtilisateurClientID = u.UtilisateurID
                               WHERE c.CommandeID = @CommandeID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CommandeID", commandeID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetCommandeItems(int commandeID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT m.NomMedicament AS Medicament,
                                      dc.Quantite,
                                      dc.PrixUnitaire,
                                      dc.SousTotal
                               FROM DetailsCommande dc
                               INNER JOIN Medicaments m ON dc.MedicamentID = m.MedicamentID
                               WHERE dc.CommandeID = @CommandeID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CommandeID", commandeID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}

