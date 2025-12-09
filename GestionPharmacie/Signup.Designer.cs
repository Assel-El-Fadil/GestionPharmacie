namespace GestionPharmacie
{
    partial class Signup
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Signup));
            Password = new TextBox();
            Username = new TextBox();
            Nom = new TextBox();
            Prenom = new TextBox();
            CIN = new TextBox();
            Adresse = new TextBox();
            Ville = new TextBox();
            Telephone = new TextBox();
            Email = new TextBox();
            DateNaissance = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            button1 = new Button();
            panel2 = new Panel();
            panelNom = new Panel();
            panelPrenom = new Panel();
            panelCin = new Panel();
            panelAdresse = new Panel();
            panelVille = new Panel();
            panelTelephone = new Panel();
            panelEmail = new Panel();
            pictureBox1 = new PictureBox();
            panel3 = new Panel();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // Password
            // 
            Password.BorderStyle = BorderStyle.None;
            Password.Location = new Point(120, 384);
            Password.MaxLength = 50;
            Password.Name = "Password";
            Password.PlaceholderText = "Password";
            Password.Size = new Size(250, 16);
            Password.TabIndex = 21;
            Password.UseSystemPasswordChar = true;
            // 
            // Username
            // 
            Username.BorderStyle = BorderStyle.None;
            Username.Location = new Point(120, 354);
            Username.MaxLength = 50;
            Username.Name = "Username";
            Username.PlaceholderText = "Username";
            Username.Size = new Size(250, 16);
            Username.TabIndex = 20;
            // 
            // Nom
            // 
            Nom.BorderStyle = BorderStyle.None;
            Nom.Location = new Point(120, 82);
            Nom.MaxLength = 100;
            Nom.Name = "Nom";
            Nom.PlaceholderText = "Nom";
            Nom.Size = new Size(250, 16);
            Nom.TabIndex = 22;
            // 
            // Prenom
            // 
            Prenom.BorderStyle = BorderStyle.None;
            Prenom.Location = new Point(120, 115);
            Prenom.MaxLength = 100;
            Prenom.Name = "Prenom";
            Prenom.PlaceholderText = "Prénom";
            Prenom.Size = new Size(250, 16);
            Prenom.TabIndex = 23;
            // 
            // CIN
            // 
            CIN.BorderStyle = BorderStyle.None;
            CIN.Location = new Point(120, 182);
            CIN.MaxLength = 20;
            CIN.Name = "CIN";
            CIN.PlaceholderText = "CIN";
            CIN.Size = new Size(250, 16);
            CIN.TabIndex = 25;
            // 
            // Adresse
            // 
            Adresse.BorderStyle = BorderStyle.None;
            Adresse.Location = new Point(120, 214);
            Adresse.MaxLength = 255;
            Adresse.Name = "Adresse";
            Adresse.PlaceholderText = "Adresse";
            Adresse.Size = new Size(250, 16);
            Adresse.TabIndex = 26;
            // 
            // Ville
            // 
            Ville.BorderStyle = BorderStyle.None;
            Ville.Location = new Point(120, 254);
            Ville.MaxLength = 100;
            Ville.Name = "Ville";
            Ville.PlaceholderText = "Ville";
            Ville.Size = new Size(250, 16);
            Ville.TabIndex = 27;
            // 
            // Telephone
            // 
            Telephone.BorderStyle = BorderStyle.None;
            Telephone.Location = new Point(120, 290);
            Telephone.MaxLength = 20;
            Telephone.Name = "Telephone";
            Telephone.PlaceholderText = "Téléphone";
            Telephone.Size = new Size(250, 16);
            Telephone.TabIndex = 28;
            // 
            // Email
            // 
            Email.BorderStyle = BorderStyle.None;
            Email.Location = new Point(120, 322);
            Email.MaxLength = 100;
            Email.Name = "Email";
            Email.PlaceholderText = "Email";
            Email.Size = new Size(250, 16);
            Email.TabIndex = 29;
            // 
            // DateNaissance
            // 
            DateNaissance.Format = DateTimePickerFormat.Short;
            DateNaissance.Location = new Point(120, 148);
            DateNaissance.Name = "DateNaissance";
            DateNaissance.Size = new Size(250, 23);
            DateNaissance.TabIndex = 24;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Cursor = Cursors.Hand;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label3.Location = new Point(226, 470);
            label3.Name = "label3";
            label3.Size = new Size(34, 16);
            label3.TabIndex = 19;
            label3.Text = "Quit";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Cursor = Cursors.Hand;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label2.Location = new Point(321, 412);
            label2.Name = "label2";
            label2.Size = new Size(49, 16);
            label2.TabIndex = 18;
            label2.Text = "Log in";
            label2.Click += label2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(1, 109, 72);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Bahnschrift", 14F, FontStyle.Bold);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(120, 431);
            button1.Name = "button1";
            button1.Size = new Size(250, 36);
            button1.TabIndex = 17;
            button1.Text = "Sign up";
            button1.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(1, 109, 72);
            panel2.Location = new Point(120, 402);
            panel2.Name = "panel2";
            panel2.Size = new Size(250, 1);
            panel2.TabIndex = 16;
            // 
            // panelNom
            // 
            panelNom.BackColor = Color.FromArgb(1, 109, 72);
            panelNom.Location = new Point(120, 100);
            panelNom.Name = "panelNom";
            panelNom.Size = new Size(250, 1);
            panelNom.TabIndex = 30;
            // 
            // panelPrenom
            // 
            panelPrenom.BackColor = Color.FromArgb(1, 109, 72);
            panelPrenom.Location = new Point(120, 133);
            panelPrenom.Name = "panelPrenom";
            panelPrenom.Size = new Size(250, 1);
            panelPrenom.TabIndex = 31;
            // 
            // panelCin
            // 
            panelCin.BackColor = Color.FromArgb(1, 109, 72);
            panelCin.Location = new Point(120, 200);
            panelCin.Name = "panelCin";
            panelCin.Size = new Size(250, 1);
            panelCin.TabIndex = 32;
            // 
            // panelAdresse
            // 
            panelAdresse.BackColor = Color.FromArgb(1, 109, 72);
            panelAdresse.Location = new Point(120, 236);
            panelAdresse.Name = "panelAdresse";
            panelAdresse.Size = new Size(250, 1);
            panelAdresse.TabIndex = 33;
            // 
            // panelVille
            // 
            panelVille.BackColor = Color.FromArgb(1, 109, 72);
            panelVille.Location = new Point(120, 272);
            panelVille.Name = "panelVille";
            panelVille.Size = new Size(250, 1);
            panelVille.TabIndex = 34;
            // 
            // panelTelephone
            // 
            panelTelephone.BackColor = Color.FromArgb(1, 109, 72);
            panelTelephone.Location = new Point(120, 308);
            panelTelephone.Name = "panelTelephone";
            panelTelephone.Size = new Size(250, 1);
            panelTelephone.TabIndex = 35;
            // 
            // panelEmail
            // 
            panelEmail.BackColor = Color.FromArgb(1, 109, 72);
            panelEmail.Location = new Point(120, 340);
            panelEmail.Name = "panelEmail";
            panelEmail.Size = new Size(250, 1);
            panelEmail.TabIndex = 36;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 150);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(415, 214);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.InactiveCaption;
            panel3.Controls.Add(pictureBox1);
            panel3.Location = new Point(531, -2);
            panel3.Name = "panel3";
            panel3.Size = new Size(440, 530);
            panel3.TabIndex = 37;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(1, 109, 72);
            panel1.ForeColor = Color.FromArgb(1, 109, 72);
            panel1.Location = new Point(120, 372);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 1);
            panel1.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bauhaus 93", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(1, 109, 72);
            label1.Location = new Point(178, 20);
            label1.Name = "label1";
            label1.Size = new Size(123, 36);
            label1.TabIndex = 12;
            label1.Text = "Sign up";
            label1.Click += label1_Click;
            // 
            // Signup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(970, 528);
            Controls.Add(panel3);
            Controls.Add(panelEmail);
            Controls.Add(panelTelephone);
            Controls.Add(panelVille);
            Controls.Add(panelAdresse);
            Controls.Add(panelCin);
            Controls.Add(panelPrenom);
            Controls.Add(panelNom);
            Controls.Add(Email);
            Controls.Add(Telephone);
            Controls.Add(Ville);
            Controls.Add(Adresse);
            Controls.Add(CIN);
            Controls.Add(DateNaissance);
            Controls.Add(Prenom);
            Controls.Add(Nom);
            Controls.Add(Password);
            Controls.Add(Username);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Signup";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Password;
        private TextBox Username;
        private TextBox Nom;
        private TextBox Prenom;
        private TextBox CIN;
        private TextBox Adresse;
        private TextBox Ville;
        private TextBox Telephone;
        private TextBox Email;
        private DateTimePicker DateNaissance;
        private Label label3;
        private Label label2;
        private Button button1;
        private Panel panel2;
        private Panel panelNom;
        private Panel panelPrenom;
        private Panel panelCin;
        private Panel panelAdresse;
        private Panel panelVille;
        private Panel panelTelephone;
        private Panel panelEmail;
        private Panel panel3;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Label label1;
    }
}
