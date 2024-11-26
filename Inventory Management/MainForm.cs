using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Form activeform = null;

        private void openChildForm(Form childform)
        {
            if (activeform != null)
            {
                activeform.Close();
            }
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(childform);
            panelMain.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerForm1());
        }
    }
}
