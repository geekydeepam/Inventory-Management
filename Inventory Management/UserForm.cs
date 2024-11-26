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
    public partial class UserForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=IMDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();

        SqlDataReader rdr;
        public UserForm()
        {
            InitializeComponent();
            Loaduser(); 
            
        }

        public void Loaduser()
        {
            int i = 0;
            dgvUsers.Rows.Clear();  
            cmd=new SqlCommand("Select * from tblUsers",con);
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                i++;
                dgvUsers.Rows.Add(i,rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString());
            }
            rdr.Close();
            con.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserModuleForm userModuleForm = new UserModuleForm();
            userModuleForm.btnSave.Enabled = true;
            userModuleForm.btnUpdate.Enabled = false    ;
            userModuleForm.ShowDialog();
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvUsers.Columns[e.ColumnIndex].Name;
            if(ColName == "Edit")
            {
                UserModuleForm userModule=new UserModuleForm();

                userModule.txtusername.Text = dgvUsers.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.txtfullname.Text = dgvUsers.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.txtpassword.Text = dgvUsers.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.txtRetypePass.Text = dgvUsers.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.txtphone.Text = dgvUsers.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.btnSave.Enabled = false;
                userModule.btnUpdate.Enabled = true;
                userModule.txtusername.Enabled = false;
                userModule.ShowDialog();


            }
            else if(ColName=="Delete")
            {
                if(MessageBox.Show("Are You Sure You want to Delete this User","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("Delete From tblUsers where phone LIKE '" + dgvUsers.Rows[e.RowIndex].Cells[4].Value.ToString()+"'",con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Deleted SuccesFully");

                }
            }

            Loaduser();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
