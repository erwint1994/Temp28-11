namespace WindowsFormsApp1
{
    partial class Links
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Links));
            this.btnWebsiteLink = new System.Windows.Forms.Button();
            this.btnMobieleApplicatieLink = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWebsiteLink
            // 
            this.btnWebsiteLink.Location = new System.Drawing.Point(45, 46);
            this.btnWebsiteLink.Name = "btnWebsiteLink";
            this.btnWebsiteLink.Size = new System.Drawing.Size(172, 152);
            this.btnWebsiteLink.TabIndex = 0;
            this.btnWebsiteLink.Text = "Website";
            this.btnWebsiteLink.UseVisualStyleBackColor = true;
            this.btnWebsiteLink.Click += new System.EventHandler(this.btnWebsiteLink_Click);
            // 
            // btnMobieleApplicatieLink
            // 
            this.btnMobieleApplicatieLink.Location = new System.Drawing.Point(293, 46);
            this.btnMobieleApplicatieLink.Name = "btnMobieleApplicatieLink";
            this.btnMobieleApplicatieLink.Size = new System.Drawing.Size(172, 152);
            this.btnMobieleApplicatieLink.TabIndex = 1;
            this.btnMobieleApplicatieLink.Text = "Mobiele Applicatie";
            this.btnMobieleApplicatieLink.UseVisualStyleBackColor = true;
            this.btnMobieleApplicatieLink.Click += new System.EventHandler(this.btnMobieleApplicatieLink_Click);
            // 
            // Links
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 249);
            this.Controls.Add(this.btnMobieleApplicatieLink);
            this.Controls.Add(this.btnWebsiteLink);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Links";
            this.Text = "Links";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWebsiteLink;
        private System.Windows.Forms.Button btnMobieleApplicatieLink;
    }
}