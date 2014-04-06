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
    public partial class AddComputerForm : Form
    {
        public AddComputerForm()
        {
            InitializeComponent();
            this.addComputerTextBox.KeyPress += addComputerTextBox_KeyPress;
        }

        void addComputerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Enter) && addComputerTextBox.Text.Length > 0)
                addComputerButton_Click(this, e);
        }

        private void addComputerButton_Click(object sender, EventArgs e)
        {
            char[] illegalChars = { '\\','/',':','*','?','\"','<','>','|',',','~','!','@','#','$','%','^','&','\'','(',')','{','}',' ','_' };
            bool invalidCharFound = false;
            foreach (char c in illegalChars)
            {
                if (addComputerTextBox.Text.Trim().Contains(c))
                    invalidCharFound = true;
            }

            if (invalidCharFound)
            {
                MessageBox.Show("Invalid character found in hostname. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MainForm.computerToAddManually = addComputerTextBox.Text.Trim().ToUpper();
                this.Dispose();
            }            
        }

        private void addComputerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (addComputerTextBox.Text.Trim().Length > 0)
                addComputerButton.Enabled = true;
            else
                addComputerButton.Enabled = false;
        }
    }
}
