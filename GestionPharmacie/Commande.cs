using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace GestionPharmacie
{
    public class Commande
    {
        private static readonly string connectionString =
            "data source = DESKTOP-DDPB0HH\\GI2_1;initial catalog=AppPharmacie;" +
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
                                      c.MontantApresRemise AS Montant
                               FROM Commandes c
                               INNER JOIN Clients u ON c.ClientID = u.ClientID
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
                                      c.MontantApresRemise AS Montant
                               FROM Commandes c
                               INNER JOIN Clients u ON c.ClientID = u.ClientID
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

        public static DataTable GetCommandeDetails(int commandeID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT c.CommandeID, 
                                      c.NumeroCommande,
                                      c.DateCommande,
                                      (u.Nom + ' ' + u.Prenom) AS Client,
                                      c.MontantApresRemise AS MontantTotal
                               FROM Commandes c
                               INNER JOIN Clients u ON c.ClientID = u.ClientID
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

        public static int insertCommande(List<DetailCommande> detailsCommande, int clientId, int pharmacienId, decimal montantTotal, 
            decimal remise, decimal montantApresRemise, String remarques)
        {
            string numeroCommande = "CMD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string numerofacture = "CMD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();

                try
                {
                    // 1 ─ Insérer la commande
                    string sqlCmd = @"INSERT INTO Commandes (NumeroCommande, ClientID, PharmacienID, DateCommande, MontantTotal, Remise, MontantApresRemise, Remarques, Validée)
                        VALUES (@NumeroCommande, @ClientID, @pharmacienId, GETDATE(), @Total, @Remise, @TotalFinal, @Remarques, 1);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd1 = new SqlCommand(sqlCmd, conn, tr);
                    cmd1.Parameters.AddWithValue("@NumeroCommande", numeroCommande);
                    cmd1.Parameters.AddWithValue("@ClientID", clientId);
                    cmd1.Parameters.AddWithValue("@pharmacienId", pharmacienId);
                    cmd1.Parameters.AddWithValue("@Total", montantTotal);
                    cmd1.Parameters.AddWithValue("@Remise", remise);
                    cmd1.Parameters.AddWithValue("@TotalFinal", montantApresRemise);
                    cmd1.Parameters.AddWithValue("@Remarques", remarques);

                    int commandeId = Convert.ToInt32(cmd1.ExecuteScalar());

                    // 2 ─ Insérer les détails
                    foreach (var detail in detailsCommande)
                    {
                        string sqlDetail = @"INSERT INTO DetailsCommande (CommandeID, MedicamentID, Quantite, PrixUnitaire, SousTotal)
                            VALUES (@CMD, @MedID, @Qte, @PrixU, @SousTotal);";

                        SqlCommand cmd2 = new SqlCommand(sqlDetail, conn, tr);
                        cmd2.Parameters.AddWithValue("@CMD", commandeId);
                        cmd2.Parameters.AddWithValue("@MedID", detail.MedicamentID);
                        cmd2.Parameters.AddWithValue("@Qte", detail.Quantite);
                        cmd2.Parameters.AddWithValue("@PrixU", detail.PrixUnitaire);
                        cmd2.Parameters.AddWithValue("@SousTotal", detail.SousTotal);

                        cmd2.ExecuteNonQuery();

                        // Mettre à jour le stock
                        string sqlStock = @"UPDATE Medicaments SET QuantiteStock = QuantiteStock - @Qte 
                            WHERE MedicamentID = @MedID";

                        SqlCommand cmdStock = new SqlCommand(sqlStock, conn, tr);
                        cmdStock.Parameters.AddWithValue("@MedID", detail.MedicamentID);
                        cmdStock.Parameters.AddWithValue("@Qte", detail.Quantite);
                        cmdStock.ExecuteNonQuery();
                    }

                    // 3 ─ Créer la facture
                    string sqlFacture = @"INSERT INTO Factures (NumeroFacture, CommandeID, ClientID, DateFacture, MontantTotal, StatutPaiement)
                            VALUES (@NUM, @CMD, @CLT, GETDATE(), @Total, 'payé');";

                    SqlCommand cmd3 = new SqlCommand(sqlFacture, conn, tr);
                    cmd3.Parameters.AddWithValue("@NUM", numerofacture);
                    cmd3.Parameters.AddWithValue("@CMD", commandeId);
                    cmd3.Parameters.AddWithValue("@CLT", clientId);
                    cmd3.Parameters.AddWithValue("@Total", montantApresRemise);
                    cmd3.ExecuteNonQuery();

                    tr.Commit();
                    MessageBox.Show("Commande enregistrée avec succès !");
                    return commandeId;
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message);
                    return 0;
                }
            }
        }

        public static DataTable GetPharmacistActivityData(int pharmacistId)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT DATEPART(WEEKDAY, c.DateCommande) - 1 as DayOfWeek,
                DATEPART(HOUR, c.DateCommande) as HourOfDay,
                COUNT(*) as ActivityCount
                FROM Commandes c
                WHERE c.PharmacienID = @PharmacistId
                    AND c.Valides = 1
                    AND c.DateCommande >= DATEADD(month, -3, GETDATE())
                GROUP BY DATEPART(WEEKDAY, c.DateCommande), DATEPART(HOUR, c.DateCommande)
                ORDER BY DayOfWeek, HourOfDay";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PharmacistId", pharmacistId);

                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable GetTopClientsByPharmacist(int pharmacienId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT TOP 5 C.Nom AS NomClient,
                         COUNT(*) AS TotalCommandes
            FROM Commandes CMD
            JOIN Clients C ON CMD.ClientID = C.ClientID
            WHERE CMD.PharmacienID = @pharmacienId
            GROUP BY C.Nom
            ORDER BY TotalCommandes DESC;
        ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@pharmacienId", pharmacienId);

                DataTable dt = new DataTable();
                conn.Open();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }
    }
}

