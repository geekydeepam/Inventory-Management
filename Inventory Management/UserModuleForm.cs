using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory_Management
{
    public partial class UserModuleForm : Form
    {

        SqlConnection con=new SqlConnection(@"Data Source=.;Initial Catalog=IMDb;Integrated Security=True;");
        SqlCommand cmd=new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtpassword.Text!=txtRetypePass.Text)
                {
                    MessageBox.Show("Password did not Match", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if(MessageBox.Show("Are You Sure You want to save this User", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd=new SqlCommand("Insert into tblUsers(username,fullname,password,phone) Values (@username,@fullname,@password,@phone)", con);
                    cmd.Parameters.AddWithValue("@username",txtusername.Text);
                    cmd.Parameters.AddWithValue("@fullname", txtfullname.Text);
                    cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@phone", txtphone.Text);

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void Clear()
        {
            txtfullname.Clear();
            txtpassword.Clear();
            txtRetypePass.Clear();
            txtusername.Clear();
            txtphone.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpassword.Text != txtRetypePass.Text)
                {
                    MessageBox.Show("Password did not Match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are You Sure You want to Update this User", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Update tblUsers set fullname=@fullname,password=@password,phone=@phone where username=@username", con);
                    cmd.Parameters.AddWithValue("@username", txtusername.Text);
                    cmd.Parameters.AddWithValue("@fullname", txtfullname.Text);
                    cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@phone", txtphone.Text);

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
    }
}
