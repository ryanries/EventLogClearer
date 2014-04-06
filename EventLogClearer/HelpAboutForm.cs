using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventLogClearer
{
    public partial class HelpAboutForm : Form
    {
        public HelpAboutForm()
        {
            InitializeComponent();
        }

        private void HelpAboutForm_Load(object sender, EventArgs e)
        {
            titleLabel.Text = Application.ProductName + " v" + Application.ProductVersion;
        }
    }
}
