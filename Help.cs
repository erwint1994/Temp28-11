using System;
using System.Windows.Forms;
using Vertalen;
using log4net;
namespace WindowsFormsApp1
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Temperatuur Parentform2 = null;
        private void Help_Load(object sender, EventArgs e)
        {
            log.Info("Load Help.cs");
            if (Parentform2.Engels == true)
            {
                Vertaal.DoVertaalForm(this, "EN");
            }
        }
    }
}
