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
            Password.Location = new Point(171, 640);
            Password.Margin = new Padding(4, 5, 4, 5);
            Password.MaxLength = 50;
            Password.Name = "Password";
            Password.PlaceholderText = "Password";
            Password.Size = new Size(357, 24);
            Password.TabIndex = 21;
            Password.UseSystemPasswordChar = true;
            // 
            // Username
            // 
            Username.BorderStyle = BorderStyle.None;
            Username.Location = new Point(171, 597);
            Username.Margin = new Padding(4, 5, 4, 5);
            Username.MaxLength = 50;
            Username.Name = "Username";
            Username.PlaceholderText = "Username";
            Username.Size = new Size(357, 24);
            Username.TabIndex = 20;
            // 
            // Nom
            // 
            Nom.BorderStyle = BorderStyle.None;
            Nom.Location = new Point(171, 250);
            Nom.Margin = new Padding(4, 5, 4, 5);
            Nom.MaxLength = 100;
            Nom.Name = "Nom";
            Nom.PlaceholderText = "Nom";
            Nom.Size = new Size(357, 24);
            Nom.TabIndex = 22;
            // 
            // Prenom
            // 
            Prenom.BorderStyle = BorderStyle.None;
            Prenom.Location = new Point(171, 293);
            Prenom.Margin = new Padding(4, 5, 4, 5);
            Prenom.MaxLength = 100;
            Prenom.Name = "Prenom";
            Prenom.PlaceholderText = "Prénom";
            Prenom.Size = new Size(357, 24);
            Prenom.TabIndex = 23;
            // 
            // CIN
            // 
            CIN.BorderStyle = BorderStyle.None;
            CIN.Location = new Point(171, 380);
            CIN.Margin = new Padding(4, 5, 4, 5);
            CIN.MaxLength = 20;
            CIN.Name = "CIN";
            CIN.PlaceholderText = "CIN";
            CIN.Size = new Size(357, 24);
            CIN.TabIndex = 25;
            // 
            // Adresse
            // 
            Adresse.BorderStyle = BorderStyle.None;
            Adresse.Location = new Point(171, 423);
            Adresse.Margin = new Padding(4, 5, 4, 5);
            Adresse.MaxLength = 255;
            Adresse.Name = "Adresse";
            Adresse.PlaceholderText = "Adresse";
            Adresse.Size = new Size(357, 24);
            Adresse.TabIndex = 26;
            // 
            // Ville
            // 
            Ville.BorderStyle = BorderStyle.None;
            Ville.Location = new Point(171, 467);
            Ville.Margin = new Padding(4, 5, 4, 5);
            Ville.MaxLength = 100;
            Ville.Name = "Ville";
            Ville.PlaceholderText = "Ville";
            Ville.Size = new Size(357, 24);
            Ville.TabIndex = 27;
            // 
            // Telephone
            // 
            Telephone.BorderStyle = BorderStyle.None;
            Telephone.Location = new Point(171, 510);
            Telephone.Margin = new Padding(4, 5, 4, 5);
            Telephone.MaxLength = 20;
            Telephone.Name = "Telephone";
            Telephone.PlaceholderText = "Téléphone";
            Telephone.Size = new Size(357, 24);
            Telephone.TabIndex = 28;
            // 
            // Email
            // 
            Email.BorderStyle = BorderStyle.None;
            Email.Location = new Point(171, 553);
            Email.Margin = new Padding(4, 5, 4, 5);
            Email.MaxLength = 100;
            Email.Name = "Email";
            Email.PlaceholderText = "Email";
            Email.Size = new Size(357, 24);
            Email.TabIndex = 29;
            // 
            // DateNaissance
            // 
            DateNaissance.Format = DateTimePickerFormat.Short;
            DateNaissance.Location = new Point(171, 337);
            DateNaissance.Margin = new Padding(4, 5, 4, 5);
            DateNaissance.Name = "DateNaissance";
            DateNaissance.Size = new Size(355, 31);
            DateNaissance.TabIndex = 24;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Cursor = Cursors.Hand;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label3.Location = new Point(323, 833);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(52, 25);
            label3.TabIndex = 19;
            label3.Text = "Quit";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Cursor = Cursors.Hand;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label2.Location = new Point(323, 783);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(71, 25);
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
            button1.Location = new Point(171, 700);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(357, 60);
            button1.TabIndex = 17;
            button1.Text = "Sign up";
            button1.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(1, 109, 72);
            panel2.Location = new Point(171, 670);
            panel2.Margin = new Padding(4, 5, 4, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(357, 2);
            panel2.TabIndex = 16;
            // 
            // panelNom
            // 
            panelNom.BackColor = Color.FromArgb(1, 109, 72);
            panelNom.Location = new Point(171, 280);
            panelNom.Margin = new Padding(4, 5, 4, 5);
            panelNom.Name = "panelNom";
            panelNom.Size = new Size(357, 2);
            panelNom.TabIndex = 30;
            // 
            // panelPrenom
            // 
            panelPrenom.BackColor = Color.FromArgb(1, 109, 72);
            panelPrenom.Location = new Point(171, 323);
            panelPrenom.Margin = new Padding(4, 5, 4, 5);
            panelPrenom.Name = "panelPrenom";
            panelPrenom.Size = new Size(357, 2);
            panelPrenom.TabIndex = 31;
            // 
            // panelCin
            // 
            panelCin.BackColor = Color.FromArgb(1, 109, 72);
            panelCin.Location = new Point(171, 410);
            panelCin.Margin = new Padding(4, 5, 4, 5);
            panelCin.Name = "panelCin";
            panelCin.Size = new Size(357, 2);
            panelCin.TabIndex = 32;
            // 
            // panelAdresse
            // 
            panelAdresse.BackColor = Color.FromArgb(1, 109, 72);
            panelAdresse.Location = new Point(171, 453);
            panelAdresse.Margin = new Padding(4, 5, 4, 5);
            panelAdresse.Name = "panelAdresse";
            panelAdresse.Size = new Size(357, 2);
            panelAdresse.TabIndex = 33;
            // 
            // panelVille
            // 
            panelVille.BackColor = Color.FromArgb(1, 109, 72);
            panelVille.Location = new Point(171, 497);
            panelVille.Margin = new Padding(4, 5, 4, 5);
            panelVille.Name = "panelVille";
            panelVille.Size = new Size(357, 2);
            panelVille.TabIndex = 34;
            // 
            // panelTelephone
            // 
            panelTelephone.BackColor = Color.FromArgb(1, 109, 72);
            panelTelephone.Location = new Point(171, 540);
            panelTelephone.Margin = new Padding(4, 5, 4, 5);
            panelTelephone.Name = "panelTelephone";
            panelTelephone.Size = new Size(357, 2);
            panelTelephone.TabIndex = 35;
            // 
            // panelEmail
            // 
            panelEmail.BackColor = Color.FromArgb(1, 109, 72);
            panelEmail.Location = new Point(171, 583);
            panelEmail.Margin = new Padding(4, 5, 4, 5);
            panelEmail.Name = "panelEmail";
            panelEmail.Size = new Size(357, 2);
            panelEmail.TabIndex = 36;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(36, 245);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(593, 357);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.InactiveCaption;
            panel3.Controls.Add(pictureBox1);
            panel3.Location = new Point(759, -3);
            panel3.Margin = new Padding(4, 5, 4, 5);
            panel3.Name = "panel3";
            panel3.Size = new Size(646, 882);
            panel3.TabIndex = 37;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(1, 109, 72);
            panel1.ForeColor = Color.FromArgb(1, 109, 72);
            panel1.Location = new Point(171, 627);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(357, 2);
            panel1.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bauhaus 93", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(1, 109, 72);
            label1.Location = new Point(171, 150);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(184, 54);
            label1.TabIndex = 12;
            label1.Text = "Sign up";
            label1.Click += label1_Click;
            // 
            // Signup
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1404, 877);
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
            Margin = new Padding(4, 5, 4, 5);
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
