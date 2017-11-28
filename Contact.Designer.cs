namespace WindowsFormsApp1
{
    partial class Contact
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Contact));
            this.BtnVerzenden = new System.Windows.Forms.Button();
            this.LblVoornaam = new System.Windows.Forms.Label();
            this.LblTussenVoegsel = new System.Windows.Forms.Label();
            this.LblAchternaam = new System.Windows.Forms.Label();
            this.Bedrijf = new System.Windows.Forms.Label();
            this.lblTelefoonNummer = new System.Windows.Forms.Label();
            this.LblBericht = new System.Windows.Forms.Label();
            this.TxbBedrijf = new System.Windows.Forms.TextBox();
            this.txbVoornaam = new System.Windows.Forms.TextBox();
            this.TxbTussenvoegsel = new System.Windows.Forms.TextBox();
            this.TxbTelefoonnummer = new System.Windows.Forms.TextBox();
            this.TxbBericht = new System.Windows.Forms.TextBox();
            this.TxbAchternaam = new System.Windows.Forms.TextBox();
            this.LblEmail = new System.Windows.Forms.Label();
            this.TxbEmail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtnVerzenden
            // 
            this.BtnVerzenden.Location = new System.Drawing.Point(100, 423);
            this.BtnVerzenden.Name = "BtnVerzenden";
            this.BtnVerzenden.Size = new System.Drawing.Size(75, 23);
            this.BtnVerzenden.TabIndex = 8;
            this.BtnVerzenden.Text = "Verzenden";
            this.BtnVerzenden.UseVisualStyleBackColor = true;
            this.BtnVerzenden.Click += new System.EventHandler(this.BtnVerzenden_Click_1);
            // 
            // LblVoornaam
            // 
            this.LblVoornaam.AutoSize = true;
            this.LblVoornaam.Location = new System.Drawing.Point(12, 25);
            this.LblVoornaam.Name = "LblVoornaam";
            this.LblVoornaam.Size = new System.Drawing.Size(55, 13);
            this.LblVoornaam.TabIndex = 1;
            this.LblVoornaam.Text = "Voornaam";
            // 
            // LblTussenVoegsel
            // 
            this.LblTussenVoegsel.AutoSize = true;
            this.LblTussenVoegsel.Location = new System.Drawing.Point(12, 50);
            this.LblTussenVoegsel.Name = "LblTussenVoegsel";
            this.LblTussenVoegsel.Size = new System.Drawing.Size(79, 13);
            this.LblTussenVoegsel.TabIndex = 2;
            this.LblTussenVoegsel.Text = "Tussenvoegsel";
            // 
            // LblAchternaam
            // 
            this.LblAchternaam.AutoSize = true;
            this.LblAchternaam.Location = new System.Drawing.Point(12, 75);
            this.LblAchternaam.Name = "LblAchternaam";
            this.LblAchternaam.Size = new System.Drawing.Size(64, 13);
            this.LblAchternaam.TabIndex = 3;
            this.LblAchternaam.Text = "Achternaam";
            // 
            // Bedrijf
            // 
            this.Bedrijf.AutoSize = true;
            this.Bedrijf.Location = new System.Drawing.Point(12, 100);
            this.Bedrijf.Name = "Bedrijf";
            this.Bedrijf.Size = new System.Drawing.Size(36, 13);
            this.Bedrijf.TabIndex = 4;
            this.Bedrijf.Text = "Bedrijf";
            // 
            // lblTelefoonNummer
            // 
            this.lblTelefoonNummer.AutoSize = true;
            this.lblTelefoonNummer.Location = new System.Drawing.Point(12, 125);
            this.lblTelefoonNummer.Name = "lblTelefoonNummer";
            this.lblTelefoonNummer.Size = new System.Drawing.Size(86, 13);
            this.lblTelefoonNummer.TabIndex = 5;
            this.lblTelefoonNummer.Text = "Telefoonnummer";
            // 
            // LblBericht
            // 
            this.LblBericht.AutoSize = true;
            this.LblBericht.Location = new System.Drawing.Point(12, 178);
            this.LblBericht.Name = "LblBericht";
            this.LblBericht.Size = new System.Drawing.Size(40, 13);
            this.LblBericht.TabIndex = 6;
            this.LblBericht.Text = "Bericht";
            // 
            // TxbBedrijf
            // 
            this.TxbBedrijf.AcceptsTab = true;
            this.TxbBedrijf.Location = new System.Drawing.Point(100, 100);
            this.TxbBedrijf.Name = "TxbBedrijf";
            this.TxbBedrijf.Size = new System.Drawing.Size(197, 20);
            this.TxbBedrijf.TabIndex = 4;
            // 
            // txbVoornaam
            // 
            this.txbVoornaam.AcceptsTab = true;
            this.txbVoornaam.Location = new System.Drawing.Point(100, 25);
            this.txbVoornaam.Name = "txbVoornaam";
            this.txbVoornaam.Size = new System.Drawing.Size(197, 20);
            this.txbVoornaam.TabIndex = 1;
            // 
            // TxbTussenvoegsel
            // 
            this.TxbTussenvoegsel.AcceptsTab = true;
            this.TxbTussenvoegsel.Location = new System.Drawing.Point(100, 50);
            this.TxbTussenvoegsel.Name = "TxbTussenvoegsel";
            this.TxbTussenvoegsel.Size = new System.Drawing.Size(197, 20);
            this.TxbTussenvoegsel.TabIndex = 2;
            // 
            // TxbTelefoonnummer
            // 
            this.TxbTelefoonnummer.AcceptsTab = true;
            this.TxbTelefoonnummer.Location = new System.Drawing.Point(100, 125);
            this.TxbTelefoonnummer.Name = "TxbTelefoonnummer";
            this.TxbTelefoonnummer.Size = new System.Drawing.Size(197, 20);
            this.TxbTelefoonnummer.TabIndex = 5;
            // 
            // TxbBericht
            // 
            this.TxbBericht.AcceptsTab = true;
            this.TxbBericht.Location = new System.Drawing.Point(100, 178);
            this.TxbBericht.Multiline = true;
            this.TxbBericht.Name = "TxbBericht";
            this.TxbBericht.Size = new System.Drawing.Size(330, 239);
            this.TxbBericht.TabIndex = 7;
            // 
            // TxbAchternaam
            // 
            this.TxbAchternaam.AcceptsTab = true;
            this.TxbAchternaam.Location = new System.Drawing.Point(100, 75);
            this.TxbAchternaam.Name = "TxbAchternaam";
            this.TxbAchternaam.Size = new System.Drawing.Size(197, 20);
            this.TxbAchternaam.TabIndex = 3;
            // 
            // LblEmail
            // 
            this.LblEmail.AutoSize = true;
            this.LblEmail.Location = new System.Drawing.Point(12, 152);
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Size = new System.Drawing.Size(32, 13);
            this.LblEmail.TabIndex = 15;
            this.LblEmail.Text = "Email";
            // 
            // TxbEmail
            // 
            this.TxbEmail.AcceptsTab = true;
            this.TxbEmail.Location = new System.Drawing.Point(100, 151);
            this.TxbEmail.Name = "TxbEmail";
            this.TxbEmail.Size = new System.Drawing.Size(197, 20);
            this.TxbEmail.TabIndex = 6;
            // 
            // Contact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 458);
            this.Controls.Add(this.TxbEmail);
            this.Controls.Add(this.LblEmail);
            this.Controls.Add(this.TxbAchternaam);
            this.Controls.Add(this.TxbBericht);
            this.Controls.Add(this.TxbTelefoonnummer);
            this.Controls.Add(this.TxbTussenvoegsel);
            this.Controls.Add(this.txbVoornaam);
            this.Controls.Add(this.TxbBedrijf);
            this.Controls.Add(this.LblBericht);
            this.Controls.Add(this.lblTelefoonNummer);
            this.Controls.Add(this.Bedrijf);
            this.Controls.Add(this.LblAchternaam);
            this.Controls.Add(this.LblTussenVoegsel);
            this.Controls.Add(this.LblVoornaam);
            this.Controls.Add(this.BtnVerzenden);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Contact";
            this.Text = "ContactFormulier";
            this.Load += new System.EventHandler(this.Contact_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnVerzenden;
        private System.Windows.Forms.Label LblVoornaam;
        private System.Windows.Forms.Label LblTussenVoegsel;
        private System.Windows.Forms.Label LblAchternaam;
        private System.Windows.Forms.Label Bedrijf;
        private System.Windows.Forms.Label lblTelefoonNummer;
        private System.Windows.Forms.Label LblBericht;
        private System.Windows.Forms.TextBox TxbBedrijf;
        private System.Windows.Forms.TextBox txbVoornaam;
        private System.Windows.Forms.TextBox TxbTussenvoegsel;
        private System.Windows.Forms.TextBox TxbTelefoonnummer;
        private System.Windows.Forms.TextBox TxbBericht;
        private System.Windows.Forms.TextBox TxbAchternaam;
        private System.Windows.Forms.Label LblEmail;
        private System.Windows.Forms.TextBox TxbEmail;
    }
}