using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Net.Http;
using System.Configuration;
using System.IO;
using pasTemp;
using System.Management;
using System.Linq;
using paSSQL;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Vertalen;
using log4net;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace WindowsFormsApp1
{
    public partial class Temperatuur : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Help help = new Help();
        private Contact contact = new Contact();
        private SettingsSensor1 SettingsSensor1 = new SettingsSensor1();
        private SettingsSensor2 SettingsSensor2 = new SettingsSensor2();
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
            // waar het scherm opent met opstarten (locatie)
            this.Location = new Point(400, 100);
            // controleerd of er verbinding is met de database
            IsServerConnected();
        }
        // wordt gedaan bij afsluiten applicatie
        private void Temperatuur_FormClosed(object sender, FormClosedEventArgs e)
        {
            // ingevoerde datum en tijd worden opgeslagen
            Properties.Settings.Default.DtpVan = DtpVan.Value;
            Properties.Settings.Default.Save();
            log.Info("Save settings application");
            // applicatie sluiten
            log.Info("Application exit");
            Application.Exit();            
        }
        // wordt geladen als de gebruiker inlogt
        private void Temperatuur_Load(object sender, EventArgs e)
        {
            // laadt alle tekst in het DE,NL,EN
            Vertaal.LoadTranslation();
            // textbox kan niet worden bewerkt door gebruiker
            TxbLastConnTime.Enabled = false;
            // textbox kan niet worden bewerkt door gebruiker
            TxbUserLoggedIn.Enabled = false;
            // textbox kan niet worden bewerkt door gebruiker
            TxbPcUser.Enabled = false;
            // textbox kan niet worden bewerkt door gebruiker
            TxbSelectedSensor1.Enabled = false;
            //achtergrond kleur naar wit
            TxbSelectedSensor1.BackColor = Color.White;
            // textbox kan niet worden bewerkt door gebruiker
            TbMinimumTemperatuur1.Enabled = false;
            // achtergrond kleur naar wit
            TbMaximumTemperatuur1.BackColor = Color.White;
            // textbox kan niet worden bewerkt door gebruiker
            TbMaximumTemperatuur1.Enabled = false;
            // achtergrond kleur naar wit
            TbMaximumTemperatuur1.BackColor = Color.White;
            // textbox kan niet worden bewerkt door gebruiker
            TxbSelectedSensor2.Enabled = false;
            // achtergrond kleur naar wit
            TxbSelectedSensor2.BackColor = Color.White;
            // textbox kan niet worden bewerkt door gebruiker
            TbMinimumTemperatuur2.Enabled = false;
            // achtergrond kleur naar wit
            TbMaximumTemperatuur2.BackColor = Color.White;
            // textbox kan niet worden bewerkt door gebruiker
            TbMaximumTemperatuur2.Enabled = false;
            // achtergrond kleur naar wit
            TbMaximumTemperatuur2.BackColor = Color.White;
        }
        private void Temperatuur_Shown(object sender, EventArgs e)
        {
            log.Info("Start Temperatuur_Shown");
            TbDigiClock.Enabled = false;
            log.Info("TbDigiClock enabled");
            TimerDigiClock.Enabled = true;
            log.Info("TimerDigiClock enabled");
            TimerDigiClock.Interval = 500;
            log.Info("TimerDigiclock interval set");           
            CheckLastDataDB();
            log.Info("CheckLastDataDB (F)");
            BtnOpvragenVanTot.Focus();
            log.Info("BtnOpvragenVanTot focus");
            BtnStatusSqlConnection.PerformClick();
            log.Info("BtnStatusSQLConnection performclick");    
            uitschakelenToolStripMenuItem.PerformClick();
            log.Info("ToolstripMenuItem: Uitschakelen performclick");
            RdbCelsius1.PerformClick();
            log.Info("RdbCelsius1 performclick");
            minimaalToolStripMenuItem1.Checked = true;
            log.Info("Toostripmenuitem: Minimaal = checked");
            minimaalToolStripMenuItem1.PerformClick();
            log.Info("Toostripmenuitem: Minimaal performclick");
            BtnTimerStop1.PerformClick();
            log.Info("BtnTimerStop performclick");
            nederlandsToolStripMenuItem.PerformClick();
            log.Info("ToolstripMenuItem: Nederlands performclick");
            SetTitleSensors();
            log.Info("SetTileSensors (F)");
            IsServerConnected();
            log.Info("IsServerConnected (F)");
            TimerStatusSQL.Enabled = true;
            log.Info("TimerStatusSql = enabled");
            TimerStatusSQL.Interval = 500;
            log.Info("TimerStatesSql interval set");
            bool laasteversie = true;
            if (laasteversie == false)
            {
                NewUpdate();
            }
            TxbUserLoggedIn.Text = Rights.username;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            log.Info("Get username from Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            string username = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];
            TxbPcUser.Text = username;
            log.Info("Username Application in Textbox TxbPcUser");
            try
            {
                DtpVan.Value = Properties.Settings.Default.DtpVan;
                log.Info("DtpVan value = Properties.Settings.Default");
                DtpTot.Value = DateTime.Now;
                log.Info("DtpTot value = datetime now");
            }
            catch
            {
                DtpVan.Value = DateTime.Today;
                log.Error("Dtpvan can't load datetime or there is no value");
            }
            Properties.Settings.Default.DtpVan = DtpVan.Value;
            Properties.Settings.Default.Save();

            DateTime AToC1 = DtpTot.Value;
            DateTime AFromC1 = DtpVan.Value;
            GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempC1(AFromC1, AToC1);
            log.Info("Load Y Axis for chart GrafiekTemperatuur");

            DtpVan.Format = DateTimePickerFormat.Custom;
            DtpVan.CustomFormat = "dd/MM/yyyy HH:mm";
            DtpTot.Format = DateTimePickerFormat.Custom;
            DtpTot.CustomFormat = "dd/MM/yyyy HH:mm";
            timer2.Enabled = true;
            log.Info("timer 2 enabled");
            if (Rights.rights == 0)
            {
                BtnLocatieSensorOpslaan.Visible = false;
                BtnSettingsSensor1.Visible = false;
                BtnSettingsSensor2.Visible = false;
                PnlActiveS1.Visible = false;
                PnlActiveS2.Visible = false;
                log.Info("Rights for admin loaded");
            }
            log.Info("Stop Temperatuur_Shown ");
            log.Info("");
        }
        // programmma stop
        // begin instellingen
            // achtergrond applicatie rood
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
            log.Info("Background color changed to red");
        }
            // achtergrond applicatie wit
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
            log.Info("Background color changed to white");
        }
            // achtergrond applicatie blauw
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
            log.Info("Background color changed to blue");
        }
            // achtegrond applicatie grijs
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
            log.Info("Background color changed to grey");
        }
            // achtergrond applicatie groen
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
            log.Info("Background color changed to green");
        }
            // achtergrond applicatie geel
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
            log.Info("Background color changed to yellow");
        }
            // achtergrond applicatie originele kleur
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
            log.Info("Background color changed to original");
        }
            // links naar website
        private void WebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.portaldemo.pasys/");
            log.Info("Open link to website");
        }
            // links naar appstore
        private void MobieleApplicatieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.portaldemo.pasys/");
            log.Info("Open link to mobile application");
        }
            // opent contact formulier
        private void ContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contact.Parentform1 = this;
            contact.ShowDialog();
            log.Info("Open contact.cs");
        }
            // opent help pagina
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help.Parentform2 = this;
            help.ShowDialog();
            log.Info("Open help.cs");
        }
            // screenshot van de grafiek opslaan als .png
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
                log.Info("Saved screenshot graph Celsius 1");
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
                log.Info("Saved screenshot graph Kelvin 1");
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
                log.Info("Saved screenshot graph Farhenheid 1");
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
                log.Info("Saved screenshot graph Celsius 2");
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
                log.Info("Saved screenshot graph Kelvin 2");
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
                log.Info("Saved screenshot graph Farhenheid 2");
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
                log.Info("Saved screenshot graph Celsius All");
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
                log.Info("Saved screenshot graph Kelvin all");
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
                log.Info("Saved screenshot graph Farhenheid all");
            }
        }
            // opent pagina gebruikers beheren
        private void GebruikersBeherenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gebruiker gebruikersBeheren = new Gebruiker();
            gebruikersBeheren.Show();
            log.Info("Open Gebruiker.cs");
        }
            // fullscreen
        private void FullScreenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            fullScreenToolStripMenuItem2.Checked = true;
            maximaalToolStripMenuItem1.Checked = false;
            minimaalToolStripMenuItem1.Checked = false;
            log.Info("Open full screen");
        }
            // maximaliseer scherm
        private void MaximaalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            fullScreenToolStripMenuItem2.Checked = false;
            maximaalToolStripMenuItem1.Checked = true;
            minimaalToolStripMenuItem1.Checked = false;
            log.Info("Maximize screen");
        }
            // minimaliseer scherm
        private void MinimaalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            fullScreenToolStripMenuItem2.Checked = false;
            maximaalToolStripMenuItem1.Checked = false;
            minimaalToolStripMenuItem1.Checked = true;
            log.Info("Minimize screen");
        }
            // select eerste datetime record uit database
        private void DtpVan_ValueChanged(object sender, EventArgs e)
        {
            SettingsDtpVan();
            log.Info("SettingsDtpVan (F)");
        }
            // select laatste datatime record uit database
        private void DtpTot_ValueChanged(object sender, EventArgs e)
        {
            SettingsDtpTot();
            log.Info("SettingsDtpTot (F)");
        }
            // inschakelen melding ontvangen na 15 min geen verbinding
        private async void MinutenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Timer15ServiceAlert.Enabled = true;
            Timer15ServiceAlert.Interval = 900000;
            Timer30ServiceAlert.Enabled = false;
            Timer60ServiceAlert.Enabled = false;
            log.Info("AlertService15: Interval set");
            log.Info("AlertService30, AlertService60 disabled");
            minutenToolStripMenuItem.Checked = true;
            minutenToolStripMenuItem1.Checked = false;
            minutenToolStripMenuItem2.Checked = false;
            uitschakelenToolStripMenuItem.Checked = false;
            log.Info("AlertService15: checked");
            if (uitschakelenToolStripMenuItem.Checked == true)
            {
                uitschakelenToolStripMenuItem.Text = "Uitgeschakeld";
                log.Info("AlertService15: text = Uitgeschakeld");
            }
            if (uitschakelenToolStripMenuItem.Checked == false)
            {
                uitschakelenToolStripMenuItem.Text = "Uitschakelen";
                log.Info("AlertService15: text = Uitschakelen");
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
                    log.Info("Select datetime");
                    DateTime TimeFromDB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateTime"]).AddMinutes(15);
                    log.Info("Datetime +15min");
                    connection.Close();
                    DateTime TimeNow = DateTime.Now;
                    if (TimeFromDB < TimeNow)
                    {
                        MessageBox.Show("Problemen met de service, 15 minuten geen nieuwe data!", "WAARSCHUWING!");
                        log.Info("Problems with the service, 15min no new data!");
                        await SendEMail();
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    log.Error("Can't load datetime", E);
                }
            }
        }
            // inschakelen melding ontvangen na 30 min geen verbinding
        private async void MinutenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Timer15ServiceAlert.Enabled = false;
            Timer30ServiceAlert.Enabled = true;
            Timer30ServiceAlert.Interval = 1800000;
            Timer60ServiceAlert.Enabled = false;
            log.Info("AlertService30: Interval set");
            log.Info("AlertService15, AlertService60 disabled");
            minutenToolStripMenuItem.Checked = false;
            minutenToolStripMenuItem1.Checked = true;
            minutenToolStripMenuItem2.Checked = false;
            uitschakelenToolStripMenuItem.Checked = false;
            log.Info("AlertService30: checked");
            if (uitschakelenToolStripMenuItem.Checked == true)
            {
                uitschakelenToolStripMenuItem.Text = "Uitgeschakeld";
                log.Info("AlertService30: text = Uitgeschakeld");
            }
            if (uitschakelenToolStripMenuItem.Checked == false)
            {
                uitschakelenToolStripMenuItem.Text = "Uitschakelen";
                log.Info("AlertService30: text = Uischakelen");
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
                    log.Info("Select datetime");
                    DateTime TimeFromDB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateTime"]).AddMinutes(30);
                    log.Info("Datetime +30min");
                    connection.Close();
                    DateTime TimeNow = DateTime.Now;

                    if (TimeFromDB < TimeNow)
                    {
                        MessageBox.Show("Problemen met de service, 30 minuten geen nieuwe data!", "WAARSCHUWING!");
                        log.Info("Problems with the service, 30min no new data!");
                        await SendEMail();
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    log.Error("Can't load datetime, E");
                }
            }
        }
            // inschakelen melding ontvangen na 60 min of meer geen verbinding
        private async void MinutenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Timer15ServiceAlert.Enabled = false;
            Timer30ServiceAlert.Enabled = false;
            Timer60ServiceAlert.Enabled = true;
            Timer60ServiceAlert.Interval = 3600000;
            log.Info("AlertService60: Interval set");
            log.Info("AlertService15, AlertService30 disabled");
            minutenToolStripMenuItem.Checked = false;
            minutenToolStripMenuItem1.Checked = false;
            minutenToolStripMenuItem2.Checked = true;
            uitschakelenToolStripMenuItem.Checked = false;
            log.Info("AlertService60: checked");
            if (uitschakelenToolStripMenuItem.Checked == true)
            {
                uitschakelenToolStripMenuItem.Text = "Uitgeschakeld";
                log.Info("AlertService60: text = Uitgeschakeld");
            }
            if (uitschakelenToolStripMenuItem.Checked == false)
            {
                uitschakelenToolStripMenuItem.Text = "Uitschakelen";
                log.Info("AlertService60: text = Uitschakelen");
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
                    log.Info("Select datetime");
                    DateTime TimeFromDB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateTime"]).AddMinutes(60);
                    connection.Close();
                    log.Info("Datetime +60min");
                    DateTime TimeNow = DateTime.Now;
                    if (TimeFromDB < TimeNow)
                    {
                        MessageBox.Show("Problemen met de service, 60 minuten (of meer) geen nieuwe data!", "WAARSCHUWING!");
                        log.Info("Problems with the service, 60min+ no new data!");
                        await SendEMail();
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    log.Error("Can't load datetime, E");
                }
            }
        }
            // meldingen uitschakelen
        private void UitschakelenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minutenToolStripMenuItem.Checked = false;
            minutenToolStripMenuItem1.Checked = false;
            minutenToolStripMenuItem2.Checked = false;
            uitschakelenToolStripMenuItem.Checked = true;
            log.Info("ToolstripMenuItem: Uitschakelen = checked");

            Timer15ServiceAlert.Enabled = false;
            Timer30ServiceAlert.Enabled = false;
            Timer60ServiceAlert.Enabled = false;

            if(uitschakelenToolStripMenuItem.Checked == true)
            {
                uitschakelenToolStripMenuItem.Text = "Uitgeschakeld";
                log.Info("ToolstripMenuItem: Uitschakelen text = Uitgeschakeld");
            }
            if(uitschakelenToolStripMenuItem.Checked == false)
            {
                uitschakelenToolStripMenuItem.Text = "Uitschakelen";
                log.Info("ToolstripMenuItem: Uitschakelen text = Uitschakelen");
            }
        }
            // vertaald applicatie naar het engels
        private void EngelsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            grafiekFarhenheid1.ChartAreas[0].AxisX.Title = "Date and time";
            grafiekFarhenheid1.ChartAreas[0].AxisY.Title = "Temperature Farhenheid";
            GrafiekTemperatuur.ChartAreas[0].AxisX.Title = "Date and time";
            GrafiekTemperatuur.ChartAreas[0].AxisY.Title = "Temperature Celsius";
            GrafiekKelvin1.ChartAreas[0].AxisX.Title = "Date and time";
            GrafiekKelvin1.ChartAreas[0].AxisY.Title = "Temperature Kelvin";

            grafiekFarhenheid2.ChartAreas[0].AxisX.Title = "Date and time";
            grafiekFarhenheid2.ChartAreas[0].AxisY.Title = "Temperature Farhenheid";
            GrafiekTemperatuur2.ChartAreas[0].AxisX.Title = "Date and time";
            GrafiekTemperatuur2.ChartAreas[0].AxisY.Title = "temperature Celsius";
            GrafiekKelvin2.ChartAreas[0].AxisX.Title = "Date and time";
            GrafiekKelvin2.ChartAreas[0].AxisY.Title = "Temperature Kelvin";

            GrafiekFarhenheidAll.ChartAreas[0].AxisX.Title = "Date and time";
            GrafiekFarhenheidAll.ChartAreas[0].AxisY.Title = "Temperature Farhenheid";
            GrafiekCelsiusAll.ChartAreas[0].AxisX.Title = "Date and time";
            GrafiekCelsiusAll.ChartAreas[0].AxisY.Title = "Temperature Celsius";
            GrafiekKelvinAll.ChartAreas[0].AxisX.Title = "Date and time";
            GrafiekKelvinAll.ChartAreas[0].AxisY.Title = "Temperature Kelvin";

            nederlandsToolStripMenuItem.Checked = false;
            duitsToolStripMenuItem.Checked = false;
            engelsToolStripMenuItem.Checked = true;
            foreach (Control control in Controls)
            {
                foreach (Control controls in GetAllControls(control))
                {
                    Vertaal.DoVertaalForm(controls, "EN");
                }
            }
            // for each in help pagina door de panels
            //foreach (Control control in )
            //{
            //    foreach (Control controls in GetAllControls(control))
            //    {
            //        Vertaal.DoVertaalForm(controls, "EN");
            //    }
            //}

            foreach (ToolStripMenuItem item in Instellingen.Items)
            {
                foreach (ToolStripMenuItem dditem in item.DropDownItems)
                {
                    Vertaal.VertaalContexMenueStrip(dditem, "EN");
                }

                foreach (ToolStripItem Item in Instellingen.Items)
                {
                    Vertaal.VertaalContexMenueStrip2(item, "EN");
                }
            }
        }
            // vertaald applicatie naar het nederlands
        private void NederlandsToolStripMenuItem_Click(object sender, EventArgs e)
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

            engelsToolStripMenuItem.Checked = false;
            duitsToolStripMenuItem.Checked = false;
            nederlandsToolStripMenuItem.Checked = true;
        }
            // vertaald applicatie naar het duits
        private void DuitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grafiekFarhenheid1.ChartAreas[0].AxisX.Title = "Datum und Uhrzeit";
            grafiekFarhenheid1.ChartAreas[0].AxisY.Title = "Temperatur Farhenheid";
            GrafiekTemperatuur.ChartAreas[0].AxisX.Title = "Datum und Uhrzeit";
            GrafiekTemperatuur.ChartAreas[0].AxisY.Title = "Temperatur Celsius";
            GrafiekKelvin1.ChartAreas[0].AxisX.Title = "Datum und Uhrzeit";
            GrafiekKelvin1.ChartAreas[0].AxisY.Title = "Temperatur Kelvin";

            grafiekFarhenheid2.ChartAreas[0].AxisX.Title = "Datum und Uhrzeit";
            grafiekFarhenheid2.ChartAreas[0].AxisY.Title = "Temperatur Farhenheid";
            GrafiekTemperatuur2.ChartAreas[0].AxisX.Title = "Datum und Uhrzeit";
            GrafiekTemperatuur2.ChartAreas[0].AxisY.Title = "temperatur Celsius";
            GrafiekKelvin2.ChartAreas[0].AxisX.Title = "Datum und Uhrzeit";
            GrafiekKelvin2.ChartAreas[0].AxisY.Title = "Temperatur Kelvin";

            GrafiekFarhenheidAll.ChartAreas[0].AxisX.Title = "Datum und Uhrzeit";
            GrafiekFarhenheidAll.ChartAreas[0].AxisY.Title = "Temperatur Farhenheid";
            GrafiekCelsiusAll.ChartAreas[0].AxisX.Title = "Datum und Uhrzeit";
            GrafiekCelsiusAll.ChartAreas[0].AxisY.Title = "Temperatur Celsius";
            GrafiekKelvinAll.ChartAreas[0].AxisX.Title = "Datum und Uhrzeit";
            GrafiekKelvinAll.ChartAreas[0].AxisY.Title = "Temperatur Kelvin";

            nederlandsToolStripMenuItem.Checked = false;
            engelsToolStripMenuItem.Checked = false;
            duitsToolStripMenuItem.Checked = true;
        }
            // exporteerd opgevraagde data naar een excel bestand
        private void ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("START export to Excel");
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
                        connection.Close();
                        log.Info("Exported data Celsius sensor 1 to Excel");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Error("Exported data Celsius sensor 1 to Excel", E);
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
                        connection.Close();
                        log.Info("Exported data Kelvin sensor 1 to Excel");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Error("Exported data Kelvin sensor 1 to Excel");
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
                        connection.Close();
                        log.Info("Exported data Farhenheid sensor 1 to Excel");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Info("Exported data Farhenheid sensor 1 to Excel", E);
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
                        connection.Close();
                        log.Info("Exported data Celsius sensor 2 to Excel");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Info("Exported data Celsius sensor 2 to Excel");
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
                        log.Info("Exported data Kelvin sensor 2 to Excel");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Info("Exported data Kelvin sensor 2 to Excel", E);
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
                        log.Info("Exported data Farhenheid sensor 2 to Excel");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Info("Exported data Farhenheid sensor 2 to Excel", E);
                    }
                }
            }

            if (RdbCelsiusAll.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT Id, TemperatureCelsius, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "');");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        ExportDataSetToExcel(ds);
                        connection.Close();
                        log.Info("Exported data Celsius sensor 1+2 to Excel");
                    }
                    catch (Exception E)
                    {
                        log.Error("Exported data Celsius sensor 1+2 to Excel", E);
                    }
                    
                }
            }
            if (RdbKelvinAll.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT Id, TemperatureKelvin, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "');");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        ExportDataSetToExcel(ds);
                        connection.Close();
                        log.Info("Exported data Kelvin sensor 1+2 to Excel");
                    }
                    catch(Exception E)
                    {
                        log.Error("Exported data Kelvin sensor 1+2 to Excel", E);
                    }
                }
            }
            if (RdbFarhenheidAll.Checked == true)
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString2))
                {
                    try
                    {
                        SqlCommand cmd;
                        connection.Open();
                        cmd = connection.CreateCommand();
                        cmd.CommandText = ("SELECT Id, TemperatureFarhenheid, DateTime, Location_Id FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + DtpVan.Value.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + DtpTot.Value.ToString("MM/dd/yyyy HH:mm:ss") + "');");
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        ExportDataSetToExcel(ds);
                        connection.Close();
                        log.Info("Exported data Farhenheid sensor 1+2 to Excel");
                    }
                    catch (Exception E)
                    {
                        log.Error("Exported data Farhenheid sensor 1+2 to Excel", E);
                    }
                }
            }
            log.Info("STOP export to Excel");
        }
        // einde instellingen
        // begin buttons
        private void BtnInstellingen_Click(object sender, EventArgs e)
        {
            Instellingen.Show(btnInstellingen.Left + this.Left, btnInstellingen.Top + btnInstellingen.Height + this.Top);
            log.Info("BtnInstellingen clicked");
        }
        private void BtnOpvragenVanTot_Click(object sender, EventArgs e)
        {
            log.Info("BtnOpvragenVanTot START");
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
                        connection.Close();
                        log.Info("Fill graph Celsius sensor 1");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Error("Fill graph Celsius sensor 1");
                    }
                }
                DateTime AToC1 = DtpTot.Value;
                DateTime AFromC1 = DtpVan.Value;
                GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempC1(AFromC1, AToC1);
                log.Info("Set Y axis max Celsius sensor 1");
                GrafiekTemperatuur.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempC1(AFromC1, AToC1);
                log.Info("Set Y axis min Celsius sensor 1");
                if (GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum == GrafiekTemperatuur.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum = GrafiekTemperatuur.ChartAreas[0].AxisY.Minimum + 2;
                    log.Error("Min,Max Y axis Celsius Sensor 1");
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
                        connection.Close();
                        log.Info("Fill graph Kelvin sensor 1");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Error("Fill graph Kelvin sensor 1");
                    }
                }
                DateTime AToK1 = DtpTot.Value;
                DateTime AFromK1 = DtpVan.Value;
                GrafiekKelvin1.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempK1(AFromK1, AToK1);
                log.Info("Set Y axis max Kelvin sensor 1");
                GrafiekKelvin1.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempK1(AFromK1, AToK1);
                log.Info("Set Y axis min Kelvin sensor 1");
                if (GrafiekKelvin1.ChartAreas[0].AxisY.Maximum == GrafiekKelvin1.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekKelvin1.ChartAreas[0].AxisY.Maximum = GrafiekKelvin1.ChartAreas[0].AxisY.Minimum + 2;
                    log.Error("Min,Max Y axis Kelvin Sensor 1");
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
                        connection.Close();
                        log.Info("Fill graph Farhenheid sensor 1");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Error("Fill graph Farhenheid sensor 1");
                    }
                }
                DateTime AToF1 = DtpTot.Value;
                DateTime AFromF1 = DtpVan.Value;
                grafiekFarhenheid1.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempF1(AFromF1, AToF1);
                log.Info("Set Y axis max Farhenheid sensor 1");
                grafiekFarhenheid1.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempF1(AFromF1, AToF1);
                log.Info("Set Y axis min Farhenheid sensor 1");
                if (grafiekFarhenheid1.ChartAreas[0].AxisY.Maximum == grafiekFarhenheid1.ChartAreas[0].AxisY.Minimum)
                {
                    grafiekFarhenheid1.ChartAreas[0].AxisY.Maximum = grafiekFarhenheid1.ChartAreas[0].AxisY.Minimum + 2;
                    log.Error("Min,Max Y axis Farhenheid Sensor 1");
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
                        connection.Close();
                        log.Info("Fill graph Celsius sensor 2");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Error("Fill graph Celsius sensor 2");
                    }
                    DateTime AToC2 = DtpTot.Value;
                    DateTime AFromC2 = DtpVan.Value;
                    GrafiekTemperatuur2.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempC2(AFromC2, AToC2);
                    log.Info("Set Y axis max Celsius sensor 2");
                    GrafiekTemperatuur2.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempC2(AFromC2, AToC2);
                    log.Info("Set Y axis min Celsius sensor 2");
                    if (GrafiekTemperatuur2.ChartAreas[0].AxisY.Maximum == GrafiekTemperatuur2.ChartAreas[0].AxisY.Minimum)
                    {
                        GrafiekTemperatuur2.ChartAreas[0].AxisY.Maximum = GrafiekTemperatuur2.ChartAreas[0].AxisY.Minimum + 2;
                        log.Error("Min,Max Y axis Celsius Sensor 2");
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
                        log.Info("Fill graph Kelvin sensor 2");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Error("Min,Max Y axis Kelvin Sensor 2");
                    }
                }
                DateTime AToK2 = DtpTot.Value;
                DateTime AFromK2 = DtpVan.Value;
                GrafiekKelvin2.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempK2(AFromK2, AToK2);
                log.Info("Set Y axis max Kelvin sensor 2");
                GrafiekKelvin2.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempK2(AFromK2, AToK2);
                log.Info("Set Y axis min Kelvin sensor 2");
                if (GrafiekKelvin2.ChartAreas[0].AxisY.Maximum == GrafiekKelvin2.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekKelvin2.ChartAreas[0].AxisY.Maximum = GrafiekKelvin2.ChartAreas[0].AxisY.Minimum + 2;
                    log.Error("Min,Max Y axis Kelvin Sensor 2");
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
                        log.Info("Fill graph Farhenheid sensor 2");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        log.Error("Fill graph Farhenheid sensor 2");
                    }
                }
                DateTime AToF2 = DtpTot.Value;
                DateTime AFromF2 = DtpVan.Value;
                grafiekFarhenheid2.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempF2(AFromF2, AToF2);
                log.Info("Set Y axis max Farhenheid sensor 2");
                grafiekFarhenheid2.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempF2(AFromF2, AToF2);
                log.Info("Set Y axis min Farhenheid sensor 2");
                if (grafiekFarhenheid2.ChartAreas[0].AxisY.Maximum == grafiekFarhenheid2.ChartAreas[0].AxisY.Minimum)
                {
                    log.Info("Set Y axis max Farhenheid sensor 2");
                    grafiekFarhenheid2.ChartAreas[0].AxisY.Maximum = grafiekFarhenheid2.ChartAreas[0].AxisY.Minimum + 2;
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
                                log.Error("Fill graph Celsius sensor 1+2");
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
                            log.Error("Y,X axis or databind Celsius sensor 1+2");
                            throw new InvalidOperationException(E.Message);
                        }
                        connection.Close();
                    }
                    GrafiekCelsiusAll.DataBind();
                    log.Info("Fill graph Celsius sensor 1+2");
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
                                log.Error("Y,X axis or databind Kelvin sensor 1+2");
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
                            log.Error("Y,X axis or databind Kelvin sensor 1+2");
                            throw new InvalidOperationException("fout");
                        }
                    }
                    GrafiekKelvinAll.DataBind();
                    connection.Close();
                    log.Info("Fill graph Kelvin sensor 1+2");
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
                                log.Error("Y,X axis or databind Farhenheid sensor 1+2");
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
                            log.Error("Y,X axis or databind Farhenheid sensor 1+2");
                            throw new InvalidOperationException("fout");
                        }
                    }

                    GrafiekFarhenheidAll.DataBind();
                    connection.Close();
                    log.Info("Fill graph Farhenheid sensor 1+2");
                }
            }
            log.Info("BtnOpvragenvanTot STOP");
        }
        private void BtnTimerStart_Click(object sender, EventArgs e)
        {
            log.Info("START BtnTimerStart");
            TimerDigiClock.Enabled = true;
            log.Info("TimerDigiClock enabled");
            BtnOpvragenVanTot.Visible = false;
            log.Info("BtnOpvragenVanTot visible = false");
            TbDigiClock.BackColor = Color.Lime;
            log.Info("DigiClock backgrond color = lime");
            BtnTimerStart1.Focus();
            log.Info("BtnTimerStart focus");
            DtpVan.Enabled = false;
            log.Info("DtpVan disabled");
            DtpTot.Enabled = false;
            log.Info("DtpTot disabled");
            Timer1_Tick(sender, e);
            log.Info("Timer1 tick");
            timer1.Enabled = true;
            log.Info("Timer1 enabled");
            timer1.Interval = 45000;
            log.Info("Timer1 interval set at 45000");
            if (nederlandsToolStripMenuItem.Checked == true)
            {
                log.Info("BtnTimerStart = Start en Stop Text to NL");
                BtnTimerStart1.Text = "Gestart";
                BtnTimerStop1.Text = "Stop";
            }
            if (engelsToolStripMenuItem.Checked == true)
            {
                log.Info("BtnTimerStart = Start en Stop Text to EN");
                BtnTimerStart1.Text = "Started";
                BtnTimerStop1.Text = "Stop";
            }
            if (duitsToolStripMenuItem.Checked == true)
            {
                log.Info("BtnTimerStart = Start en Stop Text to DE");
                BtnTimerStart1.Text = "Begonnen";
                BtnTimerStop1.Text = "Stopp";
            }
            BtnTimerStart1.Enabled = false;
            log.Info("BtnTimerStart1 disabled");
            BtnTimerStop1.Enabled = true;
            log.Info("BtnTimerStop enabled");
            log.Info("STOP BtnTimerStart");
        }
        private void BtnTimerStop_Click(object sender, EventArgs e)
        {
            log.Info("BtnTimerStop START");
            TimerDigiClock.Enabled = false;
            log.Info("TimerDigiClock disabled");
            TbDigiClock.BackColor = Color.Red;
            log.Info("DigiClock background color = red");
            TbDigiClock.Text = DateTime.Now.ToString("HH:mm:ss");
            log.Info("DigiClock = DateTime now");
            BtnOpvragenVanTot.Visible = true;
            log.Info("BtnOpvragenVanTot is visible");
            DtpVan.Enabled = true;
            log.Info("DtpVan enabled");
            DtpTot.Enabled = true;
            log.Info("DtpTot enabled");
            timer1.Enabled = false;
            if (nederlandsToolStripMenuItem.Checked == true)
            {
                BtnTimerStart1.Text = "Start";
                BtnTimerStop1.Text = "Gestopt";
                log.Info("BtnTimerStop = Start en Stop Text to NL");
            }
            if (engelsToolStripMenuItem.Checked == true)
            {
                BtnTimerStart1.Text = "Start";
                BtnTimerStop1.Text = "Stopped";
                log.Info("BtnTimerStop = Start en Stop Text to EN");
            }
            if (duitsToolStripMenuItem.Checked == true)
            {
                BtnTimerStart1.Text = "Start";
                BtnTimerStop1.Text = "Gestoppt";
                log.Info("BtnTimerStop = start en stop text to DE");
            }
            BtnTimerStart1.Enabled = true;
            log.Info("BtnTimerStart1 enabled");
            BtnTimerStop1.Enabled = false;
            log.Info("BtnTimerStop1 disabled");
            timer1.Stop();
            log.Info("Timer1 stop");
            log.Info("BtnTimerStop STOP");
        }
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            log.Info("Form 1 = hidden");
            Inloggen fl = new Inloggen();
            Vertaal.TrnRecList.Clear();
            log.Info("Clear translate list");
            fl.Show();
            log.Info("Show inloggen.cs");
        }
        private void BtnSettingsSensor1_Click(object sender, EventArgs e)
        {
            SettingsSensor1.Parentform1 = this;
            SettingsSensor1.ShowDialog();
            log.Info("Open SettingsSensor1.cs");
            SettingsSensor1.Location = new Point(1100, 305);
        }
        private void BtnSettingsSensor2_Click(object sender, EventArgs e)
        {
            SettingsSensor2.Parentform2 = this;
            SettingsSensor2.ShowDialog();
            log.Info("Open SettingsSensor2.cs");
            SettingsSensor2.Location = new Point(1100, 305);
        }
        private void BtnStatusSqlConnection_Click(object sender, EventArgs e)
        {
            if (IsServerConnected() == true)
            {
                BtnStatusSqlConnection.Text = "Actief";
                BtnStatusSqlConnection.BackColor = Color.Lime;
                //log.Info("Status SQL = Actief");
            }
            else
            {
                BtnStatusSqlConnection.Text = "Inactief";
                BtnStatusSqlConnection.BackColor = Color.Red;
                //log.Error("Status SQL = Inactief!!!");
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
                log.Error("Error timer1");
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
            // laadt grafiek met opstarten
        private void Timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            BtnOpvragenVanTot_Click(sender, e);
            log.Info("Load graph when application start");
        }
        private void TimerStatusSensor_Tick(object sender, EventArgs e)
        {
            // interval staat bij shown
            TimerStatusSQL.Enabled = false;
            if (timeLeft == 0)
            {
                TimerStatusSQL.Enabled = !TimerStatusSQL.Enabled;
                label1.Text = "Time's out.";
                log.Error("TimerStatusSql problems");
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
            log.Info("ToolstripMenuItem: 15 Minuten performclick");
        }
        private void Timer30ServiceAlert_Tick(object sender, EventArgs e)
        {
            minutenToolStripMenuItem1.PerformClick();
            log.Info("ToolstripMenuItem: 30 Minuten performclick");
        }
        private void Timer60ServiceAlert_Tick(object sender, EventArgs e)
        {
            minutenToolStripMenuItem2.PerformClick();
            log.Info("ToolstripMenuItem: 60+ Minuten performclick");
        }
        private void TimerDigiClock_Tick(object sender, EventArgs e)
        {
            TbDigiClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        // einde timers
        // sensor 1 begin
        private void RdbFarhenheid1_Click(object sender, EventArgs e)
        {
            log.Info("RadioButtonFarhenheidSensor1 START");
            PnlS12Top.Visible = false;
            log.Info("PnlS12Top (Black header) hidden");
            PnlSensor1.Invalidate();
            log.Info("PnlSensor1 invalidate");
            PnlS1Top.Visible = true;
            log.Info("PnlS1Top (Black header) visible");
            PnlS2Top.Visible = false;
            log.Info("PnlS2Top (Black header) hidden");
            Btns1.Visible = true;
            log.Info("BtnS1 visible");
            BtnS2.Visible = false;
            log.Info("BtnS2 hidden");
            BtnS12.Visible = false;
            log.Info("BtnS12 hidden");
            PnlActiveS2.BackColor = Color.Transparent;
            log.Info("PnlActiveS2 backcolor = transperant");
            PnlActiveS1.BackColor = Color.Black;
            log.Info("PnlActiveS1 backcolor = black");
            if (RdbFarhenheid1.Checked == true)
            {
                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = true;
                log.Info("GrafiekFarhenheid1 is visible");
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekFarhenheidAll.Visible = false;
                GrafiekKelvinAll.Visible = false;
                

                RdbCelsius1.Checked = false;
                RdbKelvin1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbKelvin2.Checked = false;
                RdbFarhenheid1.Checked = true;
                log.Info("RdbFarhenheid1 is checked");
                RdbFarhenheid2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;
            }
            SelectLocMinMaxFarhenheid1();
            log.Info("SelectLocMinMaxFarhenheid1 (F)");
            SelectLocMinMaxFarhenheid2();
            log.Info("SelectLocMinMaxFarhenheid2 (F)");
            grafiekFarhenheid1.ResetAutoValues();
            log.Info("Reset values grafiekFarhenheid1");
            DateTime AToF1 = DtpTot.Value;
            DateTime AFromF1 = DtpVan.Value;
            grafiekFarhenheid1.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempF1(AFromF1, AToF1);
            log.Info("Set Y axis min temp Farhenheid Sensor 1");
            grafiekFarhenheid1.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempF1(AFromF1, AToF1);
            log.Info("Set Y axis max temp Farhenheid Sensor 1");
            if (grafiekFarhenheid1.ChartAreas[0].AxisY.Maximum == grafiekFarhenheid1.ChartAreas[0].AxisY.Minimum)
            {
                grafiekFarhenheid1.ChartAreas[0].AxisY.Maximum = grafiekFarhenheid1.ChartAreas[0].AxisY.Minimum + 5;
                log.Error("Y axis min,max Farhenheid sensor 1");
            }
            grafiekFarhenheid1.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            log.Info("Make whole numbers from Y axis value Farhenheid sensor 1");
            grafiekFarhenheid1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            log.Info("Set X axis style Farhenheid sensor 1");
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            log.Info("BtnOpvragenVanTot performclick");
            log.Info("RadioButtonFarhenheidSensor1 STOP");
        }
        private void RdbKelvin1_Click(object sender, EventArgs e)
        {
            log.Info("RadioButtonKelvinSensor1 START");
            PnlS12Top.Visible = false;
            log.Info("PnlS12Top (Black header) hidden");
            PnlS1Top.Visible = true;
            log.Info("PnlS1Top (Black header) visible");
            PnlS2Top.Visible = false;
            log.Info("PnlS2Top (Black header hidden)");
            PnlActiveS2.BackColor = Color.Transparent;
            log.Info("PnlActiveS2 backcolor = transperant");
            PnlActiveS1.BackColor = Color.Black;
            log.Info("PnlActiceS1 backcolor = black");
            Btns1.Visible = true;
            log.Info("BtnS1 visible");
            BtnS2.Visible = false;
            log.Info("BtnS2 hidden");
            BtnS12.Visible = false;
            log.Info("btnS1+2 hidden");
            if (RdbKelvin1.Checked == true)
            {
                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = true;
                log.Info("Graph Kelvin sensor 1 visible");
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;

                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbKelvin1.Checked = true;
                log.Info("rdbKelvin sensor 1 visible");
                RdbKelvin2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;
            }
            SelectLocMinMaxKelvin1();
            log.Info("SelectLocMinMaxKelvin1 (F)");
            SelectLocMinMaxKelvin2();
            log.Info("SelectLocMinMaxKelvin2 (F)");
            GrafiekKelvin1.ResetAutoValues();
            log.Info("Reset values grafiekFarhenheidKelvin1");
            DateTime AToK1 = DtpTot.Value;
            DateTime AFromK1 = DtpVan.Value;
            GrafiekKelvin1.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempK1(AFromK1, AToK1);
            log.Info("Set Y axis min temp Kelvin Sensor 1");
            GrafiekKelvin1.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempK1(AFromK1, AToK1);
            log.Info("Set Y axis max temp Kelvin Sensor 1");
            if (GrafiekKelvin1.ChartAreas[0].AxisY.Maximum == GrafiekKelvin1.ChartAreas[0].AxisY.Minimum)
            {
                GrafiekKelvin1.ChartAreas[0].AxisY.Maximum = GrafiekKelvin1.ChartAreas[0].AxisY.Minimum + 10;
                log.Error("Y axis min,max Kelvin sensor 1");
            }
            GrafiekKelvin1.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            log.Info("Make whole numbers from Y axis value Kelvin sensor 1");
            GrafiekKelvin1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            log.Info("Set X axis style Kelvin sensor 1");
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            log.Info("BtnOpvragenVanTot performclick");
            log.Info("RadioButtonKelvinSensor1 STOP");
        }
        private void RdbCelsius1_Click(object sender, EventArgs e)
        {
            log.Info("RadioButtonCelsiusSensor1 START");
            PnlS12Top.Visible = false;
            log.Info("PnlS12Top (Black header) hidden");
            PnlS2Top.Visible = false;
            log.Info("PnlS2Top (Black header) hidden");
            PnlS1Top.Visible = true;
            log.Info("PnlS1Top (Black header) visible");
            PnlActiveS2.BackColor = Color.Transparent;
            log.Info("PnlActiveS2 backcolor = transperant");
            PnlActiveS1.BackColor = Color.Black;
            log.Info("PnlActiveS1 backcolor = black");
            Btns1.Visible = true;
            log.Info("BtnS1 visible");
            BtnS2.Visible = false;
            log.Info("BtnS2 hidden");
            BtnS12.Visible = false;
            log.Info("BtnS1+2 hidden");
            if (RdbCelsius1.Checked == true)
            {
                GrafiekTemperatuur.Visible = true;
                log.Info("Graph Celsius sensor 1 visible");
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
                RdbCelsius1.Checked = true;
                log.Info("Graph Celsius sensor 1 checked");
                RdbCelsius2.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;
            }
            SelectLocMinMaxCelsius1();
            log.Info("SelectLocMinMaxCelsius1 (F)");
            SelectLocMinMaxCelsius2();
            log.Info("SelectLocMinMaxCelsius2 (F)");
            GrafiekTemperatuur.ResetAutoValues();
            log.Info("Reset values grafiekCelsius1");
            DateTime AToC1 = DtpTot.Value;
            DateTime AFromC1 = DtpVan.Value;
            GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempC1(AFromC1, AToC1);
            log.Info("Set Y axis max temp Celsius Sensor 1");
            GrafiekTemperatuur.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempC1(AFromC1, AToC1);
            log.Info("Set Y axis min temp Celsius Sensor 1");
            if (GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum == GrafiekTemperatuur.ChartAreas[0].AxisY.Minimum)
            {
                GrafiekTemperatuur.ChartAreas[0].AxisY.Maximum = GrafiekTemperatuur.ChartAreas[0].AxisY.Minimum + 2;
                log.Error("Y axis min,max Celsius sensor 1");
            }
            GrafiekTemperatuur.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            log.Info("Make whole numbers from Y axis value Celsius sensor 1");
            GrafiekTemperatuur.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            log.Info("Set X axis style Celsius sensor 1");
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            log.Info("BtnOpvragenVanTot performclick");
            log.Info("RadioButtonCelsiusSensor1 STOP");
        }
        // sensor 1 einde
        // sensor 2 begin
        private void RdbCelsius2_Click(object sender, EventArgs e)
        {
            log.Info("RadioButtonCelsiusSensor2 START");
            PnlS2Top.Visible = true;
            log.Info("PnlS2Top (Black header) visible");
            PnlS12Top.Visible = false;
            log.Info("PnlS12Top (Black header) hidden");
            PnlS1Top.Visible = false;
            log.Info("PnlS1Top (Black header) hidden");
            PnlActiveS1.BackColor = Color.Transparent;
            log.Info("PnlActiveS1 backcolor = transperant");
            PnlActiveS2.BackColor = Color.Black;
            log.Info("PnlActiveS2 backcolor = black");
            BtnS2.Visible = true;
            log.Info("BtnS2 visible");
            Btns1.Visible = false;
            log.Info("BtnS1 hidden");
            BtnS12.Visible = false;
            log.Info("BtnS12 hidden");
            if (RdbCelsius2.Checked == true)
            {
                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = true;
                log.Info("Graph Celsius sensor 2 checked");
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin1.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvin2.Checked = false;

                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = true;
                log.Info("Graph Celsius sensor 2 visible");
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekKelvinAll.Visible = false;
                GrafiekFarhenheidAll.Visible = false;           
            }
            SelectLocMinMaxCelsius2();
            log.Info("SelectLocMinMaxCelsius2 (F)");
            SelectLocMinMaxCelsius1();
            log.Info("SelectLocMinMaxCelsius1 (F)");
            GrafiekTemperatuur2.ResetAutoValues();
            log.Info("Reset values grafiekCelsius2");
            DateTime AToC2 = DtpTot.Value;
            DateTime AFromC2 = DtpVan.Value;
            GrafiekTemperatuur2.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempC2(AFromC2, AToC2);
            log.Info("Set Y axis max temp Celsius Sensor 2");
            GrafiekTemperatuur2.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempC2(AFromC2, AToC2);
            log.Info("Set Y axis min temp Celsius Sensor 2");
            if (GrafiekTemperatuur2.ChartAreas[0].AxisY.Maximum == GrafiekTemperatuur2.ChartAreas[0].AxisY.Minimum)
            {
                GrafiekTemperatuur2.ChartAreas[0].AxisY.Maximum = GrafiekTemperatuur2.ChartAreas[0].AxisY.Minimum + 10;
                log.Error("Y axis min,max Celsius sensor 2");
            }
            GrafiekTemperatuur2.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            log.Info("Make whole numbers from Y axis value Celsius sensor 2");
            GrafiekTemperatuur2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            log.Info("Set X axis style Celsius sensor 2");
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            log.Info("BtnOpvragenVanTot performclick");
            log.Info("RadioButtonCelsiusSensor2 STOP");
        }
        private void RdbKelvin2_Click(object sender, EventArgs e)
        {
            log.Info("RadioButtonKelvinSensor2 START");
            PnlS12Top.Visible = false;
            log.Info("PnlS12Top (Black header) hidden");
            PnlS2Top.Visible = true;
            log.Info("PnlS2Top (Black header) visible");
            PnlS1Top.Visible = false;
            log.Info("PnlS1Top (Black header) hidden");
            PnlActiveS1.BackColor = Color.Transparent;
            log.Info("PnlActiveS1 backcolor = transperant");
            PnlActiveS2.BackColor = Color.Black;
            log.Info("PnlActiveS2 backcolor = black");
            BtnS2.Visible = true;
            log.Info("BtnS2 visible");
            Btns1.Visible = false;
            log.Info("BtnS1 hidden");
            BtnS12.Visible = false;
            log.Info("BtnS12 hidden");
            if (RdbKelvin2.Checked == true)
            {
                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin1.Checked = false;
                RdbKelvin2.Checked = true;
                log.Info("Graph Kelvin sensor 2 checked");
                RdbCelsiusAll.Checked = false;;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;

                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = true;
                log.Info("Graph Kelvin sensor 2 visible");
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekKelvinAll.Visible = false;
                GrafiekFarhenheidAll.Visible = false;
            }
            SelectLocMinMaxKelvin2();
            log.Info("SelectLocMinMaxKelvin2 (F)");
            SelectLocMinMaxKelvin1();
            log.Info("SelectLocMinMaxKelvin1 (F)");
            GrafiekKelvin2.ResetAutoValues();
            log.Info("Reset values grafiekKelvin2");
            DateTime AToK2 = DtpTot.Value;
            DateTime AFromK2 = DtpVan.Value;
            GrafiekKelvin2.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempK2(AFromK2, AToK2);
            log.Info("Set Y axis max temp Kelvin Sensor 2");
            GrafiekKelvin2.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempK2(AFromK2, AToK2);
            log.Info("Set Y axis min temp Kelvin Sensor 2");
            if (GrafiekKelvin2.ChartAreas[0].AxisY.Maximum == GrafiekKelvin2.ChartAreas[0].AxisY.Minimum)
            {
                GrafiekKelvin2.ChartAreas[0].AxisY.Maximum = GrafiekKelvin2.ChartAreas[0].AxisY.Minimum + 10;
                log.Error("Y axis min,max Kelvin sensor 2");
            }
            GrafiekKelvin2.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            log.Info("Make whole numbers from Y axis value Kelvin sensor 2");
            GrafiekKelvin2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            log.Info("Set X axis style Kelvin sensor 2");
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            log.Info("BtnOpvragenVanTot performclick");
            log.Info("RadioButtonKelvinSensor2 STOP");
        }
        private void RdbFarhenheid2_Click(object sender, EventArgs e)
        {
            log.Info("RadioButtonFarhenheidSensor2 START");
            PnlS12Top.Visible = false;
            log.Info("PnlS12Top (Black header) hidden");
            PnlS2Top.Visible = true;
            log.Info("PnlS2Top (Black header) visible");
            PnlS1Top.Visible = false;
            log.Info("PnlS1Top (Black header) hidden");
            PnlActiveS1.BackColor = Color.Transparent;
            log.Info("PnlActiveS1 backcolor = transperant");
            PnlActiveS2.BackColor = Color.Black;
            log.Info("PnlActiveS2 backcolor = black");
            BtnS2.Visible = true;
            log.Info("BtnS2 visible");
            Btns1.Visible = false;
            log.Info("BtnS1 hidden");
            BtnS12.Visible = false;
            log.Info("BtnS12 hidden");
            if (RdbFarhenheid2.Checked == true)
            {
                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = true;
                log.Info("Graph farhenheid sensor 2 visible");
                GrafiekFarhenheidAll.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekKelvinAll.Visible = false;

                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = true;
                log.Info("Graph Farhenheid sensor 2 checked");
                RdbKelvin1.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = false;
                RdbFarhenheidAll.Checked = false;
                RdbKelvinAll.Checked = false;
            }
            SelectLocMinMaxFarhenheid2();
            log.Info("SelectLocMinMaxFarhenheid2 (F)");
            SelectLocMinMaxFarhenheid1();
            log.Info("SelectLocMinMaxFarhenheid1 (F)");
            grafiekFarhenheid2.ResetAutoValues();
            log.Info("Reset values grafiekFarhenheid2");
            DateTime AToF2 = DtpTot.Value;
            DateTime AFromF2 = DtpVan.Value;
            grafiekFarhenheid2.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempF2(AFromF2, AToF2);
            log.Info("Set Y axis max temp Farhenheid Sensor 2");
            grafiekFarhenheid2.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempF2(AFromF2, AToF2);
            log.Info("Set Y axis min temp Farhenheid Sensor 2");
            if (grafiekFarhenheid2.ChartAreas[0].AxisY.Maximum == grafiekFarhenheid2.ChartAreas[0].AxisY.Minimum)
            {
                grafiekFarhenheid2.ChartAreas[0].AxisY.Maximum = grafiekFarhenheid2.ChartAreas[0].AxisY.Minimum + 5;
                log.Error("Y axis min,max Farhenheid sensor 2");
            }
            grafiekFarhenheid2.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            log.Info("Make whole numbers from Y axis value Farhenheid sensor 2");
            grafiekFarhenheid2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
            log.Info("Set X axis style Farhenheid sensor 2");
            BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
            log.Info("BtnOpvragenVanTot permformclick");
            log.Info("RadioButtonFarhenheidSensor2 STOP");
        }
        // sensor 2 einde
        // sensor All begin
        private void RdbCelsiusAll_Click(object sender, EventArgs e)
        {
            log.Info("RadioButtonCelsiusSensor1+2 START");
            PnlS12Top.Visible = true;
            log.Info("PnlS12Top (Black header) visible");
            PnlS2Top.Visible = false;
            log.Info("PnlS12Top (Black header) hidden");
            PnlS1Top.Visible = false;
            log.Info("PnlS12Top (Black header) hidden");
            BtnS2.Visible = false;
            log.Info("BtnS2 hidden");
            Btns1.Visible = false;
            log.Info("BtnS1 hidden");
            BtnS12.Visible = true;
            log.Info("BtnS12 visible");
            PnlActiveS1.BackColor = Color.Transparent;
            log.Info("PnlActiveS1 backcolor = transperant");
            PnlActiveS2.BackColor = Color.Transparent;
            log.Info("PnlActiveS2 backcolor = transperant");
            if (RdbCelsiusAll.Checked == true)
            {
                RdbCelsius1.Checked = false;
                RdbCelsius2.Checked = false;
                RdbFarhenheid1.Checked = false;
                RdbFarhenheid2.Checked = false;
                RdbKelvin1.Checked = false;
                RdbKelvin2.Checked = false;
                RdbCelsiusAll.Checked = true;
                log.Info("Graph Celsius sensor 1+2 checked");

                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = true;
                log.Info("Graph Celsius sensor 1+2 visible");
                GrafiekFarhenheidAll.Visible = false;
                GrafiekKelvinAll.Visible = false;

                GrafiekCelsiusAll.ResetAutoValues();
                log.Info("Reset values grafiekCelsius1+2");
                DateTime AToCAll = DtpTot.Value;
                DateTime AFromCAll = DtpVan.Value;
                GrafiekCelsiusAll.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempCAll(AFromCAll, AToCAll);
                log.Info("Set Y axis max temp Celsius Sensor 1+2");
                GrafiekCelsiusAll.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempCAll(AFromCAll, AToCAll);
                log.Info("Set Y axis min temp Celsius Sensor 1+2");
                if (GrafiekCelsiusAll.ChartAreas[0].AxisY.Maximum == GrafiekCelsiusAll.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekCelsiusAll.ChartAreas[0].AxisY.Maximum = GrafiekCelsiusAll.ChartAreas[0].AxisY.Minimum + 5;
                    log.Error("Y axis min, max Celsius sensor 1+2");
                }
                GrafiekCelsiusAll.ChartAreas[0].AxisY.LabelStyle.Format = "0";
                log.Info("Make whole numbers from Y axis value Celsius sensor 1+2");
                GrafiekCelsiusAll.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
                log.Info("Set X axis style Celsius sensor 1+2");
                BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
                log.Info("BtnOpvragenVanTot performclick");               
            }
            log.Info("RadioButtonCelsiusSensor1+2 STOP");
        }
        private void RdbKelvinAll_Click(object sender, EventArgs e)
        {
            log.Info("RadioButtonKelvinSensor1+2 START");
            PnlS12Top.Visible = true;
            log.Info("PnlS12Top (Black header) visible");
            PnlS2Top.Visible = false;
            log.Info("PnlS2Top (Black header) hidden");
            PnlS1Top.Visible = false;
            log.Info("PnlS1Top (Black header) hidden");
            BtnS2.Visible = false;
            log.Info("BtnS2 hidden");
            Btns1.Visible = false;
            log.Info("BtnS1 hidden");
            BtnS12.Visible = true;
            log.Info("BtnS12 visible");
            PnlActiveS1.BackColor = Color.Transparent;
            log.Info("PnlActiveS1 backcolor = transperant");
            PnlActiveS2.BackColor = Color.Transparent;
            log.Info("PnlActiveS2 backcolor = transperant");
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
                log.Info("Graph Kelvin sensor 1+2 checked");
                RdbFarhenheidAll.Checked = false;

                GrafiekTemperatuur.Visible = false;
                GrafiekTemperatuur2.Visible = false;
                GrafiekKelvin1.Visible = false;
                GrafiekKelvin2.Visible = false;
                grafiekFarhenheid1.Visible = false;
                grafiekFarhenheid2.Visible = false;
                GrafiekCelsiusAll.Visible = false;
                GrafiekKelvinAll.Visible = true;
                log.Info("Graph Kelvin sensor 1+2 visible");
                GrafiekFarhenheidAll.Visible = false;
                GrafiekKelvinAll.ResetAutoValues();
                log.Info("Reset values grafiekCelsius1+2");
                DateTime AToKAll = DtpTot.Value;
                DateTime AFromKAll = DtpVan.Value;
                GrafiekKelvinAll.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempKAll(AFromKAll, AToKAll);
                log.Info("Set Y axis max temp Celsius Sensor 1+2");
                GrafiekKelvinAll.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempKAll(AFromKAll, AToKAll);
                log.Info("Set Y axis min temp Celsius Sensor 1+2");
                if (GrafiekKelvinAll.ChartAreas[0].AxisY.Maximum == GrafiekKelvinAll.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekKelvinAll.ChartAreas[0].AxisY.Maximum = GrafiekKelvinAll.ChartAreas[0].AxisY.Minimum + 5;
                    log.Info("Y axis min, max Kelvin sensor 1+2");
                }
                GrafiekKelvinAll.ChartAreas[0].AxisY.LabelStyle.Format = "0";
                log.Info("Make whole numbers from Y axis value Kelvin sensor 1+2");
                GrafiekKelvinAll.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
                log.Info("Set X axis style Kelvin sensor 1+2");
                BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
                log.Info("BtnOpvragenVanTot performclick");            
            }
            log.Info("RadioButtonKelvinSensor1+2 STOP");
        }
        private void RdbFarhenheidAll_Click(object sender, EventArgs e)
        {
            log.Info("RadioButtonFarhenheidSensor1+2 START ");
            PnlS12Top.Visible = true;
            log.Info("PnlS12Top (Black header) visible");
            PnlS2Top.Visible = false;
            log.Info("PnlS2Top (Black header) visible");
            PnlS1Top.Visible = false;
            log.Info("PnlS1Top (Black header) visible");
            BtnS2.Visible = false;
            log.Info("BtnS2 hidden");
            Btns1.Visible = false;
            log.Info("BtnS1 hidden");
            BtnS12.Visible = true;
            log.Info("BtnS12 visible");
            PnlActiveS1.BackColor = Color.Transparent;
            log.Info("PnlActiveS1 backcolor = transperant");
            PnlActiveS2.BackColor = Color.Transparent;
            log.Info("PnlActiveS2 backcolor = transperant");
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
                log.Info("Graph Farhenheid sensor 1+2 checked");
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
                log.Info("Graph Farhenheid sensor 1+2 visible");
                GrafiekFarhenheidAll.ResetAutoValues();
                log.Info("Reset values grafiekFarhenheid1+2");
                DateTime AToFAll = DtpTot.Value;
                DateTime AFromFAll = DtpVan.Value;
                GrafiekFarhenheidAll.ChartAreas[0].AxisY.Maximum = TempMgrAxis.YMaxTempFAll(AFromFAll, AToFAll);
                log.Info("Set Y axis max temp Farhenheid Sensor 1+2");
                GrafiekFarhenheidAll.ChartAreas[0].AxisY.Minimum = TempMgrAxis.YMinTempFAll(AFromFAll, AToFAll);
                log.Info("Set Y axis min temp Farhenheid Sensor 1+2");
                if (GrafiekFarhenheidAll.ChartAreas[0].AxisY.Maximum == GrafiekFarhenheidAll.ChartAreas[0].AxisY.Minimum)
                {
                    GrafiekFarhenheidAll.ChartAreas[0].AxisY.Maximum = GrafiekFarhenheidAll.ChartAreas[0].AxisY.Minimum + 5;
                    log.Error("Y axis min, max Farhenheid sensor 1+2");
                }
                GrafiekFarhenheidAll.ChartAreas[0].AxisY.LabelStyle.Format = "0";
                log.Info("Make whole numbers from Y axis value Farhenheid sensor 1+2");
                GrafiekFarhenheidAll.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyy \n HH:mm";
                log.Info("Set X axis style Farhenheid sensor 1+2");
                BtnOpvragenVanTot_Click(BtnOpvragenVanTot, null);
                log.Info("BtnOpvragenVanTot performclick");
            }
            log.Info("RadioButtonFarhenheidSensor1+2 STOP");
        }
        // sensor All einde
        // functies
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
                    log.Error("CheckLastDataDB", E);
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
                    DateTime Dt1 = DtpTot.Value;
                    DtpVan.MaxDate = Dt1;
                    DtpVan.MinDate = dt;
                    connection.Close();
                }
                catch (Exception E)
                {
                    log.Error("SettingsDtpVan", E);
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
                    connection.Close();
                }
                catch (Exception E)
                {
                    log.Error("SelectLocMinMaxCelsius1", E);
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
                    connection.Close();
                }
                catch (Exception E)
                {
                    log.Error("SelectLocMinMaxKelvin1", E);
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
                    connection.Close();
                }
                catch (Exception E)
                {
                    log.Error("SelectLocMinMaxFarhenheid", E);
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
                    connection.Close();
                }
                catch (Exception E)
                {
                    log.Error("SelectLocMinMaxCelsius2", E);
                    MessageBox.Show(E.Message);
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
                    connection.Close();
                }
                catch (Exception E)
                {
                    log.Error("SelectLocMinMaxKelvin2", E);
                    MessageBox.Show(E.Message);
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
                    connection.Close();
                }
                catch (Exception E)
                {
                    log.Error("SelectLocMinMaxFarhenheid2", E);
                    MessageBox.Show(E.Message);
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
            log.Info("Set titles for all sensors");
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
                    log.Info("15 minites toolstrip checked");
                    minutenToolStripMenuItem.PerformClick();
                    log.Info("ToolstripItem 15 minites warning performclick");
                }
                if (minutenToolStripMenuItem1.Checked == true)
                {
                    minutenToolStripMenuItem.Checked = false;
                    minutenToolStripMenuItem1.Checked = true;
                    minutenToolStripMenuItem2.Checked = false;
                    log.Info("30 minites toolstrip checked");
                    minutenToolStripMenuItem1.PerformClick();
                    log.Info("ToolstripItem 30 minites warning performclick");
                }
                if (minutenToolStripMenuItem2.Checked == true)
                {
                    minutenToolStripMenuItem.Checked = false;
                    minutenToolStripMenuItem1.Checked = false;
                    minutenToolStripMenuItem2.Checked = true;
                    log.Info("60+ minites toolstrip checked");
                    minutenToolStripMenuItem2.PerformClick();
                    log.Info("ToolstripItem 60+ minites warning performclick");
                }
            }
            catch (Exception E)
            {
                log.Error("MinutenToolStrip Error", E);
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
                    log.Info("Select email list with status enabled");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string LVal = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                        LResult.Add(LVal);
                    }
                    return LResult;
                }
                catch (Exception E)
                {
                    log.Error("SelectSendEmail", E);
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
                NextMailAllowed = DateTime.Now.AddMinutes(5);
                log.Info("Next mail allowed 5 minutes");
            }
            else
            {
                return "";
            }
            BasePath = ConfigurationManager.AppSettings["APIBasePath"];
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
                    return "Error check log";
                }
            }
            catch (Exception E)
            {
                log.Error("HTTP Post", E);
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
            log.Info("Path mail API loaded");
            List<string> FromAddrs = new List<string>();
            string FromAddr = "1028727@idcollege.nl";
            FromAddrs.Add(FromAddr);
            log.Info("Mail: FromAddrs selected");
            List<string> ToAddrs = SelectSendEmail();
            log.Info("Mail: ToAddres selected");
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
            log.Info("Mail created");
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
        public bool IsServerConnected()
        {
            using (var l_oConnection = new SqlConnection(MyConnectionString2))
            {
                try
                {
                    l_oConnection.Open();
                    l_oConnection.Close();
                    return true;
                }
                catch (SqlException)
                {
                    MessageBox.Show("De database is niet bereikbaar");
                    return false;
                }
            }
        }
        public void NewUpdate()
        {
            MessageBox.Show("Er is een update beschikbaar.");
            log.Info("Update available");
        }
        public bool Engels
        {
            get { return engelsToolStripMenuItem.Checked; }
            set { engelsToolStripMenuItem.Checked = value; }
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
