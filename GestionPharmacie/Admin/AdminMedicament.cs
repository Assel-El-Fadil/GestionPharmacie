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

        private void AdminMedicament_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = Medicament.GetAll();
            DashboardLoad();
        }

        private void DashboardLoad()
        {
            Medicament med = new Medicament();

            lblLowStock.Text = med.GetLowStockCount().ToString();
            lblExpiry.Text = med.GetExpiryCount(30).ToString();

            LoadLowStockPanel();
            LoadExpiryPanel();
            LoadCategories();
            LoadFormes();
        }

        private void LoadCategories()
        {
            DataTable dt = Medicament.getCategories();
            categorieCbox.Items.Clear();

            categorieCbox.DataSource = dt;
            categorieCbox.DisplayMember = "NomCategorie";
            categorieCbox.ValueMember = "CategorieID";
        }

        private void LoadFormes()
        {
            DataTable dt = Medicament.getFormes();
            FormeCbox.Items.Clear();

            FormeCbox.DataSource = dt;
            FormeCbox.DisplayMember = "Forme";
            FormeCbox.ValueMember = "Forme";
        }

        private void LoadLowStockPanel()
        {
            Medicament med = new Medicament();
            DataTable dt = med.GetLowStockList();
            panelLowStock.Controls.Clear();

            System.Windows.Forms.Label header = new System.Windows.Forms.Label();
            header.Text = "Stock faible";
            System.Drawing.Font headerFont = new System.Drawing.Font("Segoe UI Semibold", 12, FontStyle.Bold);
            header.Font = headerFont;
            header.Location = new Point(14, 14);
            System.Windows.Forms.Label subheader = new System.Windows.Forms.Label();
            subheader.Text = "Médicaments à réapprovisionner.";
            System.Drawing.Font subheaderFont = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
            subheader.Font = subheaderFont;
            subheader.Location = new Point(15, 44);
            subheader.ForeColor = Color.Gray;
            header.AutoSize = true;
            subheader.AutoSize = true;
            panelLowStock.Controls.Add(header);
            panelLowStock.Controls.Add(subheader);

            int y = 64;
            foreach (DataRow row in dt.Rows)
            {
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                lbl.Text = $"{row["NomMedicament"]} - {row["QuantiteStock"]}/{row["SeuilMinimum"]}";
                lbl.Width = panelLowStock.Width - 10;
                lbl.Height = 28;
                lbl.Margin = new Padding(4);
                lbl.Location = new Point(14, y);
                panelLowStock.Controls.Add(lbl);
            }
        }

        private void LoadExpiryPanel()
        {
            Medicament med = new Medicament();
            DataTable dt = med.GetExpiryList(30);
            panelExpiry.Controls.Clear();

            System.Windows.Forms.Label header = new System.Windows.Forms.Label();
            header.Text = "Péremption proche";
            System.Drawing.Font headerFont = new System.Drawing.Font("Segoe UI Semibold", 12, FontStyle.Bold);
            header.Font = headerFont;
            header.Location = new Point(14, 14);
            System.Windows.Forms.Label subheader = new System.Windows.Forms.Label();
            subheader.Text = "Médicaments arrivant à péremption.";
            System.Drawing.Font subheaderFont = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
            subheader.Font = subheaderFont;
            subheader.Location = new Point(15, 44);
            subheader.ForeColor = Color.Gray;
            header.AutoSize = true;
            subheader.AutoSize = true;
            panelExpiry.Controls.Add(header);
            panelExpiry.Controls.Add(subheader);

            int y = 64;
            foreach (DataRow row in dt.Rows)
            {
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                lbl.Text = $"{row["NomMedicament"]} — expires: {((DateTime)row["DatePeremption"]).ToShortDateString()}";
                lbl.Width = panelExpiry.Width - 25;
                lbl.Height = 28;
                lbl.Location = new Point(14, y);
                y += lbl.Height + 4;

                panelExpiry.Controls.Add(lbl);
            }
        }

        private void buttonChercher_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchtxt.Text))
            {
                MessageBox.Show("Veuillez entrer un IdMedicament.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int medicamentId;
            if (!int.TryParse(searchtxt.Text, out medicamentId))
            {
                MessageBox.Show("Le Id medicament doit être un nombre entier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "data source = DESKTOP-DDPB0HH\\GI2_1;initial catalog=GestionPharmacie;" +
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
            Medicament med = new Medicament();
            int medicamentId;
            DataTable dt = new DataTable();
            if (!int.TryParse(searchtxt.Text, out medicamentId))
            {
                dt = med.search(searchtxt.Text);
            }
            else if (searchtxt.Text == "")
            {
                dt = Medicament.GetAll();
            }
            else
            {
                dt = med.searchID(medicamentId);
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Aucun Medicament trouvé avec ce Id.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            GridView1.DataSource = dt;
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
            {
                DataTable dt = new Medicament().getMedicament(Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["MedicamentID"].Value));
                DataRow row = dt.Rows[0];
                ViewReference.Text = $"{row["Reference"]}";
                ViewNumLot.Text = $"{row["NumeroLot"]}";
                ViewNom.Text = $"{row["NomMedicament"]}";
                ViewDosage.Text = $"{row["Dosage"]}";
                ViewDesc.Text = $"{row["Description"]}";
                ViewForme.Text = $"{row["Forme"]}";
                ViewCat.Text = $"{row["CategorieID"]}";
                ViewAchat.Text = $"{row["PrixAchat"]} Dhs";
                ViewVente.Text = $"{row["PrixVente"]} Dhs";
                ViewStock.Text = $"{row["QuantiteStock"]} unités";
                ViewSeuil.Text = $"{row["SeuilMinimum"]} unités";
                ViewDate.Text = $"{Convert.ToDateTime(row["DatePeremption"]).ToShortDateString()}";
                ViewOrdo.Text = (Convert.ToBoolean(row["Ordonnance"])) ? "Requise" : "Non Requise";
                ViewAjout.Text = $"{Convert.ToDateTime(row["DateCreation"]).ToShortDateString()}";
                ViewMedicament.Visible = true;
            }
            else if (clickX < 60)
            {
                try
                {
                    Medicament m = new Medicament
                    {
                        MedicamentID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["MedicamentID"].Value),
                        Reference = GridView1.Rows[e.RowIndex].Cells["Reference"].Value?.ToString(),
                        NomMedicament = GridView1.Rows[e.RowIndex].Cells["NomMedicament"].Value?.ToString(),
                        CategorieID = Medicament.getCategoriebyNom((string)GridView1.Rows[e.RowIndex].Cells["NomCategorie"].Value),
                        Forme = GridView1.Rows[e.RowIndex].Cells["Forme"].Value?.ToString(),
                        PrixVente = Convert.ToDecimal(GridView1.Rows[e.RowIndex].Cells["PrixVente"].Value),
                        QuantiteStock = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["QuantiteStock"].Value),
                        SeuilMinimum = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells["SeuilMinimum"].Value),
                        DatePeremption = Convert.ToDateTime(GridView1.Rows[e.RowIndex].Cells["DatePeremption"].Value),
                        NumeroLot = GridView1.Rows[e.RowIndex].Cells["NumeroLot"].Value?.ToString(),
                        Actif = Convert.ToBoolean(GridView1.Rows[e.RowIndex].Cells["Actif"].Value)
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
            panelmedicaments.Visible = false;
            panelmedicaments.Dock = DockStyle.None;
            panelClients.Visible = false;
            panelClients.Dock = DockStyle.None;
            panelCommandes.Visible = false;
            panelCommandes.Dock = DockStyle.None;
            dashboard.Visible = true;
            dashboard.Dock = DockStyle.Fill;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            dashboard.Visible = false;
            dashboard.Dock = DockStyle.None;
            panelmedicaments.Visible = true;
            panelmedicaments.Dock = DockStyle.Fill;
            panelClients.Visible = false;
            panelClients.Dock = DockStyle.None;
            panelCommandes.Visible = false;
            panelCommandes.Dock = DockStyle.None;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            // show Commandes panel
            dashboard.Visible = false;
            dashboard.Dock = DockStyle.None;
            panelmedicaments.Visible = false;
            panelmedicaments.Dock = DockStyle.None;
            panelClients.Visible = false;
            panelClients.Dock = DockStyle.None;
            panelCommandes.Visible = true;
            panelCommandes.Dock = DockStyle.Fill;

            LoadCommandes();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            // show Clients panel
            dashboard.Visible = false;
            dashboard.Dock = DockStyle.None;
            panelmedicaments.Visible = false;
            panelmedicaments.Dock = DockStyle.None;
            panelCommandes.Visible = false;
            panelCommandes.Dock = DockStyle.None;
            panelClients.Visible = true;
            panelClients.Dock = DockStyle.Fill;

            LoadClients();
        }

        private void LoadClients()
        {
            DataTable dt = Client.GetAll();
            gridClients.DataSource = dt;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void iconButton6_Click_1(object sender, EventArgs e)
        {
            Medicament med = new Medicament();
            DataTable dt = new DataTable();
            searchtxt.Text = "";
            dt = Medicament.GetAll();
            GridView1.DataSource = dt;
        }

        private void Ajouter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtReference.Text) ||
                string.IsNullOrWhiteSpace(NomMedTxt.Text) ||
                string.IsNullOrWhiteSpace(DescTxt.Text) ||
                string.IsNullOrWhiteSpace(FormeCbox.Text) ||
                string.IsNullOrWhiteSpace(DosageTxt.Text) ||
                string.IsNullOrWhiteSpace(AchatTxt.Text) ||
                string.IsNullOrWhiteSpace(VenteTxt.Text) ||
                string.IsNullOrWhiteSpace(AmountTxt.Text) ||
                string.IsNullOrWhiteSpace(seuilTxt.Text) ||
                string.IsNullOrWhiteSpace(NumlotTxt.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.",
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (categorieCbox.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner une catégorie.",
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(AchatTxt.Text, out decimal prixAchat))
            {
                MessageBox.Show("Prix d'achat invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(VenteTxt.Text, out decimal prixVente))
            {
                MessageBox.Show("Prix de vente invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(AmountTxt.Text, out int quantiteStock))
            {
                MessageBox.Show("Quantité en stock invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(seuilTxt.Text, out int seuilMinimum))
            {
                MessageBox.Show("Seuil minimum invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime datePeremption = DateTxt.Value;
            if (datePeremption < DateTime.Today)
            {
                MessageBox.Show("La date de péremption doit être dans le futur.",
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string reference = TxtReference.Text.Trim();
            string nomMedicament = NomMedTxt.Text.Trim();
            string description = DescTxt.Text.Trim();
            string forme = FormeCbox.Text.Trim();
            string dosage = DosageTxt.Text.Trim();
            string numeroLot = NumlotTxt.Text.Trim();
            bool ordonnance = Ordonnance.Checked;

            int categorieID = Convert.ToInt32(categorieCbox.SelectedValue);

            Medicament med = new Medicament(reference, nomMedicament, categorieID, description, forme,
                                      dosage, prixAchat, prixVente, quantiteStock, seuilMinimum,
                                      datePeremption, numeroLot, ordonnance);

            bool result = med.Ajouter();

            TxtReference.Text = "";
            NumlotTxt.Text = "";
            NomMedTxt.Text = "";
            DescTxt.Text = "";
            categorieCbox.SelectedIndex = 0;
            FormeCbox.SelectedIndex = 0;
            DosageTxt.Text = "";
            AchatTxt.Text = "";
            VenteTxt.Text = "";
            AmountTxt.Text = "";
            seuilTxt.Text = "";
            Ordonnance.Checked = false;
            DateTxt.Value = DateTime.Now;

            if (result)
            {
                MessageBox.Show("Médicament ajouté avec succès.", "Succès",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout du médicament.", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewMedicament.Visible = false;
        }

        private void iconPictureBox4_Click(object sender, EventArgs e)
        {
            ViewMedicament.Visible = false;
        }

        private void AddPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void searchClientBtn_Click(object sender, EventArgs e)
        {
            string txt = searchClientTxt.Text.Trim();
            DataTable dt;
            if (string.IsNullOrWhiteSpace(txt))
            {
                dt = Client.GetAll();
            }
            else
            {
                dt = Client.Search(txt);
            }
            gridClients.DataSource = dt;
        }

        private void resetClientBtn_Click(object sender, EventArgs e)
        {
            searchClientTxt.Text = string.Empty;
            gridClients.DataSource = Client.GetAll();
        }

        private void gridClients_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == gridClients.Columns["ClientsActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.ClipBounds, true);

                int x = e.CellBounds.Left + 5;
                int y = e.CellBounds.Top + (e.CellBounds.Height - 20) / 2;

                // edit icon
                e.Graphics.DrawImage(Properties.Resources.edit, new Rectangle(x, y, 20, 20));
                x += 30;
                // delete icon
                e.Graphics.DrawImage(Properties.Resources.delete, new Rectangle(x, y, 20, 20));

                e.Handled = true;
            }
        }

        private void gridClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != gridClients.Columns["ClientsActions"].Index || e.RowIndex < 0)
                return;

            int cellLeft = gridClients.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).X;
            int clickX = gridClients.PointToClient(Cursor.Position).X - cellLeft;

            int id = Convert.ToInt32(gridClients.Rows[e.RowIndex].Cells["UtilisateurID"].Value);
            string numeroClient = gridClients.Rows[e.RowIndex].Cells["NumeroClient"].Value?.ToString();
            string nom = gridClients.Rows[e.RowIndex].Cells["Nom"].Value?.ToString();
            string prenom = gridClients.Rows[e.RowIndex].Cells["Prenom"].Value?.ToString();
            string telephone = gridClients.Rows[e.RowIndex].Cells["Telephone"].Value?.ToString();
            string email = gridClients.Rows[e.RowIndex].Cells["Email"].Value?.ToString();

            if (clickX < 30)
            {
                // Modifier client
                Client c = new Client
                {
                    UtilisateurID = id,
                    NumeroClient = numeroClient,
                    Nom = nom,
                    Prenom = prenom,
                    Telephone = telephone,
                    Email = email
                };

                DialogResult confirm = MessageBox.Show(
                    $"Voulez-vous modifier le client '{nom} {prenom}' (N° {numeroClient}) ?",
                    "Confirmation de modification",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    string result = Client.UpdateClient(c);
                    MessageBox.Show(result, "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadClients();
                }
            }
            else if (clickX < 60)
            {
                // Supprimer client
                DialogResult confirm = MessageBox.Show(
                    $"Voulez-vous vraiment supprimer le client '{nom} {prenom}' (N° {numeroClient}) ?",
                    "Confirmation de suppression",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    string result = Client.DeleteClient(id);
                    MessageBox.Show(result, "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadClients();
                }
            }
        }

        private void buttonNouveauClient_Click(object sender, EventArgs e)
        {
            AddClientPanel.Visible = !AddClientPanel.Visible;
        }

        private void iconPictureBoxCloseClient_Click(object sender, EventArgs e)
        {
            AddClientPanel.Visible = false;
        }

        private void buttonAnnulerClient_Click(object sender, EventArgs e)
        {
            AddClientPanel.Visible = false;
        }

        private void buttonAjouterClient_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtNumeroClient.Text) ||
                string.IsNullOrWhiteSpace(txtNomClient.Text) ||
                string.IsNullOrWhiteSpace(txtPrenomClient.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires (marqués d'un *).",
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate email format if provided
            if (!string.IsNullOrWhiteSpace(txtEmailClient.Text))
            {
                try
                {
                    var mail = new System.Net.Mail.MailAddress(txtEmailClient.Text);
                }
                catch
                {
                    MessageBox.Show("Format d'email invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Get values
            string numeroClient = txtNumeroClient.Text.Trim();
            string nom = txtNomClient.Text.Trim();
            string prenom = txtPrenomClient.Text.Trim();
            string cin = txtCIN.Text.Trim();
            string telephone = txtTelephoneClient.Text.Trim();
            string email = txtEmailClient.Text.Trim();
            DateTime dateNaissance = dateNaissanceClient.Value;
            string ville = txtVille.Text.Trim();

            // Add client
            bool result = Client.AddClient(numeroClient, nom, prenom, cin, telephone, email, dateNaissance, ville);

            // Clear fields
            txtNumeroClient.Text = "";
            txtNomClient.Text = "";
            txtPrenomClient.Text = "";
            txtCIN.Text = "";
            txtTelephoneClient.Text = "";
            txtEmailClient.Text = "";
            txtVille.Text = "";
            dateNaissanceClient.Value = DateTime.Now;

            if (result)
            {
                MessageBox.Show("Client ajouté avec succès.", "Succès",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddClientPanel.Visible = false;
                LoadClients(); // Refresh the clients list
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout du client.", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCommandes()
        {
            DataTable dt = Commande.GetAll();
            FormatCommandesGrid(dt);
            gridCommandes.DataSource = dt;
        }

        private void FormatCommandesGrid(DataTable dt)
        {
            if (dt.Columns.Contains("Date"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Date"] != DBNull.Value && row["Date"] is DateTime)
                    {
                        row["Date"] = ((DateTime)row["Date"]).ToString("dd/MM/yyyy");
                    }
                }
            }
            if (dt.Columns.Contains("Montant"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Montant"] != DBNull.Value)
                    {
                        decimal montant = Convert.ToDecimal(row["Montant"]);
                        row["Montant"] = montant.ToString("F2");
                    }
                }
            }
        }

        private void searchCommandeBtn_Click(object sender, EventArgs e)
        {
            string txt = searchCommandeTxt.Text.Trim();
            DataTable dt;

            if (string.IsNullOrWhiteSpace(txt))
            {
                dt = Commande.GetAll();
            }
            else
            {
                dt = Commande.Search(txt);
            }

            FormatCommandesGrid(dt);
            gridCommandes.DataSource = dt;
        }

        private void resetCommandeBtn_Click(object sender, EventArgs e)
        {
            searchCommandeTxt.Text = string.Empty;
            LoadCommandes();
        }

        private void gridCommandes_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == gridCommandes.Columns["CommandesActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.ClipBounds, true);

                int x = e.CellBounds.Left + 5;
                int y = e.CellBounds.Top + (e.CellBounds.Height - 20) / 2;

                // view icon (eye) only
                e.Graphics.DrawImage(Properties.Resources.view, new Rectangle(x, y, 20, 20));

                e.Handled = true;
            }
        }

        private void gridCommandes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != gridCommandes.Columns["CommandesActions"].Index || e.RowIndex < 0)
                return;

            int cellLeft = gridCommandes.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).X;
            int clickX = gridCommandes.PointToClient(Cursor.Position).X - cellLeft;

            if (clickX < 30)
            {
                // View commande details
                int commandeID = Convert.ToInt32(gridCommandes.Rows[e.RowIndex].Cells["CommandeID"].Value);
                ShowCommandeDetails(commandeID);
            }
        }

        private void ShowCommandeDetails(int commandeID)
        {
            DataTable dtCommande = Commande.GetCommandeDetails(commandeID);
            if (dtCommande.Rows.Count == 0)
            {
                MessageBox.Show("Commande introuvable.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow cmd = dtCommande.Rows[0];
            DataTable dtItems = Commande.GetCommandeItems(commandeID);

            // Populate commande details
            labelCommandeNumero.Text = $"Détails de la commande {cmd["NumeroCommande"]}";
            labelCommandeDate.Text = $"Commande du {((DateTime)cmd["DateCommande"]).ToString("dd/MM/yyyy")}";
            labelCommandeClient.Text = cmd["Client"]?.ToString() ?? "";
            labelCommandeStatut.Text = cmd["Statut"]?.ToString() ?? "";
            labelCommandeDateValue.Text = ((DateTime)cmd["DateCommande"]).ToString("dd/MM/yyyy");
            labelCommandePaiement.Text = cmd["Paiement"]?.ToString() ?? "";
            labelCommandeMontantTotal.Text = $"{Convert.ToDecimal(cmd["MontantTotal"]):F2} DH";

            // Populate items grid
            gridCommandeItems.DataSource = dtItems;
            if (dtItems.Columns.Contains("PrixUnitaire"))
            {
                foreach (DataRow row in dtItems.Rows)
                {
                    if (row["PrixUnitaire"] != DBNull.Value)
                    {
                        decimal prix = Convert.ToDecimal(row["PrixUnitaire"]);
                        row["PrixUnitaire"] = prix.ToString("F2");
                    }
                   
                }
            }

            ViewCommande.Visible = true;
        }

        private void iconPictureBoxCloseCommande_Click(object sender, EventArgs e)
        {
            ViewCommande.Visible = false;
        }

        private void buttonFermerCommande_Click(object sender, EventArgs e)
        {
            ViewCommande.Visible = false;
        }
    }
}
