using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management
{
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=IMDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;


        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            OrderModuleForm orderModuleForm = new OrderModuleForm();
            orderModuleForm.btnSave.Enabled = true;
            orderModuleForm.btnUpdate.Enabled = false;
            orderModuleForm.btnClear.Enabled = true;
            orderModuleForm.ShowDialog();
        }
        private void LoadOrder()
        {
            int i = 0;
            dgvOrders.Rows.Clear();
            cmd = new SqlCommand("Select * from tblOrder", con);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                i++;
                dgvOrders.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
            }
            reader.Close();
            con.Close();
        }
    }
}
