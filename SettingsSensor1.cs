using System;
using WindowsFormsApp1;
using System.Windows.Forms;
using pasTemp;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Vertalen;

namespace WindowsFormsApp1
{
    public partial class SettingsSensor1 : Form
    {
        public Temperatuur Parentform1 = null;
        public Temperatuur Parentform2 = null;
        public Temperatuur Parentform3 = null;
        public Temperatuur Parentform4 = null;
        string MyConnectionString2 = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public SettingsSensor1()
        {
            InitializeComponent();
        }
        private void SettingsSensor1_Shown(object sender, EventArgs e)
        {
            FillSettingsSensor1();
            if (Parentform1.CheckSelectRdbCelsius1 == true)
            {
                TbMinimumTemperatuurCelsius.Enabled = true;
                TbMaximumTemperatuurCelsius.Enabled = true;
                TbMinimumtemperatuurKelvin.Enabled = false;
                TbMaximumtemperatuurKelvin.Enabled = false;
                TbMinimumtemperatuurFarhenheid.Enabled = false;
                TbMaximumtemperatuurFarhenheid.Enabled = false;
            }
            if (Parentform1.CheckSelectRdbKelvin1 == true)
            {
                TbMinimumTemperatuurCelsius.Enabled = false;
                TbMaximumTemperatuurCelsius.Enabled = false;
                TbMinimumtemperatuurKelvin.Enabled = true;
                TbMaximumtemperatuurKelvin.Enabled = true;
                TbMinimumtemperatuurFarhenheid.Enabled = false;
                TbMaximumtemperatuurFarhenheid.Enabled = false;
            }
            if (Parentform1.CheckSelectRdbFarhenheid1 == true)
            {
                TbMinimumTemperatuurCelsius.Enabled = false;
                TbMaximumTemperatuurCelsius.Enabled = false;
                TbMinimumtemperatuurKelvin.Enabled = false;
                TbMaximumtemperatuurKelvin.Enabled = false;
                TbMinimumtemperatuurFarhenheid.Enabled = true;
                TbMaximumtemperatuurFarhenheid.Enabled = true;
            }
            // Set focus to control
            TbLocatie.Focus();
            // Set text-selection to end
            TbLocatie.SelectionStart = TbLocatie.Text.Length == 0 ? 0 : TbLocatie.Text.Length - 1;
            // Set text-selection length (in your case 0 = no blue text)
            TbLocatie.SelectionLength = 0;
        }
        private void BtnLocatieSensorOpslaan_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TbLocatie.Text))
            {
                MessageBox.Show("Locatie is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TbMinimumTemperatuurCelsius.Text))
            {
                MessageBox.Show("Minimum temperatuur Celsius is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TbMaximumTemperatuurCelsius.Text))
            {
                MessageBox.Show("Maximum temperatuur Celsius is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TbMinimumtemperatuurKelvin.Text))
            {
                MessageBox.Show("Minimum temperatuur Kelvin is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TbMaximumtemperatuurKelvin.Text))
            {
                MessageBox.Show("Maximum temperatuur Kelvin is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TbMinimumtemperatuurFarhenheid.Text))
            {
                MessageBox.Show("Minimum temperatuur Farhenheid is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TbMaximumtemperatuurFarhenheid.Text))
            {
                MessageBox.Show("Maximum temperatuur Farhenheid is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (Parentform1.CheckSelectRdbCelsius1 == true)
                {
                    SqlConnection connection = new SqlConnection(MyConnectionString2);
                    connection.Close();
                    SqlCommand cmd;
                    connection.Open();
                    try
                    {
                        cmd = connection.CreateCommand();
                        cmd.CommandText = (" UPDATE tbl_Location SET Location = @Location , MaximumTemperatureCelsius = @MaximumTemperatureCelsius, MinimumTemperatureCelsius = @MinimumTemperatureCelsius  WHERE id = 1");
                        cmd.Parameters.AddWithValue("@MinimumTemperatureCelsius", decimal.Parse(TbMinimumTemperatuurCelsius.Text));
                        cmd.Parameters.AddWithValue("@MaximumTemperatureCelsius", decimal.Parse(TbMaximumTemperatuurCelsius.Text));                        
                        cmd.Parameters.AddWithValue("@Location", TbLocatie.Text);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        try
                        {
                            TempMgrUpdateMinMax.MinTempK();
                            TempMgrUpdateMinMax.MinTempF();
                            TempMgrUpdateMinMax.MaxTempK();
                            TempMgrUpdateMinMax.MaxTempF();
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show(E.Message);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (Parentform1.CheckSelectRdbKelvin1 == true)
                {
                    SqlConnection connection = new SqlConnection(MyConnectionString2);
                    connection.Close();
                    SqlCommand cmd;
                    connection.Open();
                    try
                    {
                        cmd = connection.CreateCommand();
                        cmd.CommandText = (" UPDATE tbl_Location SET Location = @Location , MinimumTemperatureKelvin = @MinimumTemperatureKelvin , MaximumTemperatureKelvin = @MaximumTemperatureKelvin WHERE id = 1");
                        cmd.Parameters.AddWithValue("@MinimumTemperatureKelvin", decimal.Parse(TbMinimumtemperatuurKelvin.Text));
                        cmd.Parameters.AddWithValue("@MaximumTemperatureKelvin", decimal.Parse(TbMaximumtemperatuurKelvin.Text));
                        cmd.Parameters.AddWithValue("@Location", TbLocatie.Text);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        try
                        {
                            TempMgrUpdateMinMax.MinKToC();
                            TempMgrUpdateMinMax.MinKToF();
                            TempMgrUpdateMinMax.MaxKToC();
                            TempMgrUpdateMinMax.MaxKToF();
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show(E.Message);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (Parentform1.CheckSelectRdbFarhenheid1 == true)
                {
                    SqlConnection connection = new SqlConnection(MyConnectionString2);
                    connection.Close();
                    SqlCommand cmd;
                    connection.Open();
                    try
                    {
                        cmd = connection.CreateCommand();
                        cmd.CommandText = (" UPDATE tbl_Location SET Location = @Location , MinimumTemperatureFarhenheid = @MinimumTemperatureFarhenheid , MaximumTemperatureFarhenheid = @MaximumTemperatureFarhenheid WHERE id = 1");
                        cmd.Parameters.AddWithValue("@MinimumTemperatureFarhenheid", decimal.Parse(TbMinimumtemperatuurFarhenheid.Text));
                        cmd.Parameters.AddWithValue("@MaximumTemperatureFarhenheid", decimal.Parse(TbMaximumtemperatuurFarhenheid.Text));
                        cmd.Parameters.AddWithValue("@Location", TbLocatie.Text);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        try
                        {
                            TempMgrUpdateMinMax.MinFToK();
                            TempMgrUpdateMinMax.MinFToC();
                            TempMgrUpdateMinMax.MaxFToC();
                            TempMgrUpdateMinMax.MaxFToK();
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show(E.Message);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }          
            this.Hide();
        }
        public void FillSettingsSensor1()
        {
            SqlConnection connection = new SqlConnection(MyConnectionString2);
            SqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = ("SELECT * FROM tbl_location WHERE id=1");
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                string MaximumTemperatureCelsius = Convert.ToString(ds.Tables[0].Rows[0]["MaximumTemperatureCelsius"]);
                string MinimumTemperatureCelsius = Convert.ToString(ds.Tables[0].Rows[0]["MinimumTemperatureCelsius"]);
                string MinimumTemperatureKelvin = Convert.ToString(ds.Tables[0].Rows[0]["MinimumTemperatureKelvin"]);
                string MaximumTemperatureKelvin = Convert.ToString(ds.Tables[0].Rows[0]["MaximumTemperatureKelvin"]);
                string MinimumTemperatureFarhenheid = Convert.ToString(ds.Tables[0].Rows[0]["MinimumTemperatureFarhenheid"]);
                string MaximumTemperatureFarhenheid = Convert.ToString(ds.Tables[0].Rows[0]["MaximumTemperatureFarhenheid"]);
                string Location = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
                TbMaximumTemperatuurCelsius.Text = MaximumTemperatureCelsius;
                TbMinimumTemperatuurCelsius.Text = MinimumTemperatureCelsius;
                TbMaximumtemperatuurKelvin.Text = MaximumTemperatureKelvin;
                TbMinimumtemperatuurKelvin.Text = MinimumTemperatureKelvin;
                TbMaximumtemperatuurFarhenheid.Text = MaximumTemperatureFarhenheid;
                TbMinimumtemperatuurFarhenheid.Text = MinimumTemperatureFarhenheid;
                TbLocatie.Text = Location;
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SettingsSensor1_Load(object sender, EventArgs e)
        {
            if(Engels == true)
            {
                Vertaal.VertaalControlsEN(this, "EN");
            }

            if(Duits == true)
            {
                Vertaal.VertaalControlsDE(this, "DE");
            }

            if (Nederlands == true)
            {
                Vertaal.VertaalControlsNL(this, "NL");
            }
        }

        public bool Engels => Parentform2.Engels;

        public bool Duits => Parentform3.Duits;

        public bool Nederlands => Parentform3.Nederlands;
    }
}
