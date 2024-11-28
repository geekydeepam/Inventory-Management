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
    public partial class ProductModule : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=IMDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader ;
        public ProductModule()
        {
            InitializeComponent();
            LoadCategory();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void LoadCategory()
        {
            CmbCat.Items.Clear();
            cmd=new SqlCommand("Select CatName from tblCategory",con);
            con.Open();
           reader=cmd.ExecuteReader();

            while (reader.Read()) {

                CmbCat.Items.Add(reader[0].ToString()); 
            
            }
            reader.Close();
            con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (MessageBox.Show("Are You Sure You want to save this Product", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Insert into tblProduct(Pname,Pqty,Pprice,Pdescription,PCat) Values (@pname,@qty,@price,@desc,@cat)", con);
                    cmd.Parameters.AddWithValue("@pname", txtProname.Text);
                    cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@qty",Convert.ToInt32(txtQty.Text));
                    cmd.Parameters.AddWithValue("@cat", CmbCat.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Saved SuccessFully");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (MessageBox.Show("Are You Sure You want to Update this Product", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Update tblProduct set Pname=@pname,Pqty=@qty,Pprice=@price,Pdescription=@desc,PCat=@cat where PId=@Id", con);
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(txtQty.Text));
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@pname", txtProname.Text);
                    cmd.Parameters.AddWithValue("@cat", CmbCat.Text);

                    cmd.Parameters.AddWithValue("@Id", lblProduct.Text.Split(':')[1]);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("User Saved SuccessFully");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void Clear()
        {
            txtProname.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtQty.Clear();
        }
    }
}
