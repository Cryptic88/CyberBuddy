using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE_CyberBuddy
{
    public partial class AddTaskForm : Form
    {

        public AddTaskForm()
        {
            InitializeComponent();
        }
        public string TaskTitle => txtTitle.Text.Trim();
        public string TaskDescription => txtDescription.Text.Trim();

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a task title.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;  // prevent closing
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a task description.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;  // prevent closing
                return;
            }
        }
    }
}
