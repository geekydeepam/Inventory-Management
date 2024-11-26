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
    public partial class CustomerForm1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=IMDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();

        SqlDataReader rdr;
        public CustomerForm1()
        {
            InitializeComponent();
            LoadCust();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadCust()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("Select * from tblCustomer", con);
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString());
            }
            rdr.Close();
            con.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
        }

        

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            CustomerModule CustModuleForm = new CustomerModule();
            CustModuleForm.btnSave.Enabled = true;
            CustModuleForm.btnUpdate.Enabled = false;
            CustModuleForm.ShowDialog();
            LoadCust();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (ColName == "Edit")
            {
                CustomerModule CustModuleForm = new CustomerModule();

                CustModuleForm.txtCname.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                CustModuleForm.txtCphone.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();

                CustModuleForm.btnSave.Enabled = false;
                CustModuleForm.btnUpdate.Enabled = true;
                CustModuleForm.ShowDialog();


            }
            else if (ColName == "Delete")
            {
                if (MessageBox.Show("Are You Sure You want to Delete this User", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("Delete From tblCustomer where phone LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString(), con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Deleted SuccesFully");

                }
            }

            LoadCust();
        }
    }
}
