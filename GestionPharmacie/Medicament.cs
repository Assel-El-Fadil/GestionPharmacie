using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace GestionPharmacie
{
    public class Medicament
    {
        public int MedicamentID { get; set; }
        public string Reference { get; set; }
        public string NomMedicament { get; set; }
        public int CategorieID { get; set; }
        public string Description { get; set; }
        public string Forme { get; set; }
        public string Dosage { get; set; }
        public decimal PrixAchat { get; set; }
        public decimal PrixVente { get; set; }
        public int QuantiteStock { get; set; }
        public int SeuilMinimum { get; set; }
        public DateTime DatePeremption { get; set; }
        public string NumeroLot { get; set; }
        public string ImagePath { get; set; }
        public bool Ordonnance { get; set; }
        public bool Actif { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModification { get; set; }
        public Medicament() { }

        public Medicament(string reference, string nomMedicament, int categorieID, String description, String forme, String dosage, decimal prixAchat,
                          decimal prixVente, int quantiteStock, int seuilMinimum, DateTime datePeremption, String numLot,
                          bool ordonnance = false)
        {
            Reference = reference;
            NomMedicament = nomMedicament;
            CategorieID = categorieID;
            Description = description;
            Forme = forme;
            Dosage = dosage;
            PrixAchat = prixAchat;
            PrixVente = prixVente;
            QuantiteStock = quantiteStock;
            SeuilMinimum = seuilMinimum;
            DatePeremption = datePeremption;
            NumeroLot = numLot;
            Ordonnance = ordonnance;
            Actif = true;
            DateCreation = DateTime.Now;
        }
        private static string connectionString = "data source = DESKTOP-DDPB0HH\\GI2_1;initial catalog=AppPharmacie;" +
                        "integrated security=True;TrustServerCertificate=True";

        public bool Ajouter()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO Medicaments 
                                   (Reference, NomMedicament, CategorieID, Description, Forme, Dosage, 
                                    PrixAchat, PrixVente, QuantiteStock, SeuilMinimum, DatePeremption, 
                                    NumeroLot, ImagePath, Ordonnance, Actif)
                                   VALUES
                                   (@Reference, @NomMedicament, @CategorieID, @Description, @Forme, @Dosage,
                                    @PrixAchat, @PrixVente, @QuantiteStock, @SeuilMinimum, @DatePeremption,
                                    @NumeroLot, @ImagePath, @Ordonnance, @Actif)";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@Reference", Reference);
                    cmd.Parameters.AddWithValue("@NomMedicament", NomMedicament);
                    cmd.Parameters.AddWithValue("@CategorieID", CategorieID);
                    cmd.Parameters.AddWithValue("@Description", (object)Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Forme", (object)Forme ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Dosage", (object)Dosage ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PrixAchat", PrixAchat);
                    cmd.Parameters.AddWithValue("@PrixVente", PrixVente);
                    cmd.Parameters.AddWithValue("@QuantiteStock", QuantiteStock);
                    cmd.Parameters.AddWithValue("@SeuilMinimum", SeuilMinimum);
                    cmd.Parameters.AddWithValue("@DatePeremption", DatePeremption);
                    cmd.Parameters.AddWithValue("@NumeroLot", (object)NumeroLot ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordonnance", Ordonnance);
                    cmd.Parameters.AddWithValue("@Actif", Actif);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string Modifier()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"UPDATE Medicaments
                                   SET NomMedicament = @NomMedicament,
                                       CategorieID = @CategorieID,
                                       Forme = @Forme,
                                       PrixVente = @PrixVente,
                                       QuantiteStock = @QuantiteStock,
                                       SeuilMinimum = @SeuilMinimum,
                                       DatePeremption = @DatePeremption,
                                       NumeroLot = @NumeroLot,
                                       Actif = @Actif,
                                       DateModification = GETDATE()
                                   WHERE MedicamentID = @MedicamentID";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@MedicamentID", MedicamentID);
                    cmd.Parameters.AddWithValue("@NomMedicament", NomMedicament);
                    cmd.Parameters.AddWithValue("@CategorieID", CategorieID);
                    cmd.Parameters.AddWithValue("@Forme", (object)Forme ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PrixVente", PrixVente);
                    cmd.Parameters.AddWithValue("@QuantiteStock", QuantiteStock);
                    cmd.Parameters.AddWithValue("@SeuilMinimum", SeuilMinimum);
                    cmd.Parameters.AddWithValue("@DatePeremption", DatePeremption);
                    cmd.Parameters.AddWithValue("@NumeroLot", (object)NumeroLot ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Actif", Actif);

                    cmd.ExecuteNonQuery();
                    return "Médicament modifié avec succès.";
                }
            }
            catch (Exception ex)
            {
                return " Erreur lors de la modification : " + ex.Message;
            }
        }

        public string Supprimer()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM Medicaments WHERE MedicamentID = @MedicamentID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MedicamentID", MedicamentID);
                    cmd.ExecuteNonQuery();
                    return " Médicament supprimé avec succès.";
                }
            }
            catch (Exception ex)
            {
                return " Erreur lors de la suppression : " + ex.Message;
            }
        }

        public static DataTable getCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT CategorieID, NomCategorie FROM Categories";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable getFormes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                String sql = "SELECT DISTINCT Forme FROM Medicaments;";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT MedicamentID, Reference, NomMedicament, NomCategorie, Forme, PrixVente, QuantiteStock, 
                    SeuilMinimum, DatePeremption, NumeroLot, Actif FROM Medicaments INNER JOIN Categories 
                    ON Medicaments.CategorieID = Categories.CategorieID";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int GetLowStockCount()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) 
                       FROM Medicaments
                       WHERE QuantiteStock <= SeuilMinimum
                       AND Actif = 1";

                SqlCommand cmd = new SqlCommand(sql, conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetExpiryCount(int days)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*)
                        FROM Medicaments
                        WHERE Actif = 1
                          AND (DatePeremption < GETDATE() 
                               OR DatePeremption <= DATEADD(DAY, @Days, GETDATE()))";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Days", days);

                return (int)cmd.ExecuteScalar();
            }
        }

        public DataTable GetLowStockList()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT NomMedicament, QuantiteStock, SeuilMinimum
                       FROM Medicaments
                       WHERE QuantiteStock <= SeuilMinimum
                       AND Actif = 1";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetExpiryList(int days)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT NomMedicament, DatePeremption
                        FROM Medicaments
                        WHERE Actif = 1
                          AND (DatePeremption < GETDATE() 
                               OR DatePeremption <= DATEADD(DAY, @Days, GETDATE()))
                        ORDER BY DatePeremption ASC;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Days", days);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable search(String txt)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT MedicamentID, Reference, NomMedicament, NomCategorie, Forme,PrixVente, QuantiteStock, 
                    SeuilMinimum, DatePeremption, NumeroLot, Actif FROM Medicaments INNER JOIN Categories 
                    ON Medicaments.CategorieID = Categories.CategorieID WHERE NomMedicament = @txt OR NomCategorie = @txt;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@txt", txt);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable searchID(int txt)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT MedicamentID, Reference, NomMedicament, NomCategorie, Forme, PrixVente, QuantiteStock, 
                    SeuilMinimum, DatePeremption, NumeroLot, Actif FROM Medicaments INNER JOIN Categories 
                    ON Medicaments.CategorieID = Categories.CategorieID WHERE MedicamentID = @txt;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@txt", txt);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable getMedicament(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT * FROM Medicaments WHERE MedicamentID = @id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int getCategoriebyNom(String Nom)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT CategorieID FROM Categories WHERE NomCategorie = @Nom;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nom", Nom);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static DataTable GetMonthlyRevenue()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT FORMAT(DateCommande, 'MMM') AS Month, SUM(MontantApresRemise) AS Revenue
                    FROM Commandes GROUP BY FORMAT(DateCommande, 'MMM'), MONTH(DateCommande)
                    ORDER BY MONTH(DateCommande);";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetTop5Sold()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT TOP 5 M.NomMedicament, SUM(D.Quantite) AS TotalVentes FROM DetailsCommande D
                    INNER JOIN Medicaments M ON D.MedicamentID = M.MedicamentID INNER JOIN Commandes C ON D.CommandeID = C.CommandeID
                    GROUP BY M.NomMedicament ORDER BY TotalVentes DESC;";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetCategoryCounts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT TOP 5 C.NomCategorie, COUNT(M.MedicamentID) AS Count FROM Categories C
                    LEFT JOIN Medicaments M ON M.CategorieID = C.CategorieID GROUP BY C.NomCategorie ORDER BY Count DESC;";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static String getTotalRevenue()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT SUM(MontantApresRemise) FROM Commandes";
                SqlCommand cmd = new SqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToString(result) : "";
            }
        }

        public static String getTotalOrders()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) FROM Commandes;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                return Convert.ToString(cmd.ExecuteScalar());
            }
        }

        public static String getAverageOrder()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT ROUND(AVG(MontantApresRemise), 3) FROM Commandes;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToString(result) : "";
            }
        }

        public static decimal getPrixVente(int id) {
            decimal prixUnitaire = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT PrixVente FROM Medicaments WHERE MedicamentID = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                prixUnitaire = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            return prixUnitaire;
        }

        public static DataTable GetTopMedicinesByPharmacist(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT TOP 5 M.NomMedicament, SUM(D.Quantite) AS TotalVentes FROM DetailsCommande D
                    INNER JOIN Medicaments M ON D.MedicamentID = M.MedicamentID 
                    INNER JOIN Commandes C ON D.CommandeID = C.CommandeID
                    WHERE C.PharmacienID = @PharmacistID
                    GROUP BY M.NomMedicament ORDER BY TotalVentes DESC;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PharmacistID", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetTotalQuantityByPharmacist(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT NomMedicament, SUM(D.Quantite) AS QuantiteTotale FROM DetailsCommande D
                    INNER JOIN Medicaments M ON D.MedicamentID = M.MedicamentID
                    INNER JOIN Commandes C ON D.CommandeID = C.CommandeID
                    WHERE C.PharmacienID = @PharmacistID GROUP BY NomMedicament;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PharmacistID", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
