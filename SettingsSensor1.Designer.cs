﻿namespace WindowsFormsApp1
{
    partial class SettingsSensor1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsSensor1));
            this.TbLocatie = new System.Windows.Forms.TextBox();
            this.LblLocatieSensor = new System.Windows.Forms.Label();
            this.BtnLocatieSensorOpslaan = new System.Windows.Forms.Button();
            this.TbMinimumTemperatuurCelsius = new System.Windows.Forms.TextBox();
            this.TbMaximumTemperatuurCelsius = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TbMaximumtemperatuurKelvin = new System.Windows.Forms.TextBox();
            this.TbMinimumtemperatuurKelvin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TbMaximumtemperatuurFarhenheid = new System.Windows.Forms.TextBox();
            this.TbMinimumtemperatuurFarhenheid = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TbLocatie
            // 
            this.TbLocatie.BackColor = System.Drawing.SystemColors.Window;
            this.TbLocatie.Location = new System.Drawing.Point(140, 6);
            this.TbLocatie.MaxLength = 30;
            this.TbLocatie.Name = "TbLocatie";
            this.TbLocatie.Size = new System.Drawing.Size(142, 20);
            this.TbLocatie.TabIndex = 146;
            // 
            // LblLocatieSensor
            // 
            this.LblLocatieSensor.AutoSize = true;
            this.LblLocatieSensor.Location = new System.Drawing.Point(12, 9);
            this.LblLocatieSensor.Name = "LblLocatieSensor";
            this.LblLocatieSensor.Size = new System.Drawing.Size(85, 13);
            this.LblLocatieSensor.TabIndex = 145;
            this.LblLocatieSensor.Text = "Locatie wijzigen:";
            // 
            // BtnLocatieSensorOpslaan
            // 
            this.BtnLocatieSensorOpslaan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnLocatieSensorOpslaan.Location = new System.Drawing.Point(209, 243);
            this.BtnLocatieSensorOpslaan.Name = "BtnLocatieSensorOpslaan";
            this.BtnLocatieSensorOpslaan.Size = new System.Drawing.Size(73, 23);
            this.BtnLocatieSensorOpslaan.TabIndex = 144;
            this.BtnLocatieSensorOpslaan.Text = "Opslaan";
            this.BtnLocatieSensorOpslaan.UseVisualStyleBackColor = false;
            this.BtnLocatieSensorOpslaan.Click += new System.EventHandler(this.BtnLocatieSensorOpslaan_Click);
            // 
            // TbMinimumTemperatuurCelsius
            // 
            this.TbMinimumTemperatuurCelsius.BackColor = System.Drawing.SystemColors.Window;
            this.TbMinimumTemperatuurCelsius.Location = new System.Drawing.Point(140, 42);
            this.TbMinimumTemperatuurCelsius.MaxLength = 4;
            this.TbMinimumTemperatuurCelsius.Name = "TbMinimumTemperatuurCelsius";
            this.TbMinimumTemperatuurCelsius.Size = new System.Drawing.Size(142, 20);
            this.TbMinimumTemperatuurCelsius.TabIndex = 149;
            // 
            // TbMaximumTemperatuurCelsius
            // 
            this.TbMaximumTemperatuurCelsius.BackColor = System.Drawing.SystemColors.Window;
            this.TbMaximumTemperatuurCelsius.Location = new System.Drawing.Point(140, 74);
            this.TbMaximumTemperatuurCelsius.MaxLength = 4;
            this.TbMaximumTemperatuurCelsius.Name = "TbMaximumTemperatuurCelsius";
            this.TbMaximumTemperatuurCelsius.Size = new System.Drawing.Size(142, 20);
            this.TbMaximumTemperatuurCelsius.TabIndex = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 151;
            this.label1.Text = "Minimum Temperatuur C:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 152;
            this.label3.Text = "Maximum Temperatuur C:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 156;
            this.label4.Text = "Maximum Temperatuur K:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 155;
            this.label5.Text = "Minimum Temperatuur K:";
            // 
            // TbMaximumtemperatuurKelvin
            // 
            this.TbMaximumtemperatuurKelvin.BackColor = System.Drawing.SystemColors.Window;
            this.TbMaximumtemperatuurKelvin.Location = new System.Drawing.Point(140, 142);
            this.TbMaximumtemperatuurKelvin.MaxLength = 4;
            this.TbMaximumtemperatuurKelvin.Name = "TbMaximumtemperatuurKelvin";
            this.TbMaximumtemperatuurKelvin.Size = new System.Drawing.Size(142, 20);
            this.TbMaximumtemperatuurKelvin.TabIndex = 154;
            // 
            // TbMinimumtemperatuurKelvin
            // 
            this.TbMinimumtemperatuurKelvin.BackColor = System.Drawing.SystemColors.Window;
            this.TbMinimumtemperatuurKelvin.Location = new System.Drawing.Point(140, 110);
            this.TbMinimumtemperatuurKelvin.MaxLength = 4;
            this.TbMinimumtemperatuurKelvin.Name = "TbMinimumtemperatuurKelvin";
            this.TbMinimumtemperatuurKelvin.Size = new System.Drawing.Size(142, 20);
            this.TbMinimumtemperatuurKelvin.TabIndex = 153;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 160;
            this.label6.Text = "Maximum Temperatuur F:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 13);
            this.label7.TabIndex = 159;
            this.label7.Text = "Minimum Temperatuur F:";
            // 
            // TbMaximumtemperatuurFarhenheid
            // 
            this.TbMaximumtemperatuurFarhenheid.BackColor = System.Drawing.SystemColors.Window;
            this.TbMaximumtemperatuurFarhenheid.Location = new System.Drawing.Point(140, 207);
            this.TbMaximumtemperatuurFarhenheid.MaxLength = 4;
            this.TbMaximumtemperatuurFarhenheid.Name = "TbMaximumtemperatuurFarhenheid";
            this.TbMaximumtemperatuurFarhenheid.Size = new System.Drawing.Size(142, 20);
            this.TbMaximumtemperatuurFarhenheid.TabIndex = 158;
            // 
            // TbMinimumtemperatuurFarhenheid
            // 
            this.TbMinimumtemperatuurFarhenheid.BackColor = System.Drawing.SystemColors.Window;
            this.TbMinimumtemperatuurFarhenheid.Location = new System.Drawing.Point(140, 175);
            this.TbMinimumtemperatuurFarhenheid.MaxLength = 4;
            this.TbMinimumtemperatuurFarhenheid.Name = "TbMinimumtemperatuurFarhenheid";
            this.TbMinimumtemperatuurFarhenheid.Size = new System.Drawing.Size(142, 20);
            this.TbMinimumtemperatuurFarhenheid.TabIndex = 157;
            // 
            // SettingsSensor1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 273);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TbMaximumtemperatuurFarhenheid);
            this.Controls.Add(this.TbMinimumtemperatuurFarhenheid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TbMaximumtemperatuurKelvin);
            this.Controls.Add(this.TbMinimumtemperatuurKelvin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TbMaximumTemperatuurCelsius);
            this.Controls.Add(this.TbMinimumTemperatuurCelsius);
            this.Controls.Add(this.TbLocatie);
            this.Controls.Add(this.LblLocatieSensor);
            this.Controls.Add(this.BtnLocatieSensorOpslaan);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsSensor1";
            this.Text = "SettingsSensor1";
            this.Load += new System.EventHandler(this.SettingsSensor1_Load);
            this.Shown += new System.EventHandler(this.SettingsSensor1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TbLocatie;
        private System.Windows.Forms.Label LblLocatieSensor;
        private System.Windows.Forms.Button BtnLocatieSensorOpslaan;
        private System.Windows.Forms.TextBox TbMinimumTemperatuurCelsius;
        private System.Windows.Forms.TextBox TbMaximumTemperatuurCelsius;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbMaximumtemperatuurKelvin;
        private System.Windows.Forms.TextBox TbMinimumtemperatuurKelvin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TbMaximumtemperatuurFarhenheid;
        private System.Windows.Forms.TextBox TbMinimumtemperatuurFarhenheid;
    }
}