using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using paSSQL;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Inloggen : Form
    {

        public Inloggen()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(700, 400);
        }
        string cs = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            String U = txt_UserName.Text;
            U = Regex.Replace(U, @"\B[A-Z]", m => " " + m.ToString().ToLower());
            String P = txt_Password.Text;
            P = Regex.Replace(P, @"\B[A-Z]", m => " " + m.ToString().ToLower());
            {
                if (txt_UserName.Text == "" || txt_Password.Text == "")
                {
                    MessageBox.Show("Enter UserName and Password");
                    return;
                }             
                try
                {
                    SqlConnection connection = new SqlConnection(cs);
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("Select * from tbl_Login where UserName=@UserName COLLATE Latin1_General_CS_AS and Password=@Password COLLATE Latin1_General_CS_AS");
                    cmd.Parameters.AddWithValue("@UserName", txt_UserName.Text);
                    cmd.Parameters.AddWithValue("@Password", txt_Password.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapt.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;
                    if (count == 1)
                    {
                        this.Hide();
                        Temperatuur fm = new Temperatuur();
                        fm.Show();
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }

                try
                {
                    SqlConnection connection2 = new SqlConnection(cs);
                    SqlCommand command;
                    connection2.Open();
                    command = connection2.CreateCommand();
                    command.CommandText = ("SELECT (Rights) FROM tbl_Login WHERE UserName=@username AND Password=@password");
                    command.Parameters.AddWithValue("@username", txt_UserName.Text);
                    command.Parameters.AddWithValue("@password", txt_Password.Text);
                    command.ExecuteNonQuery();
                    SqlDataAdapter adapt2 = new SqlDataAdapter(command);
                    DataSet ds2 = new DataSet();
                    adapt2.Fill(ds2);
                    int rights = Convert.ToInt32(ds2.Tables[0].Rows[0][0]);
                    Rights.rights = rights;
                }
                catch (Exception)
                {
                    MessageBox.Show("Login mislukt");
                    txt_UserName.Clear();
                    txt_Password.Clear();
                    this.ActiveControl = txt_UserName;
                }

                try
                {
                    SqlConnection connection3 = new SqlConnection(cs);
                    SqlCommand cmd2;
                    connection3.Open();
                    cmd2 = connection3.CreateCommand();
                    cmd2.CommandText = ("SELECT (UserName) FROM tbl_Login WHERE UserName=@UserName");
                    cmd2.Parameters.AddWithValue("@UserName", txt_UserName.Text);
                    cmd2.ExecuteNonQuery();
                    SqlDataAdapter adapt3 = new SqlDataAdapter(cmd2);
                    DataSet ds3 = new DataSet();
                    adapt3.Fill(ds3);
                    string username = Convert.ToString(ds3.Tables[0].Rows[0][0]);
                    Rights.username = username;
                }
                catch (Exception /*E*/)
                {
                    //MessageBox.Show(E.Message);
                }                        
            }
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txt_UserName.Text))
            {
                Application.Exit();
            }
            else
            {
                txt_UserName.Clear();
                txt_Password.Clear();
                this.ActiveControl = txt_UserName;
            }
        }
    }
}