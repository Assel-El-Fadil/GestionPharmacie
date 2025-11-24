using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace GestionPharmacie.Admin
{
    public partial class AdminMedicament : Form
    {
        public AdminMedicament()
        {
            InitializeComponent();
        }

        private void GridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridView1.Columns[e.ColumnIndex].Name == "Supprimer" && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["MedicamentID"].Value);
                string nom = GridView1.Rows[e.RowIndex].Cells["NomMedicament"].Value.ToString();

                DialogResult confirm = MessageBox.Show(
                    $"Voulez-vous vraiment supprimer le médicament '{nom}' (ID {id}) ?",
                    "Confirmation de suppression",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    Medicament m = new Medicament();
                    m.MedicamentID = id;

                    string result = m.Supprimer();

                    MessageBox.Show(result, "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AdminMedicament_Load(sender, e);
                }
            }
            else if (GridView1.Columns[e.ColumnIndex].Name == "Modifier" && e.RowIndex >= 0)
            {
                try
                {
                    Medicament m = new Medicament
                    {
                        MedicamentID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["MedicamentID"].Value),
                        Reference = GridView1.Rows[e.RowIndex].Cells["Reference"].Value?.ToString(),
                        NomMedicament = GridView1.Rows[e.RowIndex].Cells["NomMedicament"].Value?.ToString(),
                        CategorieID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["CategorieID"].Value),
                        Description = GridView1.Rows[e.RowIndex].Cells["Description"].Value?.ToString(),
                        Forme = GridView1.Rows[e.RowIndex].Cells["Forme"].Value?.ToString(),
                        Dosage = GridView1.Rows[e.RowIndex].Cells["Dosage"].Value?.ToString(),
                        PrixAchat = Convert.ToDecimal(GridView1.Rows[e.RowIndex].Cells["PrixAchat"].Value),
                        PrixVente = Convert.ToDecimal(GridView1.Rows[e.RowIndex].Cells["PrixVente"].Value),
                        QuantiteStock = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["QuantiteStock"].Value),
                        SeuilMinimum = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["SeuilMinimum"].Value),
                        DatePeremption = Convert.ToDateTime(GridView1.Rows[e.RowIndex].Cells["DatePeremption"].Value),
                        NumeroLot = GridView1.Rows[e.RowIndex].Cells["NumeroLot"].Value?.ToString(),
                        ImagePath = GridView1.Rows[e.RowIndex].Cells["ImagePath"].Value?.ToString(),
                        Ordonnance = Convert.ToBoolean(GridView1.Rows[e.RowIndex].Cells["Ordonnance"].Value),
                        Actif = Convert.ToBoolean(GridView1.Rows[e.RowIndex].Cells["Actif"].Value),
                        DateCreation = Convert.ToDateTime(GridView1.Rows[e.RowIndex].Cells["DateCreation"].Value),
                        DateModification = DateTime.Now
                    };

                    DialogResult confirm = MessageBox.Show(
                        $"Voulez-vous modifier le médicament '{m.NomMedicament}' (ID {m.MedicamentID}) ?",
                        "Confirmation de modification",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirm == DialogResult.Yes)
                    {
                        string result = m.Modifier();

                        MessageBox.Show(result, "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AdminMedicament_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" Erreur lors de la modification : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void AdminMedicament_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = Medicament.GetAll();
        }

        private void buttonChercher_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Veuillez entrer un IdMedicament.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int medicamentId;
            if (!int.TryParse(txtId.Text, out medicamentId))
            {
                MessageBox.Show("Le Id medicament doit être un nombre entier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "data source = LAPTOP-G7L9QSSV;initial catalog=GestionPharmacie;" +
                        "integrated security=True;TrustServerCertificate=True";

            string sql = $"SELECT MedicamentID, Reference, NomMedicament, CategorieID, Forme, Dosage, PrixVente, QuantiteStock," +
                $" SeuilMinimum, DatePeremption, NumeroLot, Ordonnance, Actif FROM Medicaments WHERE MedicamentId={medicamentId}";

            conn.Open();
            SqlDataAdapter dp = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            dp.Fill(dt);
            conn.Close();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Aucun Medicament trouvé avec ce Id.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            GridView1.DataSource = dt;
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            new Connexion().Show();
            this.Close();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {

        }

        private void buttonNouveau_Click(object sender, EventArgs e)
        {
            AddPanel.Visible = !(AddPanel.Visible);
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            AddPanel.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddPanel.Visible = false;
        }

        private void GridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == GridView1.Columns["Actions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.ClipBounds, true);

                int x = e.CellBounds.Left + 5;
                int y = e.CellBounds.Top + (e.CellBounds.Height - 20) / 2;

                // draw icons (import into Resources first)
                e.Graphics.DrawImage(Properties.Resources.view, new Rectangle(x, y, 20, 20));
                x += 30;
                e.Graphics.DrawImage(Properties.Resources.edit, new Rectangle(x, y, 20, 20));
                x += 30;
                e.Graphics.DrawImage(Properties.Resources.delete, new Rectangle(x, y, 20, 20));

                e.Handled = true;
            }
        }

        private void GridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != GridView1.Columns["Actions"].Index || e.RowIndex < 0)
                return;

            int cellLeft = GridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).X;
            int clickX = GridView1.PointToClient(Cursor.Position).X - cellLeft;

            if (clickX < 30)
                MessageBox.Show("View clicked");
            else if (clickX < 60)
            {
                try
                {
                    Medicament m = new Medicament
                    {
                        MedicamentID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["MedicamentID"].Value),
                        Reference = GridView1.Rows[e.RowIndex].Cells["Reference"].Value?.ToString(),
                        NomMedicament = GridView1.Rows[e.RowIndex].Cells["NomMedicament"].Value?.ToString(),
                        CategorieID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["CategorieID"].Value),
                        Description = GridView1.Rows[e.RowIndex].Cells["Description"].Value?.ToString(),
                        Forme = GridView1.Rows[e.RowIndex].Cells["Forme"].Value?.ToString(),
                        Dosage = GridView1.Rows[e.RowIndex].Cells["Dosage"].Value?.ToString(),
                        PrixAchat = Convert.ToDecimal(GridView1.Rows[e.RowIndex].Cells["PrixAchat"].Value),
                        PrixVente = Convert.ToDecimal(GridView1.Rows[e.RowIndex].Cells["PrixVente"].Value),
                        QuantiteStock = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["QuantiteStock"].Value),
                        SeuilMinimum = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["SeuilMinimum"].Value),
                        DatePeremption = Convert.ToDateTime(GridView1.Rows[e.RowIndex].Cells["DatePeremption"].Value),
                        NumeroLot = GridView1.Rows[e.RowIndex].Cells["NumeroLot"].Value?.ToString(),
                        ImagePath = GridView1.Rows[e.RowIndex].Cells["ImagePath"].Value?.ToString(),
                        Ordonnance = Convert.ToBoolean(GridView1.Rows[e.RowIndex].Cells["Ordonnance"].Value),
                        Actif = Convert.ToBoolean(GridView1.Rows[e.RowIndex].Cells["Actif"].Value),
                        DateCreation = Convert.ToDateTime(GridView1.Rows[e.RowIndex].Cells["DateCreation"].Value),
                        DateModification = DateTime.Now
                    };

                    DialogResult confirm = MessageBox.Show(
                        $"Voulez-vous modifier le médicament '{m.NomMedicament}' (ID {m.MedicamentID}) ?",
                        "Confirmation de modification",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirm == DialogResult.Yes)
                    {
                        string result = m.Modifier();

                        MessageBox.Show(result, "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AdminMedicament_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" Erreur lors de la modification : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (clickX < 90)
            {
                int id = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["MedicamentID"].Value);
                string nom = GridView1.Rows[e.RowIndex].Cells["NomMedicament"].Value.ToString();

                DialogResult confirm = MessageBox.Show(
                    $"Voulez-vous vraiment supprimer le médicament '{nom}' (ID {id}) ?",
                    "Confirmation de suppression",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    Medicament m = new Medicament();
                    m.MedicamentID = id;

                    string result = m.Supprimer();

                    MessageBox.Show(result, "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AdminMedicament_Load(sender, e);
                }

            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            dashboard.Visible = true;
            panelmedicaments.Visible = false;
        }
    }
}
