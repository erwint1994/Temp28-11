using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Vertalen;
using log4net;
namespace WindowsFormsApp1
{
    public partial class Contact : Form
    {
        public Contact()
        {
            InitializeComponent();
        }
        public Temperatuur Parentform1 = null;
        // url API
        string BasePath = "http://api.pasys.nl/msgcenter/api/MsgJob/PostMsgJob";
        HttpClient HC = new HttpClient();
        DateTime NextMailAllowed = DateTime.Now;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<string> SendEMail()
        {
            HC = new HttpClient();
            HC.DefaultRequestHeaders.Accept.Clear();
            HC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BasePath = ConfigurationManager.AppSettings["APIBasePath"];
            JavaScriptSerializer JS = new JavaScriptSerializer();

            List<string> FromAddrs = new List<string>();
            string FromAddr = TxbEmail.Text;
            FromAddrs.Add(FromAddr);

            List<string> ToAddrs = new List<string>();
            string ToAddr = ConfigurationManager.AppSettings["Email"];
            ToAddrs.Add(ToAddr);

            List<string> CCAddrs = new List<string>();

            List<string> BCCAddrs = new List<string>();

            string body = TxbBericht.Text;

            RootObject rootObject = new RootObject
            {
                Id = 0,
                Addr_from = FromAddrs,
                Addr_to = ToAddrs,
                Addr_cc =  CCAddrs,
                Addr_bcc = BCCAddrs,
                Subject = "Van een klant",
                Body = body,
                Description = "Van een klant",
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
        public async Task<string> HTTPPost(string ARequest, string AParams)
        {
            if (NextMailAllowed <= DateTime.Now)
            {
                NextMailAllowed = DateTime.Now.AddMinutes(8);
            }
            else
            {
                log.Info("NextMailAllowed limit reached");
                return "";
            }

            string BasePath = "http://api.pasys.nl/msgcenter/api/MsgJob/PostMsgJob";
            HttpClient HC = new HttpClient(); // Centrale plek opslaan
            string LPath = BasePath + ARequest;
            StringContent S = new StringContent(AParams, Encoding.UTF8, "application/json");
            HttpResponseMessage HR = await HC.PostAsync(BasePath, S);
            try
            {
                if (HR.IsSuccessStatusCode)
                {
                    log.Info("Mail send");
                    string HCRes = await HR.Content.ReadAsStringAsync();
                    return HCRes;
                }
                else
                {
                    log.Error("Error send mail");
                    return "FOUT !";
                }
            }
            catch (Exception e)
            {
                log.Error("Error send mai;", e);
                MessageBox.Show(e.Message);
                return "";
            }
        }
        private async void BtnVerzenden_Click_1(object sender, EventArgs e)
        {
            TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string ResponseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            ResponseString = reader.ReadLine();
            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = BytesFromString("" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            dataBuffer = BytesFromString("MAIL FROM:<1028727@idcollege.nl>" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            /* Read Response of the RCPT TO Message to know from google if it exist or not */
            dataBuffer = BytesFromString("RCPT TO:<" + TxbEmail.Text.Trim() + ">" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            if (GetResponseCode(ResponseString) == 550)
            {
                MessageBox.Show("Email bestaat niet!");
            }else if (String.IsNullOrEmpty(txbVoornaam.Text))
            {
                MessageBox.Show("Voornaam is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TxbAchternaam.Text))
            {
                MessageBox.Show("Achternaam is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if (String.IsNullOrEmpty(TxbBedrijf.Text))
            {
                MessageBox.Show("Bedrijf is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TxbTelefoonnummer.Text) || !Regex.IsMatch(TxbTelefoonnummer.Text, @"([0-9\-]+)"))
            {
                MessageBox.Show("Telefoonnummer is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TxbEmail.Text) || !Regex.IsMatch(TxbEmail.Text, @"(@)(.+)$"))
            {
                MessageBox.Show("Email is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(TxbBericht.Text))
            {
                MessageBox.Show("Bericht is niet ingevuld.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                await SendEMail();
                MessageBox.Show("Email is verzonden.");
                log.Info("Mail send");
                this.Close();
                dataBuffer = BytesFromString("QUITE" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                tClient.Close();
            }          
        }
        private void Contact_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txbVoornaam;
            if (Parentform1.Engels == true)
            {
                Vertaal.DoVertaalForm(this, "EN");
            }
        }
        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }
    }
}
