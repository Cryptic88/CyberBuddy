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
    public partial class SetReminderForm : Form
    {
        public int Days => int.TryParse(txtDays.Text.Trim(), out var result) ? result : 0;
        public SetReminderForm()
        {
            InitializeComponent();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDays.Text.Trim(), out int days) || days <= 0)
            {
                MessageBox.Show("Please enter a valid number of days (e.g., 5).",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; // Prevent form from closing
            }
        }
    }
}
