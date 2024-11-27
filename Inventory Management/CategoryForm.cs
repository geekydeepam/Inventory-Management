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
    public partial class CategoryForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=IMDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();

        SqlDataReader rdr;
        public CategoryForm()
        {
            InitializeComponent();
            LoadCat();
        }

        public void LoadCat()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cmd = new SqlCommand("Select * from tblCategory", con);
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, rdr[0].ToString(), rdr[1].ToString() );
            }
            rdr.Close();
            con.Close();

        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (ColName == "Edit")
            {
                CategoryModule CustModuleForm = new CategoryModule();

                CustModuleForm.txtCatName.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();
                CustModuleForm.lblCid.Text = "Category ID :" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();

                CustModuleForm.btnSave.Enabled = false;
                CustModuleForm.btnUpdate.Enabled = true;
                CustModuleForm.ShowDialog();

            }
            else if (ColName == "Delete")
            {
                if (MessageBox.Show("Are You Sure You want to Delete this User", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("Delete From tblCategory where CatID LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Deleted SuccesFully");

                }
            }

            LoadCat();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            CategoryModule CatModule = new CategoryModule();
            CatModule.btnSave.Enabled = true;
            CatModule.btnUpdate.Enabled = false;
            CatModule.ShowDialog();
            LoadCat();
        }
    }
}
