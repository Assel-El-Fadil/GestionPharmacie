using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GestionPharmacie
{
    public partial class Signup : Form
    {
        private readonly string connectionString = "data source = DESKTOP-DDPB0HH\\GI2_1;initial catalog=AppPharmacie;integrated security=True;TrustServerCertificate=True";

        public Signup()
        {
            InitializeComponent();
            button1.Click += button1_Click;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            new Connexion().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Nom.Text) ||
                string.IsNullOrWhiteSpace(Prenom.Text) ||
                string.IsNullOrWhiteSpace(CIN.Text) ||
                string.IsNullOrWhiteSpace(Adresse.Text) ||
                string.IsNullOrWhiteSpace(Ville.Text) ||
                string.IsNullOrWhiteSpace(Telephone.Text) ||
                string.IsNullOrWhiteSpace(Email.Text) ||
                string.IsNullOrWhiteSpace(Username.Text) ||
                string.IsNullOrWhiteSpace(Password.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"INSERT INTO Utilisateurs
                                   (NomUtilisateur, MotDePasse, Nom, Prenom, DateNaissance, CIN, Adresse, Ville, Telephone, Email, Actif)
                                   VALUES
                                   (@NomUtilisateur, @MotDePasse, @Nom, @Prenom, @DateNaissance, @CIN, @Adresse, @Ville, @Telephone, @Email, 1)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@NomUtilisateur", Username.Text.Trim());
                        cmd.Parameters.AddWithValue("@MotDePasse", Password.Text.Trim());
                        cmd.Parameters.AddWithValue("@Nom", Nom.Text.Trim());
                        cmd.Parameters.AddWithValue("@Prenom", Prenom.Text.Trim());
                        cmd.Parameters.AddWithValue("@DateNaissance", DateNaissance.Value.Date);
                        cmd.Parameters.AddWithValue("@CIN", CIN.Text.Trim());
                        cmd.Parameters.AddWithValue("@Adresse", Adresse.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ville", Ville.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telephone", Telephone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", Email.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Compte pharmacien créé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                new Connexion().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création du compte : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
