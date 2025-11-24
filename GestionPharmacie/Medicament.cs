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

        public Medicament(string reference, string nomMedicament, int categorieID, decimal prixAchat,
                          decimal prixVente, int quantiteStock, int seuilMinimum, DateTime datePeremption,
                          bool ordonnance = false)
        {
            Reference = reference;
            NomMedicament = nomMedicament;
            CategorieID = categorieID;
            PrixAchat = prixAchat;
            PrixVente = prixVente;
            QuantiteStock = quantiteStock;
            SeuilMinimum = seuilMinimum;
            DatePeremption = datePeremption;
            Ordonnance = ordonnance;
            Actif = true;
            DateCreation = DateTime.Now;
        }
        private string connectionString ="data source = LAPTOP-G7L9QSSV;initial catalog=GestionPharmacie;" +
                        "integrated security=True;TrustServerCertificate=True";

        public string Ajouter()
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
                    return " Médicament ajouté avec succès.";
                }
            }
            catch (Exception ex)
            {
                return " Erreur lors de l’ajout : " + ex.Message;
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
                                       Description = @Description,
                                       Forme = @Forme,
                                       Dosage = @Dosage,
                                       PrixAchat = @PrixAchat,
                                       PrixVente = @PrixVente,
                                       QuantiteStock = @QuantiteStock,
                                       SeuilMinimum = @SeuilMinimum,
                                       DatePeremption = @DatePeremption,
                                       NumeroLot = @NumeroLot,
                                       ImagePath = @ImagePath,
                                       Ordonnance = @Ordonnance,
                                       Actif = @Actif,
                                       DateModification = GETDATE()
                                   WHERE MedicamentID = @MedicamentID";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@MedicamentID", MedicamentID);
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

        public static DataTable GetAll()
        {
            string connectionString ="data source = LAPTOP-G7L9QSSV;initial catalog=GestionPharmacie;" +
        "integrated security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT MedicamentID, Reference, NomMedicament, CategorieID, Forme, Dosage, PrixVente, QuantiteStock,
                     SeuilMinimum, DatePeremption, NumeroLot, Ordonnance, Actif FROM Medicaments";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
