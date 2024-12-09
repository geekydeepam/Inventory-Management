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
            orderModuleForm.btnClear.Enabled = true;
            orderModuleForm.ShowDialog();
            LoadOrder();
        }
        private void LoadOrder()
        {
            int i = 0;
            int total = 0;
            dgvOrders.Rows.Clear();
            cmd = new SqlCommand("Select Oid,Odate,O.Pid,P.Pname,O.Cid,C.Cname,Qty,Price,Totalprice from tblOrder as O Join tblCustomer as C on O.Cid=C.CId Join tblProduct as P on O.Pid=P.PId", con);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                i++;
                dgvOrders.Rows.Add(i, reader[0].ToString(), Convert.ToDateTime(reader[1].ToString()).ToString("dd/MM/yyyy"), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString());
                total += Convert.ToInt32(reader[8].ToString());
            }

            reader.Close();
            con.Close();

            lblTotal.Text=total.ToString();
            lblQty.Text = i.ToString();
        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvOrders.Columns[e.ColumnIndex].Name;

            if (ColName == "Delete")
            {
                if (MessageBox.Show("Are You Sure You want to Delete this Order", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("Delete From tblOrder where Oid  =" + dgvOrders.Rows[e.RowIndex].Cells[1].Value.ToString(), con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Deleted SuccesFully");


                    cmd = new SqlCommand("Update tblProduct set Pqty +=@qty where PId Like '" + dgvOrders.Rows[e.RowIndex].Cells[3].Value.ToString()+"'", con);
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells[7].Value.ToString()));


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

            LoadOrder();
        }
    }
}
