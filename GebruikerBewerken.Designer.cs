namespace WindowsFormsApp1
{
    partial class GebruikerBewerken
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GebruikerBewerken));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbVoornaam = new System.Windows.Forms.TextBox();
            this.TxbTussenvoegsel = new System.Windows.Forms.TextBox();
            this.TxbAchternaam = new System.Windows.Forms.TextBox();
            this.TxbEmail = new System.Windows.Forms.TextBox();
            this.BtnVerzenden = new System.Windows.Forms.Button();
            this.TxbId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Voornaam* :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Achternaam*  :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tussenvoegsel :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Email*  : ";
            // 
            // txbVoornaam
            // 
            this.txbVoornaam.Location = new System.Drawing.Point(101, 32);
            this.txbVoornaam.Name = "txbVoornaam";
            this.txbVoornaam.Size = new System.Drawing.Size(258, 20);
            this.txbVoornaam.TabIndex = 5;
            // 
            // TxbTussenvoegsel
            // 
            this.TxbTussenvoegsel.Location = new System.Drawing.Point(101, 61);
            this.TxbTussenvoegsel.Name = "TxbTussenvoegsel";
            this.TxbTussenvoegsel.Size = new System.Drawing.Size(258, 20);
            this.TxbTussenvoegsel.TabIndex = 6;
            // 
            // TxbAchternaam
            // 
            this.TxbAchternaam.Location = new System.Drawing.Point(101, 92);
            this.TxbAchternaam.Name = "TxbAchternaam";
            this.TxbAchternaam.Size = new System.Drawing.Size(258, 20);
            this.TxbAchternaam.TabIndex = 7;
            // 
            // TxbEmail
            // 
            this.TxbEmail.Location = new System.Drawing.Point(101, 121);
            this.TxbEmail.Name = "TxbEmail";
            this.TxbEmail.Size = new System.Drawing.Size(258, 20);
            this.TxbEmail.TabIndex = 8;
            // 
            // BtnVerzenden
            // 
            this.BtnVerzenden.Location = new System.Drawing.Point(284, 147);
            this.BtnVerzenden.Name = "BtnVerzenden";
            this.BtnVerzenden.Size = new System.Drawing.Size(75, 23);
            this.BtnVerzenden.TabIndex = 10;
            this.BtnVerzenden.Text = "Verzenden";
            this.BtnVerzenden.UseVisualStyleBackColor = true;
            this.BtnVerzenden.Click += new System.EventHandler(this.BtnVerzenden_Click);
            // 
            // TxbId
            // 
            this.TxbId.Location = new System.Drawing.Point(101, 6);
            this.TxbId.Name = "TxbId";
            this.TxbId.ReadOnly = true;
            this.TxbId.Size = new System.Drawing.Size(258, 20);
            this.TxbId.TabIndex = 11;
            this.TxbId.Visible = false;
            // 
            // GebruikerBewerken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 180);
            this.Controls.Add(this.TxbId);
            this.Controls.Add(this.BtnVerzenden);
            this.Controls.Add(this.TxbEmail);
            this.Controls.Add(this.TxbAchternaam);
            this.Controls.Add(this.TxbTussenvoegsel);
            this.Controls.Add(this.txbVoornaam);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GebruikerBewerken";
            this.Text = "Bewerken";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Edit_FormClosed);
            this.Load += new System.EventHandler(this.GebruikerBewerken_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbVoornaam;
        private System.Windows.Forms.TextBox TxbTussenvoegsel;
        private System.Windows.Forms.TextBox TxbAchternaam;
        private System.Windows.Forms.TextBox TxbEmail;
        private System.Windows.Forms.Button BtnVerzenden;
        private System.Windows.Forms.TextBox TxbId;
    }
}