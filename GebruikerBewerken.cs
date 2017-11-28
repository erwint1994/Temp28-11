using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using log4net;
namespace WindowsFormsApp1
{
    public partial class GebruikerBewerken : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        int id = Rights.id;
        string MyConnectionString2 = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public GebruikerBewerken()
        {
            InitializeComponent();
        }
        private void BtnVerzenden_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                SqlCommand cmd;
                connection.Open();
                try
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("UPDATE tbl_EmailAdress SET Email=@email, Voornaam=@voornaam, Achternaam=@achternaam, Tussenvoegsel=@tussenvoegsel WHERE id=@id");
                    cmd.Parameters.AddWithValue("@email", TxbEmail.Text);
                    cmd.Parameters.AddWithValue("@voornaam", txbVoornaam.Text);
                    cmd.Parameters.AddWithValue("@achternaam", TxbAchternaam.Text);
                    cmd.Parameters.AddWithValue("@tussenvoegsel", TxbTussenvoegsel.Text);
                    cmd.Parameters.AddWithValue("@id", TxbId.Text);
                    cmd.ExecuteNonQuery();
                    log.Info("UPDATE user");
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    log.Error("Can't UPDATE user", E);
                }
                connection.Close();
                this.Close();
                log.Info("Close GebruikerBewerken.cs");
            }             
        }
        private void Edit_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("GebruikerBewerken.cs closed");
            Gebruiker form2 = new Gebruiker();
            log.Info("Opens Gebruiker.cs");
            form2.Show();
        }
        private void GebruikerBewerken_Load(object sender, EventArgs e)
        {
            log.Info("GebruikerBewerken load START");
            using (SqlConnection connection2 = new SqlConnection(MyConnectionString2))
            {
                SqlCommand command;
                connection2.Open();
                try
                {
                    command = connection2.CreateCommand();
                    command.CommandText = "SELECT * FROM tbl_EmailAdress WHERE Id=@Id";
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataAdapter adap = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);
                        string Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                        string voornaam = Convert.ToString(ds.Tables[0].Rows[0]["voornaam"]);
                        string Achternaam = Convert.ToString(ds.Tables[0].Rows[0]["Achternaam"]);
                        string Tussenvoegsel = Convert.ToString(ds.Tables[0].Rows[0]["Tussenvoegsel"]);
                        TxbEmail.Text = Email;
                        txbVoornaam.Text = voornaam;
                        TxbAchternaam.Text = Achternaam;
                        TxbTussenvoegsel.Text = Tussenvoegsel;
                        TxbId.Text = Convert.ToString(id);
                        log.Info("SELECT * FROM tbl_EmailAdres");
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("Error gebruiker(s) opvragen.");
                    log.Error("SELECT * FROM tbl_EmailAdres", E);
                }
            }
            log.Info("GebruikerBewerken load STOP");
        }
    }
}
