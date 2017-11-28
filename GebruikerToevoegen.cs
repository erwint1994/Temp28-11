using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Vertalen;

namespace WindowsFormsApp1
{
    public partial class GebruikerToevoegen : Form
    {
        string MyConnectionString2 = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public Temperatuur Parentform1 { get; set; }
        public GebruikerToevoegen()
        {
            InitializeComponent();
          
        }
        public void BtnVerzenden_Click(object sender, EventArgs e)
        {
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
                }
                catch (Exception)
                {
                    throw;
                }
                connection.Close();
                Close();
            }          
        }

        private void GebruikerToevoegen_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void GebruikerToevoegen_Load(object sender, EventArgs e)
        {
            if (Parentform1.Engels == true)
            {
                Vertaal.VertaalControlsEN(this, "EN");
            }

            if (Parentform1.Duits == true)
            {
                Vertaal.VertaalControlsDE(this, "DE");
            }

            if (Parentform1.Nederlands == true)
            {
                Vertaal.VertaalControlsNL(this, "NL");
            }
        }
    }
}
