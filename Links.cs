using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Links : Form
    {
        public Links()
        {
            InitializeComponent();
        }

        private void btnWebsiteLink_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.google.com/");
        }

        private void btnMobieleApplicatieLink_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.google.com/");
        }
    }
}
