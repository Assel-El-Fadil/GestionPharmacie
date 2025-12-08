using GestionPharmacie.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GestionPharmacie
{
    public partial class Connexion : Form
    {
        public Connexion()
        {
            InitializeComponent();
        }

        private void Connexion_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Email.Text == "" || Password.Text == "")
            {
                message.Text = "veuiller remplir les champs";
            }

            else
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "data source = DESKTOP-DDPB0HH\\GI2_1;initial catalog=AppPharmacie;" +
                        "integrated security=True;TrustServerCertificate=True";
                String sql = "select * from Utilisateurs where NomUtilisateur='" + Email.Text + "' and MotDePasse='" + Password.Text + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Utilisateur utilisateur = new Utilisateur(reader.GetInt32(0));
                    AdminMedicament admin = new AdminMedicament(utilisateur);
                    admin.Show();
                    this.Hide();
                }
                else
                {
                    message.Text = "Nom d'utilisateur ou mot de passe incorrect";
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new Signup().Show();
            this.Hide();
        }
    }
}
