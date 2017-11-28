using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using paSSQL;
using System.Text.RegularExpressions;
using log4net;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string cs = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            log.Info("BtnSubmit START");
            String U = txt_UserName.Text;
            U = Regex.Replace(U, @"\B[A-Z]", m => " " + m.ToString().ToLower());
            log.Info("Replace A-Z to lower (txt_username)");
            String P = txt_Password.Text;
            P = Regex.Replace(P, @"\B[A-Z]", m => " " + m.ToString().ToLower());
            log.Info("replace A-Z to lower (txt_password)");
            {
                if (txt_UserName.Text == "" || txt_Password.Text == "")
                {
                    MessageBox.Show("Enter UserName and Password");
                    log.Error("Enter username and password");
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
                    log.Info("Username and password are correct");
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    log.Error(E);
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
                    log.Info("Selected rights for user");
                }
                catch (Exception E)
                {
                    MessageBox.Show("Login mislukt");
                    log.Error(E);
                    txt_UserName.Clear();
                    txt_Password.Clear();
                    this.ActiveControl = txt_UserName;
                    log.Info("Fail to login, reset username and password, active control to txt_username");
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
                    log.Info("Username saved in class Rights");
                }
                catch (Exception /*E*/)
                {
                    //MessageBox.Show(E.Message);
                }                        
            }
            log.Info("BtnSubmit STOP");
            log.Info(" ");
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txt_UserName.Text))
            {
                log.Info("Application Exit");
                Application.Exit();
            }
            else
            {
                txt_UserName.Clear();
                log.Info("Clear txt_username");
                txt_Password.Clear();
                log.Info("Clear txt_password");
                this.ActiveControl = txt_UserName;
                log.Info("Active control to txt_username");
            }
        }
    }
}