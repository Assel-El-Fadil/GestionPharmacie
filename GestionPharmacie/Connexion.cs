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
                conn.ConnectionString = "data source = LAPTOP-G7L9QSSV;initial catalog=GestionPharmacie;" +
                        "integrated security=True;TrustServerCertificate=True";
                String sql = "select * from Utilisateurs where Email ='" + Email.Text + "' and MotDePasse='" + Password.Text + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    AdminMedicament admin = new AdminMedicament();
                    admin.Show();
                    this.Hide();
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
