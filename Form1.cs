using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Net.Http;
using System.Configuration;
using pasLogger;
using pasTemp;
using System.Management;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Vertalen;

namespace WindowsFormsApp1
{
    public partial class Temperatuur : Form
    {
        private SettingsSensor1 SettingsSensor1 = new SettingsSensor1();
        private SettingsSensor2 SettingsSensor2 = new SettingsSensor2();
        private Contact contact = new Contact();
        private Help help = new Help();
        string MyConnectionString2 = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        Timer Timer = new Timer();
        Label label = new Label();
        double timeLeft = 9999999999999999;
        HttpClient HC = new HttpClient();
        DateTime NextMailAllowed = DateTime.Now;
        string BasePath = "http://api.pasys.nl/msgcenter/api/MsgJob/PostMsgJob";
        private Excel.Application app = null;
        private Excel.Workbook workbook = null;
        private Excel.Worksheet worksheet = null;
        // programma start
        public Temperatuur()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(400, 100);
            IsServerConnected();
        }
        private void ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RdbCelsius1.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT Id, TemperatureCelsius, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        ExportDataSetToExcel(ds);
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }
            if (RdbKelvin1.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT Id, TemperatureKelvin, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        ExportDataSetToExcel(ds);
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }
            if (RdbFarhenheid1.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT Id, TemperatureFarhenheid, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        ExportDataSetToExcel(ds);
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }

            if (RdbCelsius2.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT Id, TemperatureCelsius, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        ExportDataSetToExcel(ds);
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }
            if (RdbKelvin2.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT Id, TemperatureKelvin, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        ExportDataSetToExcel(ds);
                        connection.Close();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }
            if (RdbFarhenheid2.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT Id, TemperatureFarhenheid, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        ExportDataSetToExcel(ds);
                        connection.Close();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }

            if (RdbCelsiusAll.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT Id, TemperatureCelsius, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "');");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    ExportDataSetToExcel(ds);
                }
            }
            if (RdbKelvinAll.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT Id, TemperatureKelvin, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "');");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    ExportDataSetToExcel(ds);
                }
            }
            if (RdbFarhenheidAll.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT Id, TemperatureFarhenheid, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "');");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    ExportDataSetToExcel(ds);
                }
            }
        }
        public bool IsServerConnected()
        {
            using (var l_oConnection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    l_oConnection.Open();
                    Logger.Log(LogType.ltInfo, "Connectie verbonden.");
                    l_oConnection.Close();
                    return true;
                }
                catch (SqlException)
                {
                    MessageBox.Show("De database is niet bereikbaar");
                    Logger.Log(LogType.ltError, "De database is niet bereikbaar");
                    return false;
                }
            }
        }
        public void NewUpdate()
        {
            MessageBox.Show("Er is een update beschikbaar.");
        }
        private void Temperatuur_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.DtpVan = DtpVan.Value;
            Properties.Settings.Default.Save();
            Application.Exit();
        }
        private void Temperatuur_Load_1(object sender, EventArgs e)
        {

        }
        private void Temperatuur_Shown(object sender, EventArgs e)
        {
            TxbLastConnTime.Enabled = false;
            TxbUserLoggedIn.Enabled = false;
            TxbPcUser.Enabled = false;
            TbDigiClock.Enabled = false;
            TimerDigiClock.Enabled = true;
            TimerDigiClock.Interval = 500;         
            TxbSelectedSensor1.Enabled = false;
            TxbSelectedSensor1.BackColor = Color.White;
            TbMinimumTemperatuur1.Enabled = false;
            TbMaximumTemperatuur1.BackColor = Color.White;
            TbMaximumTemperatuur1.Enabled = false;
            TbMaximumTemperatuur1.BackColor = Color.White;
            TxbSelectedSensor2.Enabled = false;
            TxbSelectedSensor2.BackColor = Color.White;
            TbMinimumTemperatuur2.Enabled = false;
            TbMaximumTemperatuur2.BackColor = Color.White;
            TbMaximumTemperatuur2.Enabled = false;
            TbMaximumTemperatuur2.BackColor = Color.White;
            TimerStatusSQL.Enabled = true;
            TimerStatusSQL.Interval = 500;
            IsServerConnected();
            bool laasteversie = true;
            if (laasteversie == false)
            {
                NewUpdate();
            }
            uitschakelenToolStripMenuItem.PerformClick();
            RdbCelsius1.PerformClick();
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            minimaalToolStripMenuItem1.Checked = true;
            minimaalToolStripMenuItem1.PerformClick();
            BtnTimerStop.PerformClick();
            TxbUserLoggedIn.Text = Rights.username;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            string username = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];
            TxbPcUser.Text = username;
            BtnStatusSqlConnection.PerformClick();
            try
            {
                DtpVan.Value = Properties.Settings.Default.DtpVan;
            }
            catch
            {
                DtpVan.Value = DateTime.Today;
            }
            Properties.Settings.Default.DtpVan = DtpVan.Value;
            Properties.Settings.Default.Save();
            DtpVan.Format = DateTimePickerFormat.Custom;
            DtpVan.CustomFormat = "dd/MM/yyyy HH:mm";
            DtpTot.Format = DateTimePickerFormat.Custom;
            DtpTot.CustomFormat = "dd/MM/yyyy HH:mm";

            timer2.Enabled = true;
            // rechten gebruiker of admin
            if (Rights.rights == 0)
            {
                BtnSettingsSensor1.Visible = false;
                BtnSettingsSensor2.Visible = false;
                PnlActiveS1.Visible = false;
                PnlActiveS2.Visible = false;
            }
            CheckLastDataDB();
            BtnOpvragenVanTot.Focus();
            SetTitleAxis();
            SetTitleSensors();
            Vertaal.LoadVertaal();
        }
        // programmma stop
        // begin instellingen
        private void RoodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            roodToolStripMenuItem.Checked = true;
            this.BackColor = Color.Red;
            witToolStripMenuItem.Checked = false;
            blauwToolStripMenuItem.Checked = false;
            grijsToolStripMenuItem.Checked = false;
            groenToolStripMenuItem.Checked = false;
            geelToolStripMenuItem.Checked = false;
            orgineelToolStripMenuItem.Checked = false;
        }
        private void WitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            witToolStripMenuItem.Checked = true;
            this.BackColor = Color.White;
            roodToolStripMenuItem.Checked = false;
            blauwToolStripMenuItem.Checked = false;
            grijsToolStripMenuItem.Checked = false;
            groenToolStripMenuItem.Checked = false;
            geelToolStripMenuItem.Checked = false;
            orgineelToolStripMenuItem.Checked = false;
        }
        private void BlauwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blauwToolStripMenuItem.Checked = true;
            this.BackColor = Color.Blue;
            roodToolStripMenuItem.Checked = false;
            witToolStripMenuItem.Checked = false;
            grijsToolStripMenuItem.Checked = false;
            groenToolStripMenuItem.Checked = false;
            geelToolStripMenuItem.Checked = false;
            orgineelToolStripMenuItem.Checked = false;
        }
        private void GrijsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grijsToolStripMenuItem.Checked = true;
            this.BackColor = Color.Gray;
            roodToolStripMenuItem.Checked = false;
            witToolStripMenuItem.Checked = false;
            blauwToolStripMenuItem.Checked = false;
            groenToolStripMenuItem.Checked = false;
            geelToolStripMenuItem.Checked = false;
            orgineelToolStripMenuItem.Checked = false;
        }
        private void GroenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groenToolStripMenuItem.Checked = true;
            this.BackColor = Color.Green;
            roodToolStripMenuItem.Checked = false;
            witToolStripMenuItem.Checked = false;
            blauwToolStripMenuItem.Checked = false;
            grijsToolStripMenuItem.Checked = false;
            geelToolStripMenuItem.Checked = false;
            orgineelToolStripMenuItem.Checked = false;
        }
        private void GeelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            geelToolStripMenuItem.Checked = true;
            this.BackColor = Color.Yellow;
            witToolStripMenuItem.Checked = false;
            roodToolStripMenuItem.Checked = false;
            blauwToolStripMenuItem.Checked = false;
            grijsToolStripMenuItem.Checked = false;
            groenToolStripMenuItem.Checked = false;
            orgineelToolStripMenuItem.Checked = false;
        }
        private void OrigineelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            orgineelToolStripMenuItem.Checked = true;
            blauwToolStripMenuItem.Checked = false;
            this.BackColor = Color.FromArgb(240, 240, 240);
            roodToolStripMenuItem.Checked = false;
            witToolStripMenuItem.Checked = false;
            grijsToolStripMenuItem.Checked = false;
            groenToolStripMenuItem.Checked = false;
            geelToolStripMenuItem.Checked = false;
        }
        private void WebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.portaldemo.pasys/");
        }
        private void MobieleApplicatieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.portaldemo.pasys/");
        }
        private void ContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Contact contact = new Contact())
            {
                contact.Parentform1 = this;
                contact.Parentform2 = this;
                contact.Parentform3 = this;
                contact.ShowDialog();
            }
        }
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help.Parentform1 = this;
            help.ShowDialog();
        }
        private void ScreenshotGrafiekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RdbCelsius1.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPeg Image|*.jpg",
                    Title = "Save Chart As Image File",
                    FileName = "CelsiusSensor1.png"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                if (result == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (saveFileDialog.CheckPathExists)
                        {
                            if (saveFileDialog.FilterIndex == 2)
                            {
                                GrafiekTemperatuur.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                            }
                            else if (saveFileDialog.FilterIndex == 1)
                            {
                                GrafiekTemperatuur.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("De locatie waar u het wil opslaan bestaat niet.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (RdbKelvin1.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPeg Image|*.jpg",
                    Title = "Save Chart As Image File",
                    FileName = "KelvinSensor1.png"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                if (result == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (saveFileDialog.CheckPathExists)
                        {
                            if (saveFileDialog.FilterIndex == 2)
                            {
                                GrafiekKelvin1.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                            }
                            else if (saveFileDialog.FilterIndex == 1)
                            {
                                GrafiekKelvin1.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("De locatie waar u het wil opslaan bestaat niet.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (RdbFarhenheid1.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPeg Image|*.jpg",
                    Title = "Save Chart As Image File",
                    FileName = "FarhenheidSensor1.png"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                if (result == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (saveFileDialog.CheckPathExists)
                        {
                            if (saveFileDialog.FilterIndex == 2)
                            {
                                grafiekFarhenheid1.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                            }
                            else if (saveFileDialog.FilterIndex == 1)
                            {
                                grafiekFarhenheid1.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("De locatie waar u het wil opslaan bestaat niet.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (RdbCelsius2.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPeg Image|*.jpg",
                    Title = "Save Chart As Image File",
                    FileName = "CelsiusSensor2.png"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                if (result == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (saveFileDialog.CheckPathExists)
                        {
                            if (saveFileDialog.FilterIndex == 2)
                            {
                                GrafiekTemperatuur2.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                            }
                            else if (saveFileDialog.FilterIndex == 1)
                            {
                                GrafiekTemperatuur2.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("De locatie waar u het wil opslaan bestaat niet.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (RdbKelvin2.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPeg Image|*.jpg",
                    Title = "Save Chart As Image File",
                    FileName = "KelvinSensor2.png"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                if (result == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (saveFileDialog.CheckPathExists)
                        {
                            if (saveFileDialog.FilterIndex == 2)
                            {
                                GrafiekKelvin2.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                            }
                            else if (saveFileDialog.FilterIndex == 1)
                            {
                                GrafiekKelvin2.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("De locatie waar u het wil opslaan bestaat niet.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (RdbFarhenheid2.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPeg Image|*.jpg",
                    Title = "Save Chart As Image File",
                    FileName = "FarhenheidSensor2.png"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                if (result == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (saveFileDialog.CheckPathExists)
                        {
                            if (saveFileDialog.FilterIndex == 2)
                            {
                                grafiekFarhenheid2.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                            }
                            else if (saveFileDialog.FilterIndex == 1)
                            {
                                grafiekFarhenheid2.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("De locatie waar u het wil opslaan bestaat niet.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (RdbCelsiusAll.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPeg Image|*.jpg",
                    Title = "Save Chart As Image File",
                    FileName = "CelsiusSensor1,2.png"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                if (result == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (saveFileDialog.CheckPathExists)
                        {
                            if (saveFileDialog.FilterIndex == 2)
                            {
                                GrafiekCelsiusAll.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                            }
                            else if (saveFileDialog.FilterIndex == 1)
                            {
                                GrafiekCelsiusAll.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("De locatie waar u het wil opslaan bestaat niet.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (RdbKelvinAll.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPeg Image|*.jpg",
                    Title = "Save Chart As Image File",
                    FileName = "KlevinSensor1,2.png"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                if (result == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (saveFileDialog.CheckPathExists)
                        {
                            if (saveFileDialog.FilterIndex == 2)
                            {
                                GrafiekKelvinAll.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                            }
                            else if (saveFileDialog.FilterIndex == 1)
                            {
                                GrafiekKelvinAll.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("De locatie waar u het wil opslaan bestaat niet.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (RdbFarhenheidAll.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPeg Image|*.jpg",
                    Title = "Save Chart As Image File",
                    FileName = "FarhenheidSensor1,2.png"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                if (result == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (saveFileDialog.CheckPathExists)
                        {
                            if (saveFileDialog.FilterIndex == 2)
                            {
                                GrafiekFarhenheidAll.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                            }
                            else if (saveFileDialog.FilterIndex == 1)
                            {
                                GrafiekFarhenheidAll.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                            }
                        }
                        else
                        {
                            MessageBox.Show("De locatie waar u het wil opslaan bestaat niet.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void MeldingenUitschakelenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gebruiker gebruiker = new Gebruiker()
            {
                Parentform1 = this,
            };
            gebruiker.ShowDialog();
        }
        private void FullScreenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            fullScreenToolStripMenuItem2.Checked = true;
            maximaalToolStripMenuItem1.Checked = false;
            minimaalToolStripMenuItem1.Checked = false;
        }
        private void MaximaalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            fullScreenToolStripMenuItem2.Checked = false;
            maximaalToolStripMenuItem1.Checked = true;
            minimaalToolStripMenuItem1.Checked = false;
        }
        private void MinimaalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            fullScreenToolStripMenuItem2.Checked = false;
            maximaalToolStripMenuItem1.Checked = false;
            minimaalToolStripMenuItem1.Checked = true;
        }
        private void DtpVan_ValueChanged(object sender, EventArgs e)
        {
            SettingsDtpVan();
        }
        private void DtpTot_ValueChanged(object sender, EventArgs e)
        {
            SettingsDtpTot();
        }
        private async void MinutenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Timer15ServiceAlert.Enabled = true;
            Timer15ServiceAlert.Interval = 900000;
            Timer30ServiceAlert.Enabled = false;
            Timer60ServiceAlert.Enabled = false;

            minutenToolStripMenuItem.Checked = true;
            minutenToolStripMenuItem1.Checked = false;
            minutenToolStripMenuItem2.Checked = false;
            uitschakelenToolStripMenuItem.Checked = false;

            if (uitschakelenToolStripMenuItem.Checked == true)
            {
                uitschakelenToolStripMenuItem.Text = "Uitgeschakeld";
            }
            if (uitschakelenToolStripMenuItem.Checked == false)
            {
                uitschakelenToolStripMenuItem.Text = "Uitschakelen";
            }

            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT DateTime FROM tbl_Temperature WHERE ID = (SELECT MAX(ID)  FROM tbl_Temperature)");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    DateTime TimeFromDB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateTime"]).AddMinutes(15);
                    connection.Close();
                    DateTime TimeNow = DateTime.Now;
                    if (TimeFromDB < TimeNow)
                    {
                        MessageBox.Show("Problemen met de service, 15 minuten geen nieuwe data!", "WAARSCHUWING!");
                        await SendEMail();
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }
        private async void MinutenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Timer15ServiceAlert.Enabled = false;
            Timer30ServiceAlert.Enabled = true;
            Timer30ServiceAlert.Interval = 1800000;
            Timer60ServiceAlert.Enabled = false;

            minutenToolStripMenuItem.Checked = false;
            minutenToolStripMenuItem1.Checked = true;
            minutenToolStripMenuItem2.Checked = false;
            uitschakelenToolStripMenuItem.Checked = false;

            if (uitschakelenToolStripMenuItem.Checked == true)
            {
                uitschakelenToolStripMenuItem.Text = "Uitgeschakeld";
            }
            if (uitschakelenToolStripMenuItem.Checked == false)
            {
                uitschakelenToolStripMenuItem.Text = "Uitschakelen";
            }

            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    // laatste tijd in db
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT DateTime FROM tbl_Temperature WHERE ID = (SELECT MAX(ID)  FROM tbl_Temperature)");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    DateTime TimeFromDB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateTime"]).AddMinutes(30);
                    connection.Close();
                    // tijd nu
                    DateTime TimeNow = DateTime.Now;

                    if (TimeFromDB < TimeNow)
                    {
                        MessageBox.Show("Problemen met de service, 30 minuten geen nieuwe data!", "WAARSCHUWING!");
                        await SendEMail();
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }
        private async void MinutenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Timer15ServiceAlert.Enabled = false;
            Timer30ServiceAlert.Enabled = false;
            Timer60ServiceAlert.Enabled = true;
            Timer60ServiceAlert.Interval = 3600000;

            minutenToolStripMenuItem.Checked = false;
            minutenToolStripMenuItem1.Checked = false;
            minutenToolStripMenuItem2.Checked = true;
            uitschakelenToolStripMenuItem.Checked = false;

            if (uitschakelenToolStripMenuItem.Checked == true)
            {
                uitschakelenToolStripMenuItem.Text = "Uitgeschakeld";
            }
            if (uitschakelenToolStripMenuItem.Checked == false)
            {
                uitschakelenToolStripMenuItem.Text = "Uitschakelen";
            }

            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    // laatste tijd in db
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT DateTime FROM tbl_Temperature WHERE ID = (SELECT MAX(ID)  FROM tbl_Temperature)");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    DateTime TimeFromDB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateTime"]).AddMinutes(60);
                    connection.Close();
                    // tijd nu
                    DateTime TimeNow = DateTime.Now;
                    if (TimeFromDB < TimeNow)
                    {
                        MessageBox.Show("Problemen met de service, 60 minuten (of meer) geen nieuwe data!", "WAARSCHUWING!");
                        await SendEMail();
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }

        }
        private void UitschakelenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minutenToolStripMenuItem.Checked = false;
            minutenToolStripMenuItem1.Checked = false;
            minutenToolStripMenuItem2.Checked = false;
            uitschakelenToolStripMenuItem.Checked = true;

            Timer15ServiceAlert.Enabled = false;
            Timer30ServiceAlert.Enabled = false;
            Timer60ServiceAlert.Enabled = false;

            if (uitschakelenToolStripMenuItem.Checked == true)
            {
                uitschakelenToolStripMenuItem.Text = "Uitgeschakeld";
            }
            if (uitschakelenToolStripMenuItem.Checked == false)
            {
                uitschakelenToolStripMenuItem.Text = "Uitschakelen";
            }
        }
        private void EngelsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            nederlandsToolStripMenuItem.Checked = false;
            duitsToolStripMenuItem.Checked = false;
            engelsToolStripMenuItem.Checked = true;

            foreach (Control control in Controls)
            {
                foreach (Control controls in GetAllControls(control))
                {
                    Vertaal.VertaalControlsEN(controls, "EN");
                }
            }

            foreach (ToolStripMenuItem item in Instellingen.Items)
            {
                foreach (ToolStripMenuItem dditem in item.DropDownItems)
                {
                    Vertaal.VertaalContexMenueStripEN(dditem, "EN");
                }

                foreach (ToolStripItem Item in Instellingen.Items)
                {
                    Vertaal.VertaalContexMenueStrip2EN(item, "EN");
                }
            }
        }
        private void NederlandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engelsToolStripMenuItem.Checked = false;
            duitsToolStripMenuItem.Checked = false;
            nederlandsToolStripMenuItem.Checked = true;

            foreach (Control control in Controls)
            {
                foreach (Control controls in GetAllControls(control))
                {
                   Vertaal.VertaalControlsNL(controls, "NL");
                }
            }

            foreach (ToolStripMenuItem item in Instellingen.Items)
            {
                foreach (ToolStripMenuItem dditem in item.DropDownItems)
                {
                    Vertaal.VertaalContexMenueStripNL(dditem, "NL");
                }

                foreach (ToolStripItem Item in Instellingen.Items)
                {
                    Vertaal.VertaalContexMenueStrip2NL(item, "NL");
                }
            }
        }
        private void DuitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nederlandsToolStripMenuItem.Checked = false;
            engelsToolStripMenuItem.Checked = false;
            duitsToolStripMenuItem.Checked = true;

            foreach (Control control in Controls)
            {
                foreach (Control controls in GetAllControls(control))
                {
                    Vertaal.VertaalControlsDE(controls, "DE");
                }
            }

            foreach (ToolStripMenuItem item in Instellingen.Items)
            {
                foreach (ToolStripMenuItem dditem in item.DropDownItems)
                {
                    Vertaal.VertaalContexMenueStripDE(dditem, "DE");
                }

                foreach (ToolStripItem Item in Instellingen.Items)
                {
                    Vertaal.VertaalContexMenueStrip2DE(item, "DE");
                }
            }

        }
        // einde instellingen
        // begin buttons
        private void BtnInstellingen_Click(object sender, EventArgs e)
        {
            Instellingen.Show(BtnInstellingen.Left + this.Left, BtnInstellingen.Top + BtnInstellingen.Height + this.Top);
        }
        private void BtnOpvragenVanTot_Click(object sender, EventArgs e)
        {
            if (RdbCelsius1.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT TemperatureCelsius, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        GrafiekTemperatuur.DataSource = ds.Tables[0].DefaultView;
                        GrafiekTemperatuur.DataBind();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }
            if (RdbKelvin1.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT TemperatureKelvin, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        GrafiekKelvin1.DataSource = ds.Tables[0].DefaultView;
                        GrafiekKelvin1.DataBind();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }
            if (RdbFarhenheid1.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT TemperatureFarhenheid, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        grafiekFarhenheid1.DataSource = ds.Tables[0].DefaultView;
                        grafiekFarhenheid1.DataBind();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }

            if (RdbCelsius2.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT TemperatureCelsius, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        GrafiekTemperatuur2.DataSource = ds.Tables[0].DefaultView;
                        GrafiekTemperatuur2.DataBind();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }
            if (RdbKelvin2.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT TemperatureKelvin, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        GrafiekKelvin2.DataSource = ds.Tables[0].DefaultView;
                        GrafiekKelvin2.DataBind();
                        connection.Close();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }
            if (RdbFarhenheid2.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT TemperatureFarhenheid, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        grafiekFarhenheid2.DataSource = ds.Tables[0].DefaultView;
                        grafiekFarhenheid2.DataBind();
                        connection.Close();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
            }

            if (RdbCelsiusAll.Checked == true)
            {
                GrafiekCelsiusAll.Series.Clear();
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT TemperatureCelsius, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "');");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    GrafiekCelsiusAll.DataSource = ds;

                    int AmountOfRows = Convert.ToInt32(ds.Tables[0].Rows[1]["Location_Id"]);
                    for (int i = 0; i < AmountOfRows; i++)
                    {
                        List<DateTime> xvals = new List<DateTime>();
                        List<decimal> yvals = new List<decimal>();
                        string serieName = ds.Tables[0].Rows[i]["Location_Id"].ToString();
                        GrafiekCelsiusAll.Series.Add(serieName);
                        GrafiekCelsiusAll.Series[i].ChartType = SeriesChartType.Line;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            try
                            {
                                if (String.Equals(serieName, dr["Location_Id"].ToString(), StringComparison.Ordinal))
                                {
                                    xvals.Add(Convert.ToDateTime(dr["DateTime"]));
                                    yvals.Add(Convert.ToDecimal(dr["TemperatureCelsius"].ToString()));
                                }
                            }
                            catch (Exception E)
                            {
                                throw new InvalidOperationException(E.Message);
                            }
                        }
                        try
                        {
                            GrafiekCelsiusAll.Series[serieName].XValueType = ChartValueType.DateTime;
                            GrafiekCelsiusAll.Series[serieName].YValueType = ChartValueType.Auto;
                            GrafiekCelsiusAll.Series[serieName].Points.DataBindXY(xvals.ToArray(), yvals.ToArray());
                        }
                        catch (Exception E)
                        {
                            throw new InvalidOperationException(E.Message);
                        }
                    }
                    GrafiekCelsiusAll.DataBind();
                }
            }
            if (RdbKelvinAll.Checked == true)
            {
                GrafiekKelvinAll.Series.Clear();
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT TemperatureKelvin, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "');");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    GrafiekKelvinAll.DataSource = ds;

                    int AmountOfRows = Convert.ToInt32(ds.Tables[0].Rows[1]["Location_Id"]);
                    for (int i = 0; i < AmountOfRows; i++)
                    {
                        List<DateTime> xvals = new List<DateTime>();
                        List<decimal> yvals = new List<decimal>();
                        string serieName = ds.Tables[0].Rows[i]["Location_Id"].ToString();
                        GrafiekKelvinAll.Series.Add(serieName);
                        GrafiekKelvinAll.Series[i].ChartType = SeriesChartType.Line;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            try
                            {
                                if (String.Equals(serieName, dr["Location_Id"].ToString(), StringComparison.Ordinal))
                                {
                                    xvals.Add(Convert.ToDateTime(dr["DateTime"]));
                                    yvals.Add(Convert.ToDecimal(dr["TemperatureKelvin"].ToString()));
                                }
                            }
                            catch (Exception E)
                            {
                                throw new InvalidOperationException(E.Message);
                            }
                        }
                        try
                        {
                            GrafiekKelvinAll.Series[serieName].XValueType = ChartValueType.DateTime;
                            GrafiekKelvinAll.Series[serieName].YValueType = ChartValueType.Auto;
                            GrafiekKelvinAll.Series[serieName].Points.DataBindXY(xvals.ToArray(), yvals.ToArray());
                        }
                        catch (Exception)
                        {
                            throw new InvalidOperationException("fout");
                        }
                    }
                    GrafiekKelvinAll.DataBind();
                }
            }
            if (RdbFarhenheidAll.Checked == true)
            {
                GrafiekFarhenheidAll.Series.Clear();
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT TemperatureFarhenheid, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "');");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    GrafiekFarhenheidAll.DataSource = ds;

                    int AmountOfRows = Convert.ToInt32(ds.Tables[0].Rows[1]["Location_Id"]);
                    for (int i = 0; i < AmountOfRows; i++)
                    {
                        List<DateTime> xvals = new List<DateTime>();
                        List<decimal> yvals = new List<decimal>();
                        string serieName = ds.Tables[0].Rows[i]["Location_Id"].ToString();
                        GrafiekFarhenheidAll.Series.Add(serieName);
                        GrafiekFarhenheidAll.Series[i].ChartType = SeriesChartType.Line;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            try
                            {
                                if (String.Equals(serieName, dr["Location_Id"].ToString(), StringComparison.Ordinal))
                                {
                                    xvals.Add(Convert.ToDateTime(dr["DateTime"]));
                                    yvals.Add(Convert.ToDecimal(dr["TemperatureFarhenheid"].ToString()));
                                }
                            }
                            catch (Exception E)
                            {
                                throw new InvalidOperationException(E.Message);
                            }
                        }
                        try
                        {
                            GrafiekFarhenheidAll.Series[serieName].XValueType = ChartValueType.DateTime;
                            GrafiekFarhenheidAll.Series[serieName].YValueType = ChartValueType.Auto;
                            GrafiekFarhenheidAll.Series[serieName].Points.DataBindXY(xvals.ToArray(), yvals.ToArray());
                        }
                        catch (Exception)
                        {
                            throw new InvalidOperationException("fout");
                        }
                    }
                    GrafiekFarhenheidAll.DataBind();
                }
            }
        }
        private void BtnTimerStart_Click(object sender, EventArgs e)
        {
            TimerDigiClock.Enabled = true;
            BtnOpvragenVanTot.Visible = false;
            TbDigiClock.BackColor = Color.Lime;
            BtnTimerStart.Focus();
            DtpVan.Enabled = false;
            DtpTot.Enabled = false;
            Timer1_Tick(sender, e);
            timer1.Enabled = true;
            timer1.Interval = 45000;
            //BtnTimerStart.Text = "Gestart";
            //BtnTimerStop.Text = "Stop";
            BtnTimerStart.Enabled = false;
            BtnTimerStop.Enabled = true;
        }
        private void BtnTimerStop_Click(object sender, EventArgs e)
        {
            TimerDigiClock.Enabled = false;
            TbDigiClock.BackColor = Color.Red;
            TbDigiClock.Text = DateTime.Now.ToString("HH:mm:ss");
            BtnOpvragenVanTot.Visible = true;
            DtpVan.Enabled = true;
            DtpTot.Enabled = true;
            timer1.Enabled = false;
            //BtnTimerStart.Text = "Start";
            //BtnTimerStop.Text = "Gestopt";
            BtnTimerStart.Enabled = true;
            BtnTimerStop.Enabled = false;
            timer1.Stop();
        }
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inloggen fl = new Inloggen();
            Vertaal.TrnRecList.Clear();
            fl.Show();
        }
        private void BtnSettingsSensor1_Click(object sender, EventArgs e)
        {
            using (SettingsSensor1 ss1 = new SettingsSensor1())
            {
                ss1.Parentform1 = this;
                ss1.Parentform2 = this;
                ss1.Parentform3 = this;
                ss1.ShowDialog();             
            }
        }
        private void BtnSettingsSensor2_Click(object sender, EventArgs e)
        {
            using (SettingsSensor2 ss2 = new SettingsSensor2())
            {
                ss2.Parentform1 = this;
                ss2.Parentform2 = this;
                ss2.Parentform3 = this;
                ss2.ShowDialog();             
            }            
        }
        private void BtnStatusSqlConnection_Click(object sender, EventArgs e)
        {
            if (IsServerConnected() == true)
            {
                BtnStatusSqlConnection.Text = "Actief";
                BtnStatusSqlConnection.BackColor = Color.Lime;
            }
            else
            {
                BtnStatusSqlConnection.Text = "Inactief";
                BtnStatusSqlConnection.BackColor = Color.Red;
            }
        }
        // einde buttons
        // begin timers
        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (timeLeft == 0)
            {
                timer1.Enabled = !timer1.Enabled;
                label1.Text = "Time's out.";
            }
            else
            {
                DtpVan.Enabled = false;
                DtpTot.Enabled = false;
                timeLeft--;
                DtpTot.Value = DateTime.Now;
                label1.Text = "Time Left: " + timeLeft;
                timer1.Enabled = true;
                BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
                BtnStatusSqlConnection.PerformClick();
                TxbLastConnTime.Refresh();
            }
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            BtnOpvragenVanTot_Click(sender, e);
            Logger.Log(LogType.ltInfo, "Info melding");
            Logger.Log(LogType.ltWarning, "Waarschuwing melding");
            Logger.Log(LogType.ltError, "Fout melding");
        }
        private void TimerStatusSensor_Tick(object sender, EventArgs e)
        {
            // interval staat bij shown
            TimerStatusSQL.Enabled = false;
            if (timeLeft == 0)
            {
                TimerStatusSQL.Enabled = !TimerStatusSQL.Enabled;
                label1.Text = "Time's out.";
            }
            else
            {
                TxbLastConnTime.Refresh();
                CheckLastDataDB();
                BtnStatusSqlConnection.PerformClick();
                TimerStatusSQL.Enabled = true;
            }
            if (RdbCelsius1.Checked == true)
            {
                SelectLocMinMaxCelsius1();
                BtnSettingsSensor1.Enabled = true;
                BtnSettingsSensor2.Enabled = false;
            }
            if (RdbFarhenheid1.Checked == true)
            {
                SelectLocMinMaxFarhenheid1();
                BtnSettingsSensor1.Enabled = true;
                BtnSettingsSensor2.Enabled = false;
            }
            if (RdbKelvin1.Checked == true)
            {
                SelectLocMinMaxKelvin1();
                BtnSettingsSensor1.Enabled = true;
                BtnSettingsSensor2.Enabled = false;
            }
            if (RdbCelsius2.Checked == true)
            {
                SelectLocMinMaxCelsius2();
                BtnSettingsSensor1.Enabled = false;
                BtnSettingsSensor2.Enabled = true;
            }
            if (RdbFarhenheid2.Checked == true)
            {
                SelectLocMinMaxFarhenheid2();
                BtnSettingsSensor1.Enabled = false;
                BtnSettingsSensor2.Enabled = true;
            }
            if (RdbKelvin2.Checked == true)
            {
                SelectLocMinMaxKelvin2();
                BtnSettingsSensor1.Enabled = false;
                BtnSettingsSensor2.Enabled = true;
            }
        }
        private void Timer15ServiceAlert_Tick(object sender, EventArgs e)
        {
            minutenToolStripMenuItem.PerformClick();
        }
        private void Timer30ServiceAlert_Tick(object sender, EventArgs e)
        {
            minutenToolStripMenuItem1.PerformClick();
        }
        private void Timer60ServiceAlert_Tick(object sender, EventArgs e)
        {
            minutenToolStripMenuItem2.PerformClick();
        }
        private void TimerDigiClock_Tick(object sender, EventArgs e)
        {
            TbDigiClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        // einde timers
        // sensor 1 begin
        private void RdbFarhenheid1_Click(object sender, EventArgs e)
        {
            PnlS12Top.Visible = false;
            PnlSensor1.Invalidate();
            PnlS1Top.Visible = true;
            PnlS2Top.Visible = false;
            Btns1.Visible = true;
            BtnS2.Visible = false;
            BtnS12.Visible = false;
            PnlActiveS2.BackColor = Color.Transparent;
            PnlActiveS1.BackColor = Color.Lime;
            if (RdbFarhenheid1.Checked == true)
            {
                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = true;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekFarhenheidAll.Visible = false;
                GrafiekKelvinAll.Visible = false;


                RdbCelsius1.Checked = false;
                RdbKelvin1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbKelvin2.Checked = false;
                RdbFarhenheid1.Checked = true;
                RdbFarhenheid2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;

                TxbSelectedSensor2.Clear();
                TbMinimumTemperatuur2.Clear();
                TbMaximumTemperatuur2.Clear();
            }
            SelectLocMinMaxFarhenheid1();
            SelectLocMinMaxFarhenheid2();
            grafiekFarhenheid1.ResetAutoValues();
            DateTime AToF1 = DtpTot.Value;
            DateTime AFromF1 = DtpVan.Value;
            grafiekFarhenheid1.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempF1(AFromF1, AToF1);
            grafiekFarhenheid1.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempF1(AFromF1, AToF1);
            if (grafiekFarhenheid1.ChartAreas[0].AxisY.Maximum == grafiekFarhenheid1.ChartAreas[0].AxisY.Minimum)
            {
                grafiekFarhenheid1.ChartAreas[0].AxisY.Maximum = grafiekFarhenheid1.ChartAreas[0].AxisY.Minimum + 10;
            }
            grafiekFarhenheid1.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            grafiekFarhenheid1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
        }
        private void RdbKelvin1_Click(object sender, EventArgs e)
        {
            PnlS12Top.Visible = false;
            PnlS1Top.Visible = true;
            PnlS2Top.Visible = false;
            PnlActiveS2.BackColor = Color.Transparent;
            PnlActiveS1.BackColor = Color.Lime;
            Btns1.Visible = true;
            BtnS2.Visible = false;
            BtnS12.Visible = false;
            if (RdbKelvin1.Checked == true)
            {
                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = true;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;

                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbKelvin1.Checked = true;
                RdbKelvin2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;

                TxbSelectedSensor2.Clear();
                TbMinimumTemperatuur2.Clear();
                TbMaximumTemperatuur2.Clear();
            }
            SelectLocMinMaxKelvin1();
            SelectLocMinMaxKelvin2();
            GrafiekKelvin1.ResetAutoValues();
            DateTime AToK1 = DtpTot.Value;
            DateTime AFromK1 = DtpVan.Value;
            GrafiekKelvin1.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempK1(AFromK1, AToK1);
            GrafiekKelvin1.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempK1(AFromK1, AToK1);
            if (GrafiekKelvin1.ChartAreas[0].AxisY.Maximum == GrafiekKelvin1.ChartAreas[0].AxisY.Minimum)
            {
                GrafiekKelvin1.ChartAreas[0].AxisY.Maximum = GrafiekKelvin1.ChartAreas[0].AxisY.Minimum + 10;
            }
            GrafiekKelvin1.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            GrafiekKelvin1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
        }
        private void RdbCelsius1_Click(object sender, EventArgs e)
        {
            PnlS12Top.Visible = false;
            PnlS2Top.Visible = false;
            PnlS1Top.Visible = true;
            PnlActiveS2.BackColor = Color.Transparent;
            PnlActiveS1.BackColor = Color.Lime;
            Btns1.Visible = true;
            BtnS2.Visible = false;
            BtnS12.Visible = false;
            if (RdbCelsius1.Checked == true)
            {
                GrafiekTemperatuur.Visible = true;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekFarhenheidAll.Visible = false;
                GrafiekKelvinAll.Visible = false;

                RdbFarhenheid1.Checked = false;
                RdbKelvin1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;

                TxbSelectedSensor2.Clear();
                TbMinimumTemperatuur2.Clear();
                TbMaximumTemperatuur2.Clear();
            }
            SelectLocMinMaxCelsius1();
            SelectLocMinMaxCelsius2();
            GrafiekTemperatuur.ResetAutoValues();
            DateTime AToC1 = DtpTot.Value;
            DateTime AFromC1 = DtpVan.Value;
            GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempC1(AFromC1, AToC1);
            GrafiekTemperatuur.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempC1(AFromC1, AToC1);
            if (GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum == GrafiekTemperatuur.ChartAreas[0].AxisY.Minimum)
            {
                GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum = GrafiekTemperatuur.ChartAreas[0].AxisY.Minimum + 10;
            }
            GrafiekTemperatuur.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            GrafiekTemperatuur.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
        }
        // sensor 1 einde
        // sensor 2 begin
        private void RdbCelsius2_Click(object sender, EventArgs e)
        {
            PnlS2Top.Visible = true;
            PnlS12Top.Visible = false;
            PnlS1Top.Visible = false;
            PnlActiveS1.BackColor = Color.Transparent;
            PnlActiveS2.BackColor = Color.Lime;
            BtnS2.Visible = true;
            Btns1.Visible = false;
            BtnS12.Visible = false;
            if (RdbCelsius2.Checked == true)
            {
                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = true;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin1.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvin2.Checked = false;

                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = true;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekKelvinAll.Visible = false;
                GrafiekFarhenheidAll.Visible = false;

                TxbSelectedSensor1.Clear();
                TbMinimumTemperatuur1.Clear();
                TbMaximumTemperatuur1.Clear();

                SelectLocMinMaxCelsius2();
                SelectLocMinMaxCelsius1();
                GrafiekTemperatuur2.ResetAutoValues();
                DateTime AToC2 = DtpTot.Value;
                DateTime AFromC2 = DtpVan.Value;
                GrafiekTemperatuur2.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempC2(AFromC2, AToC2);
                GrafiekTemperatuur2.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempC2(AFromC2, AToC2);
                if (GrafiekTemperatuur2.ChartAreas[0].AxisY.Maximum == GrafiekTemperatuur2.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekTemperatuur2.ChartAreas[0].AxisY.Maximum = GrafiekTemperatuur2.ChartAreas[0].AxisY.Minimum + 10;
                }
                GrafiekTemperatuur2.ChartAreas[0].AxisY.LabelStyle.Format = "0";
                GrafiekTemperatuur2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
                BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            }
        }
        private void RdbKelvin2_Click(object sender, EventArgs e)
        {
            PnlS12Top.Visible = false;
            PnlS2Top.Visible = true;
            PnlS1Top.Visible = false;
            PnlActiveS1.BackColor = Color.Transparent;
            PnlActiveS2.BackColor = Color.Lime;
            BtnS2.Visible = true;
            Btns1.Visible = false;
            BtnS12.Visible = false;
            if (RdbKelvin2.Checked == true)
            {
                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin1.Checked = false;
                RdbKelvin1.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;

                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = true;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekKelvinAll.Visible = false;
                GrafiekFarhenheidAll.Visible = false;

                TxbSelectedSensor1.Clear();
                TbMinimumTemperatuur1.Clear();
                TbMaximumTemperatuur1.Clear();
            }
            SelectLocMinMaxKelvin2();
            SelectLocMinMaxKelvin1();
            GrafiekKelvin2.ResetAutoValues();
            DateTime AToK2 = DtpTot.Value;
            DateTime AFromK2 = DtpVan.Value;
            GrafiekKelvin2.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempK2(AFromK2, AToK2);
            GrafiekKelvin2.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempK2(AFromK2, AToK2);
            if (GrafiekKelvin2.ChartAreas[0].AxisY.Maximum == GrafiekKelvin2.ChartAreas[0].AxisY.Minimum)
            {
                GrafiekKelvin2.ChartAreas[0].AxisY.Maximum = GrafiekKelvin2.ChartAreas[0].AxisY.Minimum + 10;
            }
            GrafiekKelvin2.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            GrafiekKelvin2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
        }
        private void RdbFarhenheid2_Click(object sender, EventArgs e)
        {
            PnlS12Top.Visible = false;
            PnlS2Top.Visible = true;
            PnlS1Top.Visible = false;
            PnlActiveS1.BackColor = Color.Transparent;
            PnlActiveS2.BackColor = Color.Lime;
            BtnS2.Visible = true;
            Btns1.Visible = false;
            BtnS12.Visible = false;
            if (RdbFarhenheid2.Checked == true)
            {
                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = true;
                GrafiekCelsiusAll.Visible = true;
                GrafiekFarhenheidAll.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekKelvinAll.Visible = false;

                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = true;
                RdbKelvin1.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;

                TxbSelectedSensor1.Clear();
                TbMinimumTemperatuur1.Clear();
                TbMaximumTemperatuur1.Clear();
            }
            SelectLocMinMaxFarhenheid2();
            SelectLocMinMaxFarhenheid1();

            grafiekFarhenheid2.ResetAutoValues();

            DateTime AToF2 = DtpTot.Value;
            DateTime AFromF2 = DtpVan.Value;

            grafiekFarhenheid2.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempF2(AFromF2, AToF2);
            grafiekFarhenheid2.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempF2(AFromF2, AToF2);

            if (grafiekFarhenheid2.ChartAreas[0].AxisY.Maximum == grafiekFarhenheid2.ChartAreas[0].AxisY.Minimum)
            {
                grafiekFarhenheid2.ChartAreas[0].AxisY.Maximum = grafiekFarhenheid2.ChartAreas[0].AxisY.Minimum + 10;
            }

            grafiekFarhenheid2.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            grafiekFarhenheid2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";

            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
        }
        // sensor 2 einde
        // sensor All begin
        private void RdbCelsiusAll_Click(object sender, EventArgs e)
        {
            PnlS12Top.Visible = true;
            PnlS2Top.Visible = false;
            PnlS1Top.Visible = false;
            BtnS2.Visible = false;
            Btns1.Visible = false;
            BtnS12.Visible = true;
            PnlActiveS1.BackColor = Color.Transparent;
            PnlActiveS2.BackColor = Color.Transparent;
            if (RdbCelsiusAll.Checked == true)
            {
                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin1.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = true;

                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = true;
                GrafiekFarhenheidAll.Visible = false;
                GrafiekKelvinAll.Visible = false;

                GrafiekCelsiusAll.ResetAutoValues();

                DateTime AToCAll = DtpTot.Value;
                DateTime AFromCAll = DtpVan.Value;

                GrafiekCelsiusAll.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempCAll(AFromCAll, AToCAll);
                GrafiekCelsiusAll.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempCAll(AFromCAll, AToCAll);

                if (GrafiekCelsiusAll.ChartAreas[0].AxisY.Maximum == GrafiekCelsiusAll.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekCelsiusAll.ChartAreas[0].AxisY.Maximum = GrafiekCelsiusAll.ChartAreas[0].AxisY.Minimum + 10;
                }

                GrafiekCelsiusAll.ChartAreas[0].AxisY.LabelStyle.Format = "0";
                GrafiekCelsiusAll.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";

                BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            }
        }
        private void RdbKelvinAll_Click(object sender, EventArgs e)
        {
            PnlS12Top.Visible = true;
            PnlS2Top.Visible = false;
            PnlS1Top.Visible = false;
            BtnS2.Visible = false;
            Btns1.Visible = false;
            BtnS12.Visible = true;
            PnlActiveS1.BackColor = Color.Transparent;
            PnlActiveS2.BackColor = Color.Transparent;
            if (RdbKelvinAll.Checked == true)
            {
                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin1.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbKelvinAll.Checked = true;
                RdbFarhenheidAll.Checked = false;

                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekKelvinAll.Visible = true;
                GrafiekFarhenheidAll.Visible = false;

                GrafiekKelvinAll.ResetAutoValues();

                DateTime AToKAll = DtpTot.Value;
                DateTime AFromKAll = DtpVan.Value;
                GrafiekKelvinAll.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempKAll(AFromKAll, AToKAll);
                GrafiekKelvinAll.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempKAll(AFromKAll, AToKAll);
                if (GrafiekKelvinAll.ChartAreas[0].AxisY.Maximum == GrafiekKelvinAll.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekKelvinAll.ChartAreas[0].AxisY.Maximum = GrafiekKelvinAll.ChartAreas[0].AxisY.Minimum + 10;
                }
                GrafiekKelvinAll.ChartAreas[0].AxisY.LabelStyle.Format = "0";
                GrafiekKelvinAll.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
                BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            }
        }
        private void RdbFarhenheidAll_Click(object sender, EventArgs e)
        {
            PnlS12Top.Visible = true;
            PnlS2Top.Visible = false;
            PnlS1Top.Visible = false;
            BtnS2.Visible = false;
            Btns1.Visible = false;
            BtnS12.Visible = true;
            PnlActiveS1.BackColor = Color.Transparent;
            PnlActiveS2.BackColor = Color.Transparent;
            if (RdbFarhenheidAll.Checked == true)
            {
                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin1.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = true;
                RdbKelvinAll.Checked = false;

                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekKelvinAll.Visible = false;
                GrafiekFarhenheidAll.Visible = true;

                GrafiekFarhenheidAll.ResetAutoValues();

                DateTime AToFAll = DtpTot.Value;
                DateTime AFromFAll = DtpVan.Value;
                GrafiekFarhenheidAll.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempFAll(AFromFAll, AToFAll);
                GrafiekFarhenheidAll.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempFAll(AFromFAll, AToFAll);
                if (GrafiekFarhenheidAll.ChartAreas[0].AxisY.Maximum == GrafiekFarhenheidAll.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekFarhenheidAll.ChartAreas[0].AxisY.Maximum = GrafiekFarhenheidAll.ChartAreas[0].AxisY.Minimum + 10;
                }
                GrafiekFarhenheidAll.ChartAreas[0].AxisY.LabelStyle.Format = "0";
                GrafiekFarhenheidAll.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
                BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            }
        }
        // sensor All einde
        // functies
        public bool Nederlands
        {
            get { return nederlandsToolStripMenuItem.Checked; }
            set { nederlandsToolStripMenuItem.Checked = value; }
        }
        public bool Duits
        {
            get { return duitsToolStripMenuItem.Checked; }
            set { duitsToolStripMenuItem.Checked = value; }
        }
        public bool Engels
        {
            get { return engelsToolStripMenuItem.Checked; }
            set { engelsToolStripMenuItem.Checked = value; }
        }
        public void CheckLastDataDB()
        {
            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT DateTime FROM tbl_Temperature WHERE ID = (SELECT MAX(ID)  FROM tbl_Temperature)");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    string DateTime = Convert.ToString(ds.Tables[0].Rows[0]["DateTime"]);
                    TxbLastConnTime.Text = DateTime;
                    connection.Close();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }
        public void SettingsDtpVan()
        {
            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT DateTime FROM tbl_Temperature WHERE ID = (SELECT MIN(ID)  FROM tbl_Temperature)");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateTime"]);
                    //DateTime Dt1 = DateTime.Now.AddMinutes(-5);
                    DateTime Dt1 = DtpTot.Value;
                    DtpVan.MaxDate = Dt1;
                    DtpVan.MinDate = dt;
                    connection.Close();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }
        public void SettingsDtpTot()
        {
            //using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            //{
            //    SqlCommand cmd;
            //    connection.Open();
            //    cmd = connection.CreateCommand();
            //    cmd.CommandText = ("SELECT DateTime FROM tbl_Temperature WHERE ID = (SELECT MAX(ID)  FROM tbl_Temperature)");
            //    SqlDataAdapter adap = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    adap.Fill(ds);
            //    DateTime dt = DtpVan.Value.AddMinutes(5);
            //    connection.Close();
            //    DateTime Dt1 = DateTime.Now.AddMinutes(0);

            //    DtpTot.MinDate = dt;
            //    //DtpTot.MaxDate = Dt1;
            //}
        }
        public void SelectLocMinMaxCelsius1()
        {
            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT Location, MinimumTemperatureCelsius, MaximumTemperatureCelsius FROM tbl_Location WHERE id=1");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    cmd.ExecuteNonQuery();
                    string MinimumTemperatureCelsius = Convert.ToString(ds.Tables[0].Rows[0]["MinimumTemperatureCelsius"]);
                    string MaximumTemperatureCelsius = Convert.ToString(ds.Tables[0].Rows[0]["MaximumTemperatureCelsius"]);
                    string LocationSensor1Celsius = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
                    TbMinimumTemperatuur1.Text = MinimumTemperatureCelsius;
                    TbMaximumTemperatuur1.Text = MaximumTemperatureCelsius;
                    TxbSelectedSensor1.Text = LocationSensor1Celsius;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }
        public void SelectLocMinMaxKelvin1()
        {
            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT Location, MinimumTemperatureKelvin, MaximumTemperatureKelvin FROM tbl_Location WHERE id=1");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    string MinimumTemperatureKelvin = Convert.ToString(ds.Tables[0].Rows[0]["MinimumTemperatureKelvin"]);
                    string MaximumTemperatureKelvin = Convert.ToString(ds.Tables[0].Rows[0]["MaximumTemperatureKelvin"]);
                    string LocationSensor1Kelvin = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
                    TbMinimumTemperatuur1.Text = MinimumTemperatureKelvin;
                    TbMaximumTemperatuur1.Text = MaximumTemperatureKelvin;
                    TxbSelectedSensor1.Text = LocationSensor1Kelvin;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void SelectLocMinMaxFarhenheid1()
        {
            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT Location, MinimumTemperatureFarhenheid, MaximumTemperatureFarhenheid FROM tbl_Location WHERE id=1");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    string MinimumTemperatureFarhenheid = Convert.ToString(ds.Tables[0].Rows[0]["MinimumTemperatureFarhenheid"]);
                    string MaximumTemperatureFarhenheid = Convert.ToString(ds.Tables[0].Rows[0]["MaximumTemperatureFarhenheid"]);
                    string LocationSensorFarhenheid = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
                    TbMinimumTemperatuur1.Text = MinimumTemperatureFarhenheid;
                    TbMaximumTemperatuur1.Text = MaximumTemperatureFarhenheid;
                    TxbSelectedSensor1.Text = LocationSensorFarhenheid;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }
        public void SelectLocMinMaxCelsius2()
        {
            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT Location, MinimumTemperatureCelsius, MaximumTemperatureCelsius FROM tbl_Location WHERE id=2");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    string MinimumTemperatureCelsius = Convert.ToString(ds.Tables[0].Rows[0]["MinimumTemperatureCelsius"]);
                    string MaximumTemperatureCelsius = Convert.ToString(ds.Tables[0].Rows[0]["MaximumTemperatureCelsius"]);
                    string LocationSensor2Celsius = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
                    TbMinimumTemperatuur2.Text = MinimumTemperatureCelsius;
                    TbMaximumTemperatuur2.Text = MaximumTemperatureCelsius;
                    TxbSelectedSensor2.Text = LocationSensor2Celsius;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    throw;
                }
            }
        }
        public void SelectLocMinMaxKelvin2()
        {
            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT Location, MinimumTemperatureKelvin, MaximumTemperatureKelvin FROM tbl_Location WHERE id=2");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    string MinimumTemperatureKelvin = Convert.ToString(ds.Tables[0].Rows[0]["MinimumTemperatureKelvin"]);
                    string MaximumTemperatureKelvin = Convert.ToString(ds.Tables[0].Rows[0]["MaximumTemperatureKelvin"]);
                    string LocationSensor2Kelvin = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
                    TbMinimumTemperatuur2.Text = MinimumTemperatureKelvin;
                    TbMaximumTemperatuur2.Text = MaximumTemperatureKelvin;
                    TxbSelectedSensor2.Text = LocationSensor2Kelvin;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    throw;
                }
            }
        }
        public void SelectLocMinMaxFarhenheid2()
        {
            using (SqlConnection connection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    SqlCommand cmd;
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = ("SELECT Location, MinimumTemperatureFarhenheid, MaximumTemperatureFarhenheid FROM tbl_Location WHERE id=2");
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    string MinimumTemperatureFarhenheid = Convert.ToString(ds.Tables[0].Rows[0]["MinimumTemperatureFarhenheid"]);
                    string MaximumTemperatureFarhenheid = Convert.ToString(ds.Tables[0].Rows[0]["MaximumTemperatureFarhenheid"]);
                    string LocationSensor2Farhenheid = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
                    TbMinimumTemperatuur2.Text = MinimumTemperatureFarhenheid;
                    TbMaximumTemperatuur2.Text = MaximumTemperatureFarhenheid;
                    TxbSelectedSensor2.Text = LocationSensor2Farhenheid;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    throw;
                }
            }
        }
        public bool CheckSelectRdbCelsius1
        {
            get { return RdbCelsius1.Checked; }
            set { RdbCelsius1.Checked = value; }
        }
        public bool CheckSelectRdbKelvin1
        {
            get { return RdbKelvin1.Checked; }
            set { RdbKelvin1.Checked = value; }
        }
        public bool CheckSelectRdbFarhenheid1
        {
            get { return RdbFarhenheid1.Checked; }
            set { RdbFarhenheid1.Checked = value; }
        }
        public bool CheckSelectRdbCelsius2
        {
            get { return RdbCelsius2.Checked; }
            set { RdbCelsius2.Checked = value; }
        }
        public bool CheckSelectRdbKelvin2
        {
            get { return RdbKelvin2.Checked; }
            set { RdbKelvin2.Checked = value; }
        }
        public bool CheckSelectRdbFarhenheid2
        {
            get { return RdbFarhenheid2.Checked; }
            set { RdbFarhenheid2.Checked = value; }
        }
        public void SetTitleSensors()
        {
            grafiekFarhenheid1.Titles.Add("Sensor 1");
            GrafiekKelvin1.Titles.Add("Sensor 1");
            GrafiekTemperatuur.Titles.Add("Sensor 1");

            GrafiekTemperatuur2.Titles.Add("Sensor 2");
            grafiekFarhenheid2.Titles.Add("Sensor 2");
            GrafiekKelvin2.Titles.Add("Sensor 2");

            GrafiekCelsiusAll.Titles.Add("Sensor 1 en 2");
            GrafiekFarhenheidAll.Titles.Add("Sensor 1 en 2");
            GrafiekKelvinAll.Titles.Add("Sensor 1 en 2");
        }
        public void SetTitleAxis()
        {
            grafiekFarhenheid1.ChartAreas[0].AxisX.Title = "Datum en tijd";
            grafiekFarhenheid1.ChartAreas[0].AxisY.Title = "Temperatuur Farhenheid";
            GrafiekTemperatuur.ChartAreas[0].AxisX.Title = "Datum en tijd";
            GrafiekTemperatuur.ChartAreas[0].AxisY.Title = "Temperatuur Celsius";
            GrafiekKelvin1.ChartAreas[0].AxisX.Title = "Datum en tijd";
            GrafiekKelvin1.ChartAreas[0].AxisY.Title = "Temperatuur Kelvin";

            grafiekFarhenheid2.ChartAreas[0].AxisX.Title = "Datum en tijd";
            grafiekFarhenheid2.ChartAreas[0].AxisY.Title = "Temperatuur Farhenheid";
            GrafiekTemperatuur2.ChartAreas[0].AxisX.Title = "Datum en tijd";
            GrafiekTemperatuur2.ChartAreas[0].AxisY.Title = "temperatuur Celsius";
            GrafiekKelvin2.ChartAreas[0].AxisX.Title = "Datum en tijd";
            GrafiekKelvin2.ChartAreas[0].AxisY.Title = "Temperatuur Kelvin";

            GrafiekFarhenheidAll.ChartAreas[0].AxisX.Title = "Datum en tijd";
            GrafiekFarhenheidAll.ChartAreas[0].AxisY.Title = "Temperatuur Farhenheid";
            GrafiekCelsiusAll.ChartAreas[0].AxisX.Title = "Datum en tijd";
            GrafiekCelsiusAll.ChartAreas[0].AxisY.Title = "Temperatuur Celsius";
            GrafiekKelvinAll.ChartAreas[0].AxisX.Title = "Datum en tijd";
            GrafiekKelvinAll.ChartAreas[0].AxisY.Title = "Temperatuur Kelvin";
        }
        public void MinutenToolStrip()
        {
            try
            {
                if (minutenToolStripMenuItem.Checked == true)
                {
                    minutenToolStripMenuItem.Checked = true;
                    minutenToolStripMenuItem1.Checked = false;
                    minutenToolStripMenuItem2.Checked = false;

                    minutenToolStripMenuItem.PerformClick();
                }
                if (minutenToolStripMenuItem1.Checked == true)
                {
                    minutenToolStripMenuItem.Checked = false;
                    minutenToolStripMenuItem1.Checked = true;
                    minutenToolStripMenuItem2.Checked = false;

                    minutenToolStripMenuItem1.PerformClick();
                }
                if (minutenToolStripMenuItem2.Checked == true)
                {
                    minutenToolStripMenuItem.Checked = false;
                    minutenToolStripMenuItem1.Checked = false;
                    minutenToolStripMenuItem2.Checked = true;

                    minutenToolStripMenuItem2.PerformClick();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        public List<string> SelectSendEmail()
        {
            using (SqlConnection connection2 = new SqlConnection(MyConnectionString2))
            {
                connection2.Open();
                SqlCommand command;

                List<string> LResult = new List<string>();
                try
                {
                    command = connection2.CreateCommand();
                    command.CommandText = "SELECT (Email) FROM tbl_EmailAdressWFapp WHERE Status='Ingeschakeld'";
                    command.ExecuteNonQuery();
                    SqlDataAdapter adap = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string LVal = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                        LResult.Add(LVal);
                    }
                    return LResult;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                connection2.Close();
                return LResult;
            }
        }
        public async Task<string> HTTPPost(string ARequest, string AParams)
        {
            if (NextMailAllowed <= DateTime.Now)
            {
                NextMailAllowed = DateTime.Now.AddMinutes(15);
            }
            else
            {
                return "";
            }

            string BasePath = "http://api.pasys.nl/msgcenter/api/MsgJob/PostMsgJob";
            string LPath = BasePath + ARequest;
            StringContent S = new StringContent(AParams, Encoding.UTF8, "application/json");
            HttpResponseMessage HR = await HC.PostAsync(BasePath, S);
            try
            {
                if (HR.IsSuccessStatusCode)
                {
                    string HCRes = await HR.Content.ReadAsStringAsync();
                    return HCRes;
                }
                else
                {
                    return "FOUT !";
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return "";
            }
        }
        public async Task<string> SendEMail()
        {
            HC = new HttpClient();
            HC.DefaultRequestHeaders.Accept.Clear();
            HC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BasePath = ConfigurationManager.AppSettings["APIBasePath"];
            JavaScriptSerializer JS = new JavaScriptSerializer();

            List<string> FromAddrs = new List<string>();
            string FromAddr = "1028727@idcollege.nl";
            FromAddrs.Add(FromAddr);

            List<string> ToAddrs = SelectSendEmail();

            List<string> CCAddrs = new List<string>();

            List<string> BCCAddrs = new List<string>();

            RootObject rootObject = new RootObject
            {
                Id = 0,
                Addr_from = FromAddrs,
                Addr_to = ToAddrs,
                Addr_cc = CCAddrs,
                Addr_bcc = BCCAddrs,
                Subject = "Waarschuwing!",
                Body = "Er is een probleem met de service" + "<br> Controleer of de service(paSTempLog) 'gestart' is." + "<br> Controleer het logboek voor meer informatie!",
                Description = "Controleer verbinding service!",
                Eventtype_ad = "EMAIL",
                Docref_ad = "1",
                Rel_ad = "0",
                Msg_status_id = 0,
                Dt_sent = DateTime.Now,
                SendLog = "string",
                Dt_created = DateTime.Now,
                Dt_modified = DateTime.Now,
                Syshumres_id = 0,
                Systerminal_id = 0,
                Syscompany_id = 0
            };
            string json = new JavaScriptSerializer().Serialize(rootObject);
            return await HTTPPost("a", json);
        }
        public void ExportDataSetToExcel(DataSet ds)
        {
            app = new Excel.Application
            {
                Visible = true
            };
            workbook = app.Workbooks.Add(1);
            worksheet = (Excel.Worksheet)workbook.Sheets[1];

            foreach (DataTable table in ds.Tables)
            {
                worksheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        worksheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }
        }
        public IEnumerable<Control> GetAllControls(Control root)
        {
            foreach (Control control in root.Controls)
            {
                foreach (Control child in GetAllControls(control))
                {
                    yield return child;
                }
            }
            yield return root;
        }
    }
}