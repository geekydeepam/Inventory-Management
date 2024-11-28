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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=IMDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();

        SqlDataReader rdr;
        public ProductForm()
        {
            InitializeComponent();
            Loadproduct();
        }

        private void btnAddPro_Click(object sender, EventArgs e)
        {
            ProductModule productModule = new ProductModule();
            productModule.btnSave.Enabled = true;
            productModule.btnUpdate.Enabled = false;
            productModule.ShowDialog();
            Loadproduct();
            
        }
        public void Loadproduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("Select * from tblProduct WHERE CONCAT(Pname,Pdescription,PCat) LIKE '%"+txtSearchBox.Text+"%'", con);
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), rdr[4].ToString(), rdr[5].ToString());
            }
            rdr.Close();
            con.Close();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvProduct.Columns[e.ColumnIndex].Name;

            if (ColName == "Edit")
            {
                ProductModule P = new ProductModule();

                P.txtProname.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                P.txtQty.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                P.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                P.txtDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                P.CmbCat.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                P.lblProduct.Text="Product Id :"+ dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();

                P.btnSave.Enabled = false;
                P.btnUpdate.Enabled = true;
                P.ShowDialog();



            }
            else if (ColName == "Delete")
            {
                if (MessageBox.Show("Are You Sure You want to Delete this Product", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("Delete From tblProduct where PId LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Deleted SuccesFully");

                }
            }

            Loadproduct();
        }

        private void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            Loadproduct();
        }
    }
}
