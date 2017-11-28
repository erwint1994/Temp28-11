namespace WindowsFormsApp1
{
    partial class GebruikerToevoegen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GebruikerToevoegen));
            this.BtnVerzenden = new System.Windows.Forms.Button();
            this.TxbEmail = new System.Windows.Forms.TextBox();
            this.TxbAchternaam = new System.Windows.Forms.TextBox();
            this.TxbTussenvoegsel = new System.Windows.Forms.TextBox();
            this.txbVoornaam = new System.Windows.Forms.TextBox();
            this.LblEmail = new System.Windows.Forms.Label();
            this.LblTussenvoegsel = new System.Windows.Forms.Label();
            this.LblAchternaam = new System.Windows.Forms.Label();
            this.LblVoornaam = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnVerzenden
            // 
            this.BtnVerzenden.Location = new System.Drawing.Point(297, 145);
            this.BtnVerzenden.Name = "BtnVerzenden";
            this.BtnVerzenden.Size = new System.Drawing.Size(75, 23);
            this.BtnVerzenden.TabIndex = 32;
            this.BtnVerzenden.Text = "Verzenden";
            this.BtnVerzenden.UseVisualStyleBackColor = true;
            this.BtnVerzenden.Click += new System.EventHandler(this.BtnVerzenden_Click);
            // 
            // TxbEmail
            // 
            this.TxbEmail.Location = new System.Drawing.Point(114, 109);
            this.TxbEmail.Name = "TxbEmail";
            this.TxbEmail.Size = new System.Drawing.Size(258, 20);
            this.TxbEmail.TabIndex = 30;
            // 
            // TxbAchternaam
            // 
            this.TxbAchternaam.Location = new System.Drawing.Point(114, 80);
            this.TxbAchternaam.Name = "TxbAchternaam";
            this.TxbAchternaam.Size = new System.Drawing.Size(258, 20);
            this.TxbAchternaam.TabIndex = 29;
            // 
            // TxbTussenvoegsel
            // 
            this.TxbTussenvoegsel.Location = new System.Drawing.Point(114, 49);
            this.TxbTussenvoegsel.Name = "TxbTussenvoegsel";
            this.TxbTussenvoegsel.Size = new System.Drawing.Size(258, 20);
            this.TxbTussenvoegsel.TabIndex = 28;
            // 
            // txbVoornaam
            // 
            this.txbVoornaam.Location = new System.Drawing.Point(114, 20);
            this.txbVoornaam.Name = "txbVoornaam";
            this.txbVoornaam.Size = new System.Drawing.Size(258, 20);
            this.txbVoornaam.TabIndex = 27;
            // 
            // LblEmail
            // 
            this.LblEmail.AutoSize = true;
            this.LblEmail.Location = new System.Drawing.Point(59, 112);
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Size = new System.Drawing.Size(42, 13);
            this.LblEmail.TabIndex = 25;
            this.LblEmail.Text = "Email* :";
            // 
            // LblTussenvoegsel
            // 
            this.LblTussenvoegsel.AutoSize = true;
            this.LblTussenvoegsel.Location = new System.Drawing.Point(15, 52);
            this.LblTussenvoegsel.Name = "LblTussenvoegsel";
            this.LblTussenvoegsel.Size = new System.Drawing.Size(85, 13);
            this.LblTussenvoegsel.TabIndex = 24;
            this.LblTussenvoegsel.Text = "Tussenvoegsel :";
            // 
            // LblAchternaam
            // 
            this.LblAchternaam.AutoSize = true;
            this.LblAchternaam.Location = new System.Drawing.Point(30, 83);
            this.LblAchternaam.Name = "LblAchternaam";
            this.LblAchternaam.Size = new System.Drawing.Size(74, 13);
            this.LblAchternaam.TabIndex = 23;
            this.LblAchternaam.Text = "Achternaam* :";
            // 
            // LblVoornaam
            // 
            this.LblVoornaam.AutoSize = true;
            this.LblVoornaam.Location = new System.Drawing.Point(39, 23);
            this.LblVoornaam.Name = "LblVoornaam";
            this.LblVoornaam.Size = new System.Drawing.Size(65, 13);
            this.LblVoornaam.TabIndex = 22;
            this.LblVoornaam.Text = "Voornaam* :";
            // 
            // GebruikerToevoegen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 181);
            this.Controls.Add(this.BtnVerzenden);
            this.Controls.Add(this.TxbEmail);
            this.Controls.Add(this.TxbAchternaam);
            this.Controls.Add(this.TxbTussenvoegsel);
            this.Controls.Add(this.txbVoornaam);
            this.Controls.Add(this.LblEmail);
            this.Controls.Add(this.LblTussenvoegsel);
            this.Controls.Add(this.LblAchternaam);
            this.Controls.Add(this.LblVoornaam);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GebruikerToevoegen";
            this.Text = "aanmaken";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GebruikerToevoegen_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnVerzenden;
        private System.Windows.Forms.TextBox TxbEmail;
        private System.Windows.Forms.TextBox TxbAchternaam;
        private System.Windows.Forms.TextBox TxbTussenvoegsel;
        private System.Windows.Forms.TextBox txbVoornaam;
        private System.Windows.Forms.Label LblEmail;
        private System.Windows.Forms.Label LblTussenvoegsel;
        private System.Windows.Forms.Label LblAchternaam;
        private System.Windows.Forms.Label LblVoornaam;
    }
}