using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using log4net;
namespace WindowsFormsApp1
{
    public partial class GebruikerToevoegen : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string MyConnectionString2 = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public GebruikerToevoegen()
        {
            InitializeComponent();
        }
        public void BtnVerzenden_Click(object sender, EventArgs e)
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
                    cmd.CommandText = ("INSERT INTO tbl_EmailAdress (Email, Voornaam, Achternaam, Tussenvoegsel, Status) VALUES (@email,@voornaam,@achternaam, @tussenvoegsel, @Status);");
                    cmd.Parameters.AddWithValue("@Status", "Ingeschakeld");
                    cmd.Parameters.AddWithValue("@email", TxbEmail.Text);
                    cmd.Parameters.AddWithValue("@voornaam", txbVoornaam.Text);
                    cmd.Parameters.AddWithValue("@achternaam", TxbAchternaam.Text);
                    cmd.Parameters.AddWithValue("@tussenvoegsel", TxbTussenvoegsel.Text);
                    cmd.ExecuteNonQuery();
                    log.Info("INSERT INTO tbl_EmailAdress");
                }
                catch (Exception E)
                {
                    log.Error("INSERT INTO tbl_EmailAdress", E);
                }
                connection.Close();
                log.Info("Close GebruikerToevoegen.cs");
                this.Close();
            }          
        }

        private void GebruikerToevoegen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Gebruiker form2 = new Gebruiker();
            form2.Show();
            log.Info("Open Gebruiker.cs");
        }
    }
}
