using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vertalen;

namespace WindowsFormsApp1
{
    public partial class Help : Form
    {
        public Temperatuur Parentform1 = null;
        public Help()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Help_Load(object sender, EventArgs e)
        {
            if (Parentform1.Engels == true)
            {
                foreach (Control control in Controls)
                {
                    Vertaal.VertaalControlsEN(control, "EN");
                    foreach (Control controls in GetAllControls(control))
                    {
                        Vertaal.VertaalControlsEN(controls, "EN");
                    }
                }
            }

            if (Parentform1.Duits == true)
            {
                foreach (Control control in Controls)
                {
                    Vertaal.VertaalControlsDE(control, "DE");
                    foreach (Control controls in GetAllControls(control))
                    {
                        Vertaal.VertaalControlsDE(controls, "DE");
                    }
                }             
            }

            if (Parentform1.Nederlands == true)
            {
                foreach (Control control in Controls)
                {
                    Vertaal.VertaalControlsNL(control, "NL");
                    foreach (Control controls in GetAllControls(control))
                    {
                        Vertaal.VertaalControlsNL(controls, "NL");
                    }
                }
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {

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

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
