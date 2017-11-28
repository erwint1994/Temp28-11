using System;
using WindowsFormsApp1;
using System.Windows.Forms;
using pasTemp;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class SettingsSensor2 : Form
    {
        string MyConnectionString2 = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public Temperatuur Parentform2 = null;
        public SettingsSensor2()
        {
            InitializeComponent();
        }
        private void SettingsSensor2_Shown(object sender, EventArgs e)
        {
            FillSettingsSensor2();
            if (Parentform2.CheckSelectRdbCelsius2 == true)
            {
                TbMinimumTemperatuurCelsius.Enabled = true;
                TbMaximumTemperatuurCelsius.Enabled = true;
                TbMinimumtemperatuurKelvin.Enabled = false;
                TbMaximumtemperatuurKelvin.Enabled = false;
                TbMinimumtemperatuurFarhenheid.Enabled = false;
                TbMaximumtemperatuurFarhenheid.Enabled = false;
            }
            if (Parentform2.CheckSelectRdbKelvin2 == true)
            {
                TbMinimumTemperatuurCelsius.Enabled = false;
                TbMaximumTemperatuurCelsius.Enabled = false;
                TbMinimumtemperatuurKelvin.Enabled = true;
                TbMaximumtemperatuurKelvin.Enabled = true;
                TbMinimumtemperatuurFarhenheid.Enabled = false;
                TbMaximumtemperatuurFarhenheid.Enabled = false;
            }
            if (Parentform2.CheckSelectRdbFarhenheid2 == true)
            {
                TbMinimumTemperatuurCelsius.Enabled = false;
                TbMaximumTemperatuurCelsius.Enabled = false;
                TbMinimumtemperatuurKelvin.Enabled = false;
                TbMaximumtemperatuurKelvin.Enabled = false;
                TbMinimumtemperatuurFarhenheid.Enabled = true;
                TbMaximumtemperatuurFarhenheid.Enabled = true;
            }
        }
        private void BtnLocatieSensorOpslaan_Click_1(object sender, EventArgs e)
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
                if (Parentform2.CheckSelectRdbCelsius2 == true)
                {
                    SqlConnection connection = new SqlConnection(MyConnectionString2);
                    connection.Close();
                    SqlCommand cmd;
                    connection.Open();
                    try
                    {
                        cmd = connection.CreateCommand();
                        cmd.CommandText = (" UPDATE tbl_Location SET Location = @Location , MaximumTemperatureCelsius = @MaximumTemperatureCelsius, MinimumTemperatureCelsius = @MinimumTemperatureCelsius  WHERE id = 2");
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
                if (Parentform2.CheckSelectRdbKelvin2 == true)
                {
                    SqlConnection connection = new SqlConnection(MyConnectionString2);
                    connection.Close();
                    SqlCommand cmd;
                    connection.Open();
                    try
                    {
                        cmd = connection.CreateCommand();
                        cmd.CommandText = (" UPDATE tbl_Location SET Location = @Location , MinimumTemperatureKelvin = @MinimumTemperatureKelvin , MaximumTemperatureKelvin = @MaximumTemperatureKelvin WHERE id = 2");
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
                if (Parentform2.CheckSelectRdbFarhenheid2 == true)
                {
                    SqlConnection connection = new SqlConnection(MyConnectionString2);
                    connection.Close();
                    SqlCommand cmd;
                    connection.Open();
                    try
                    {
                        cmd = connection.CreateCommand();
                        cmd.CommandText = (" UPDATE tbl_Location SET Location = @Location , MinimumTemperatureFarhenheid = @MinimumTemperatureFarhenheid , MaximumTemperatureFarhenheid = @MaximumTemperatureFarhenheid WHERE id = 2");
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
        public void FillSettingsSensor2()
        {
            SqlConnection connection = new SqlConnection(MyConnectionString2);
            SqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = ("SELECT * FROM tbl_location WHERE id=2");
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
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
