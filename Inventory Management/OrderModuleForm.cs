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
    public partial class OrderModuleForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=IMDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();

        SqlDataReader rdr;

        int qty = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCust();
            Loadproduct();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCust()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("Select * from tblCustomer WHERE CONCAT(CId,Cname) LIKE '%" + txtsearchbox.Text + "%'", con);
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, rdr[0].ToString(), rdr[1].ToString());
            }
            rdr.Close();
            con.Close();

        }

        public void Loadproduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("Select * from tblProduct WHERE CONCAT(Pname,Pdescription,PCat) LIKE '%" + txtsearchboxPro.Text + "%'", con);
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

        private void txtsearchbox_TextChanged(object sender, EventArgs e)
        {
            LoadCust();
        }

        private void txtsearchboxPro_TextChanged(object sender, EventArgs e)
        {
            Loadproduct ();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void numQty_ValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(numQty.Value)>qty)
            {
                MessageBox.Show("Instock quantity is not enough !","Warning",MessageBoxButtons.OK, MessageBoxIcon.Error);
                numQty.Value = numQty.Value - 1;
                return;
            }
            int total=Convert.ToInt32(txtPrice.Text)* Convert.ToInt32(numQty.Value);
            txttotal.Text = total.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            
            qty = Convert.ToInt32(dgvProduct.Rows[e.RowIndex].Cells[3].Value);
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCustName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (MessageBox.Show("Are You Sure You want to Add this Order", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Insert into tblOrder(Odate,Cid,Pid,Qty,Price,Totalprice) Values (@odate,@cid,@pid,@qty,@price,@tprice)", con);
                    cmd.Parameters.AddWithValue("@odate", Odtm.Value);
                    cmd.Parameters.AddWithValue("@cid", Convert.ToInt32( txtCustId.Text));
                    cmd.Parameters.AddWithValue("@pid",Convert.ToInt32( txtProId.Text));
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(numQty.Text));
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@tprice", Convert.ToInt32(txttotal.Text));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("User Saved SuccessFully");
                    Clear();
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtProId.Clear();
            txtProName.Clear();
           numQty.Value = 0;
            txtPrice.Clear();
            txttotal.Clear();
        }
    }
}
