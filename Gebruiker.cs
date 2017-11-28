using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using paSSQL;
using log4net;
namespace WindowsFormsApp1
{
    public partial class Gebruiker : Form
    {
        string MyConnectionString2 = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        private int selectedCellRow = 0;
        private int selectedCellColumn = 0;
        public new Gebruiker ParentForm { get; set; }
        public Gebruiker ParentForm2 { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // load
        public Gebruiker()
        {
            InitializeComponent();
        }
        public void AddMailUsers_Load(object sender, EventArgs e)
        {
            log.Info("Load Gebruiker.cs");
            SelectUsers();
            log.Info("SelectUsers (F)");
            SelectUsersWFApp();
            log.Info("SelectUsersWFApp (F)");
        }
        // buttons mail van service
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            log.Info("BtnAdd clicked, open gebruikerToevoegen.cs");
            GebruikerToevoegen gebruikersToevoegen = new GebruikerToevoegen();
            gebruikersToevoegen.Show();
            gebruikersToevoegen.Location = new Point(100, 100);
            if (ParentForm2 != null)
                ParentForm2.Refresh();
            Close();
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            log.Info("BtnDelete clicked");
            DeleteRecord();
            log.Info("DeleteRecord (F)");         
            SelectUsers();
            log.Info("SelectUsers (F)");
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            log.Info("BtnEdit clicked");
            GetId();
            log.Info("getId (F)");
            GebruikerBewerken gebruikerbewerken= new GebruikerBewerken();
            gebruikerbewerken.Show();
            log.Info("Open GebruikersBewerken.cs");
            if (ParentForm2 != null)
                ParentForm2.Refresh();
            Close();
        }
        private void BtnUnban_Click(object sender, EventArgs e)
        {
            log.Info("BtnUnban clicked");
            UnBanAccountMail();
            log.Info("UnBanAccountMail (F)");
            SelectUsers();
            log.Info("SelectUsers (F)");
        }
        private void BtnBan_Click(object sender, EventArgs e)
        {
            log.Info("BtnBan clicked");
            BanAccountMail();
            log.Info("BanAccountMail (F)");
            SelectUsers();
            log.Info("SelectUsers (F)");
        }
        // query (mail service)
        public void SelectUsers()
        {
            DtgMailMinMax.DataSource = SQL.GetSQLDataView("SELECT Id, Email, Voornaam, Tussenvoegsel, Achternaam, Status FROM tbl_EmailAdress");
            DtgMailMinMax.Columns[0].Visible = false;
            DtgMailMinMax.Columns[1].Width = 240;
            DtgMailMinMax.Columns[2].Width = 230;
            DtgMailMinMax.Columns[3].Width = 100;
            DtgMailMinMax.Columns[4].Width = 235;
            DtgMailMinMax.Columns[5].Width = 100;
            log.Info("SELECT * FROM tbl_Emailadress");
        }
        public void DeleteRecord()
        {
            using (SqlConnection connection2 = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    if (DtgMailMinMax.SelectedRows.Count > 0)
                    {
                        int selectedIndex = DtgMailMinMax.SelectedRows[0].Index;
                        int rowID = int.Parse(DtgMailMinMax[0, selectedIndex].Value.ToString());
                        string sql = "DELETE FROM tbl_EmailAdress WHERE id=@RowID";
                        SqlCommand deleteRecord = new SqlCommand
                        {
                            Connection = connection2,
                            CommandType = CommandType.Text,
                            CommandText = sql
                        };
                        SqlParameter RowParameter = new SqlParameter
                        {
                            ParameterName = "@RowID",
                            SqlDbType = SqlDbType.Int,
                            IsNullable = false,
                            Value = rowID
                        };
                        deleteRecord.Parameters.Add(RowParameter);
                        deleteRecord.Connection.Open();
                        deleteRecord.ExecuteNonQuery();
                        deleteRecord.Connection.Close();
                    }
                    log.Info("DELETE FROM tbl_EmailAdress");
                }
                catch (Exception E)
                {
                    log.Error("ERROR DELETE FROM tbl_EmailAdress", E);
                    MessageBox.Show("Error delete records");
                }              
            }          
        }
        public void GetId()
        {
            try
            {
                int id = Convert.ToInt32(DtgMailMinMax.SelectedRows[0].Cells[0].Value);
                Rights.id = id;
                log.Info("Get id from class rights");
            }
            catch(Exception E)
            {
                log.Error("Can't get id from class rights");
                MessageBox.Show(E.Message);
            }
        }
        public void BanAccountMail()
        {
            using (SqlConnection connection2 = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    if (DtgMailMinMax.SelectedRows.Count > 0)
                    {
                        int selectedIndex = DtgMailMinMax.SelectedRows[0].Index;
                        int rowID = int.Parse(DtgMailMinMax[0, selectedIndex].Value.ToString());
                        string sql = "UPDATE tbl_EmailAdress SET Status='Ingeschakeld' WHERE id=@RowID";
                        SqlCommand BanUser = new SqlCommand
                        {
                            Connection = connection2,
                            CommandType = CommandType.Text,
                            CommandText = sql
                        };
                        SqlParameter RowParameter = new SqlParameter
                        {
                            ParameterName = "@RowID",
                            SqlDbType = SqlDbType.Int,
                            IsNullable = false,
                            Value = rowID
                        };
                        BanUser.Parameters.Add(RowParameter);
                        BanUser.Connection.Open();
                        BanUser.ExecuteNonQuery();
                        BanUser.Connection.Close();
                        log.Info("UPDATE status account to active");
                    }
                }
                catch (Exception E)
                {
                    log.Error("BanAccountMail", E);
                    MessageBox.Show("Error ban account");
                }            
            }             
        }
        public void UnBanAccountMail()
        {
            using (SqlConnection connection2 = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    if (DtgMailMinMax.SelectedRows.Count > 0)
                    {
                        int selectedIndex = DtgMailMinMax.SelectedRows[0].Index;
                        int rowID = int.Parse(DtgMailMinMax[0, selectedIndex].Value.ToString());
                        string sql = "UPDATE tbl_EmailAdress SET Status='Uitgeschakeld' WHERE id=@RowID";
                        SqlCommand UnbanUser = new SqlCommand
                        {
                            Connection = connection2,
                            CommandType = CommandType.Text,
                            CommandText = sql
                        };
                        SqlParameter RowParameter = new SqlParameter
                        {
                            ParameterName = "@RowID",
                            SqlDbType = SqlDbType.Int,
                            IsNullable = false,
                            Value = rowID
                        };
                        UnbanUser.Parameters.Add(RowParameter);
                        UnbanUser.Connection.Open();
                        UnbanUser.ExecuteNonQuery();
                        UnbanUser.Connection.Close();
                    }
                    log.Info("UPDATE status account to inactive");
                }
                catch (Exception E)
                {
                    log.Error("UnbanEmailAccount", E);
                    MessageBox.Show("Error unban account");
                }            
            }              
        }
        // query (mail wfapp)
        public void DeleteRecordWFApp()
        {
            using (SqlConnection connection2 = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    if (DtgServiceError.SelectedRows.Count > 0)
                    {
                        int selectedIndex = DtgServiceError.SelectedRows[0].Index;
                        int rowID = int.Parse(DtgServiceError[0, selectedIndex].Value.ToString());
                        string sql = "DELETE FROM tbl_EmailAdressWFapp WHERE id=@RowID";
                        SqlCommand deleteRecord = new SqlCommand
                        {
                            Connection = connection2,
                            CommandType = CommandType.Text,
                            CommandText = sql
                        };
                        SqlParameter RowParameter = new SqlParameter
                        {
                            ParameterName = "@RowID",
                            SqlDbType = SqlDbType.Int,
                            IsNullable = false,
                            Value = rowID
                        };
                        deleteRecord.Parameters.Add(RowParameter);
                        deleteRecord.Connection.Open();
                        deleteRecord.ExecuteNonQuery();
                        deleteRecord.Connection.Close();
                    }
                    log.Info("DELETE FROM tbl_EmailAdressWFApp");
                }
                catch (Exception E)
                {
                    log.Error("DeleteRecordWFApp (F)", E);
                    MessageBox.Show("Error unban account Windows form app");
                }                
            }              
        }
        public void SelectUsersWFApp()
        {
            DtgServiceError.DataSource = SQL.GetSQLDataView("SELECT Id, Email, Voornaam, Tussenvoegsel, Achternaam, Status FROM tbl_EmailAdressWFapp");
            this.DtgServiceError.Columns[1].ReadOnly = true;
            this.DtgServiceError.Columns[2].ReadOnly = true;
            this.DtgServiceError.Columns[3].ReadOnly = true;
            this.DtgServiceError.Columns[4].ReadOnly = true;
            this.DtgServiceError.Columns[5].ReadOnly = true;
            DtgServiceError.Columns[0].Visible = false;
            DtgServiceError.Columns[1].Width = 240;
            DtgServiceError.Columns[2].Width = 230;
            DtgServiceError.Columns[3].Width = 100;
            DtgServiceError.Columns[4].Width = 235;
            DtgServiceError.Columns[5].Width = 100;
            log.Info("SELECT FROM tbl_EmailAdressWFApp");
        }
        public void BanAccountMailWFApp()
        {
            using (SqlConnection connection2 = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    if (DtgServiceError.SelectedRows.Count > 0)
                    {
                        int selectedIndex = DtgServiceError.SelectedRows[0].Index;
                        int rowID = int.Parse(DtgServiceError[0, selectedIndex].Value.ToString());
                        string sql = "UPDATE tbl_EmailAdressWFapp SET Status='Ingeschakeld' WHERE id=@RowID";
                        SqlCommand BanUser = new SqlCommand
                        {
                            Connection = connection2,
                            CommandType = CommandType.Text,
                            CommandText = sql
                        };
                        SqlParameter RowParameter = new SqlParameter
                        {
                            ParameterName = "@RowID",
                            SqlDbType = SqlDbType.Int,
                            IsNullable = false,
                            Value = rowID
                        };
                        BanUser.Parameters.Add(RowParameter);
                        BanUser.Connection.Open();
                        BanUser.ExecuteNonQuery();
                        BanUser.Connection.Close();
                    }
                    log.Info("UPDATE tbl_EmailAdressWFApp status to active");
                }
                catch (Exception E)
                {
                    log.Error("BanAccountMailWFApp", E);
                    MessageBox.Show("BanAccountMailWFApp Error");
                }            
            }
        }
        public void UnBanAccountMailWFApp()
        {
            using (SqlConnection connection2 = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    if (DtgServiceError.SelectedRows.Count > 0)
                    {
                        int selectedIndex = DtgServiceError.SelectedRows[0].Index;
                        int rowID = int.Parse(DtgServiceError[0, selectedIndex].Value.ToString());
                        string sql = "UPDATE tbl_EmailAdressWFapp SET Status='Uitgeschakeld' WHERE id=@RowID";
                        SqlCommand UnbanUser = new SqlCommand
                        {
                            Connection = connection2,
                            CommandType = CommandType.Text,
                            CommandText = sql
                        };
                        SqlParameter RowParameter = new SqlParameter
                        {
                            ParameterName = "@RowID",
                            SqlDbType = SqlDbType.Int,
                            IsNullable = false,
                            Value = rowID
                        };
                        UnbanUser.Parameters.Add(RowParameter);
                        UnbanUser.Connection.Open();
                        UnbanUser.ExecuteNonQuery();
                        UnbanUser.Connection.Close();
                    }
                    log.Info("UPDATE tbl_EmailAdressWFApp status to active");
                }
                catch(Exception E)
                {
                    log.Error("UnBanAccountMailWFApp", E);
                    MessageBox.Show("UnBanAccountMailWFApp Error");
                }              
            }
        }
        public void GetIdWFApp()
        {
            try
            {
                int id = Convert.ToInt32(DtgServiceError.SelectedRows[0].Cells[0].Value);
                Rights.id = id;
                log.Info("Get id from class rights");
            }
            catch (Exception E)
            {
                log.Error("GetIdWFApp", E);
                MessageBox.Show(E.Message);
            }
        }
        // buttons mail wfapp
        private void BtnAdd2_Click(object sender, EventArgs e)
        {
            GebruikerWFToevoegen gebruikerwfToevoegen = new GebruikerWFToevoegen();
            gebruikerwfToevoegen.Show();
            gebruikerwfToevoegen.Location = new Point(100, 100);
            if (ParentForm2 != null)
                ParentForm2.Refresh();
            Close();
            log.Info("BtnAdd2 clicked, open gebruikerWFToevoegen.cs");
        }
        private void BtnEdit2_Click(object sender, EventArgs e)
        {
            log.Info("BtnEdit2 clicked");
            GetIdWFApp();
            log.Info("GetIdWFApp (F)");
            GebruikersWFBewerken gebruikerwfbewerken = new GebruikersWFBewerken();
            gebruikerwfbewerken.Show();
            log.Info("Open GebruikersWFBewerken.cs");
            if (ParentForm2 != null)
                ParentForm2.Refresh();
            Close();
        }
        private void BtnDelete2_Click(object sender, EventArgs e)
        {
            log.Info("BtnDelete2 clicked");
            DeleteRecordWFApp();
            log.Info("DeleteRecordWFApp (F)");
            SelectUsersWFApp();
            log.Info("SelectUsersWFApp (F)");
        }
        private void BtnBan2_Click(object sender, EventArgs e)
        {
            log.Info("BtnBan2 clicked");
            BanAccountMailWFApp();
            log.Info("BanAccountMailWFApp (F)");
            SelectUsersWFApp();
            log.Info("SelectUsersWFApp (F)");
        }
        private void BtnUnban2_Click(object sender, EventArgs e)
        {
            log.Info("BtnUnban2 clicked");
            UnBanAccountMailWFApp();
            log.Info("UnBanAccountMailWFApp (F)");
            SelectUsersWFApp();
            log.Info("SelectUsersWFApp (F)");
        }

        private void DataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.Cell == null || e.StateChanged != DataGridViewElementStates.Selected)
                return;

            if (e.Cell.RowIndex == selectedCellRow && e.Cell.ColumnIndex == selectedCellColumn)
                return;

            if (!e.Cell.Selected)
                return;

            if (e.Cell.RowIndex == 0 || e.Cell.ColumnIndex == 0 || e.Cell.RowIndex == 1 && e.Cell.ColumnIndex == 1 || e.Cell.RowIndex == 1 || e.Cell.ColumnIndex == 1 || e.Cell.RowIndex == 2 && e.Cell.ColumnIndex == 2
                || e.Cell.RowIndex == 2 || e.Cell.ColumnIndex == 2 || e.Cell.RowIndex == 3 && e.Cell.ColumnIndex == 3 || e.Cell.RowIndex == 3 || e.Cell.ColumnIndex == 3 || e.Cell.RowIndex == 4 && e.Cell.ColumnIndex == 4
                || e.Cell.RowIndex == 4 || e.Cell.ColumnIndex == 4)
            {
                e.Cell.Selected = false;
                DtgMailMinMax.Rows[selectedCellRow].Cells[selectedCellColumn].Selected = true;
            }
            else
            {
                selectedCellRow = e.Cell.RowIndex;
                selectedCellColumn = e.Cell.ColumnIndex;
            }
            log.Info("DatagridViewFormApp cells can't be changed");
        }

        private void DataGridView2_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.Cell == null || e.StateChanged != DataGridViewElementStates.Selected)
                return;

            if (e.Cell.RowIndex == selectedCellRow && e.Cell.ColumnIndex == selectedCellColumn)
                return;

            if (!e.Cell.Selected)
                return;

            if (e.Cell.RowIndex == 0 || e.Cell.ColumnIndex == 0 || e.Cell.RowIndex == 1 && e.Cell.ColumnIndex == 1 || e.Cell.RowIndex == 1 || e.Cell.ColumnIndex == 1 || e.Cell.RowIndex == 2 && e.Cell.ColumnIndex == 2
                || e.Cell.RowIndex == 2 || e.Cell.ColumnIndex == 2 || e.Cell.RowIndex == 3 && e.Cell.ColumnIndex == 3 || e.Cell.RowIndex == 3 || e.Cell.ColumnIndex == 3 || e.Cell.RowIndex == 4 && e.Cell.ColumnIndex == 4
                || e.Cell.RowIndex == 4 || e.Cell.ColumnIndex == 4)
            {
                e.Cell.Selected = false;
                DtgServiceError.Rows[selectedCellRow].Cells[selectedCellColumn].Selected = true;
            }
            else
            {
                selectedCellRow = e.Cell.RowIndex;
                selectedCellColumn = e.Cell.ColumnIndex;
            }
            log.Info("DatagridViewService cells can't be changed");
        }
    }
}