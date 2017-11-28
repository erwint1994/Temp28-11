using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
namespace WindowsFormsApp1
{
    public partial class GebruikerWFToevoegen : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string MyConnectionString2 = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public GebruikerWFToevoegen()
        {
            InitializeComponent();
        }
        private void BtnVerzenden_Click_1(object sender, EventArgs e)
        {
            log.Info("BtnVerzenden clicked");
            if (String.IsNullOrEmpty(txbVoornaam.Text))
            {
                MessageBox.Show("Voornaam niet ingevuld.");
            }
            else if (String.IsNullOrEmpty(TxbAchternaam.Text))
            {
                MessageBox.Show("Achternaam is niet ingevuld.");
            }
            else if (String.IsNullOrEmpty(TxbEmail.Text) || !Regex.IsMatch(TxbEmail.Text, @"([a-z@.\-]+)"))
            {
                MessageBox.Show("Geen geldig email adres.");
            }
            else
            {
                SqlConnection connection = new SqlConnection(MyConnectionString2);
                SqlCommand cmd;
                connection.Open();
                try
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("INSERT INTO tbl_EmailAdressWFapp (Email, Voornaam, Achternaam, Tussenvoegsel, Status) VALUES (@email,@voornaam,@achternaam, @tussenvoegsel, @Status);");
                    cmd.Parameters.AddWithValue("@Status", "Ingeschakeld");
                    cmd.Parameters.AddWithValue("@email", TxbEmail.Text);
                    cmd.Parameters.AddWithValue("@voornaam", txbVoornaam.Text);
                    cmd.Parameters.AddWithValue("@achternaam", TxbAchternaam.Text);
                    cmd.Parameters.AddWithValue("@tussenvoegsel", TxbTussenvoegsel.Text);
                    cmd.ExecuteNonQuery();
                    log.Info("INSERT INTO tbl_EmailAdressWFapp");
                }
                catch (Exception E)
                {
                    log.Error("INSERT INTO tbl_EmailAdressWFapp", E);
                }
                connection.Close();
                log.Info("Open Gebruiker.cs");
                this.Close();
            }
        }

        private void GebruikerWFToevoegen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Gebruiker form2 = new Gebruiker();
            form2.Show();
            log.Info("Open GebruikerBeheren.cs");
        }
    }
}

