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

namespace PharmacyManangement
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCust();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PharmacyManagementDb;Integrated Security=True");
        private void ShowCust()
        {
            con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CDTGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void Reset()
        {
            CustName.Text = "";
            CustPhone.Text = "";
            CustGenderCbx.SelectedIndex=0;
            CustAdd.Text = "";

        }

        private void CustSaveBtn_Click(object sender, EventArgs e)
        {
            if (CustName.Text == "" || CustPhone.Text == "" || CustPhone.Text == "" || CustGenderCbx.SelectedIndex == -1)
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl(CustName,CustPhone,CustAdd,CustDOB,CustGen)values(@CN,@CP,@CA,@CDB,@CG)", con);
                    cmd.Parameters.AddWithValue("@CN", CustName.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhone.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAdd.Text);
                    cmd.Parameters.AddWithValue("@CDB", CustDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", CustGenderCbx.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added");
                    con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void CDTGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustName.Text = CDTGV.SelectedRows[0].Cells[1].Value.ToString();
            CustPhone.Text = CDTGV.SelectedRows[0].Cells[2].Value.ToString();
            CustAdd.Text = CDTGV.SelectedRows[0].Cells[3].Value.ToString();
            CustDOB.Text = CDTGV.SelectedRows[0].Cells[4].Value.ToString();
            CustGenderCbx.SelectedItem = CDTGV.SelectedRows[0].Cells[5].Value.ToString();
            if (CustName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CDTGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void CustDeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Customer");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CustomerTbl where CustNum=@CKey", con);
                    cmd.Parameters.AddWithValue("@CKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void CustEditBtn_Click(object sender, EventArgs e)
        {
            if (CustName.Text == "" || CustAdd.Text == "" || CustPhone.Text == "" || CustGenderCbx.SelectedIndex == -1)
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTbl set CustName=@CN,CustAdd=@CA,CustPhone=@CP,CustDOB=@CDB,CustGen=@CG where CustNum=@CKey", con);
                    cmd.Parameters.AddWithValue("@CN", CustName.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAdd.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhone.Text);
                    cmd.Parameters.AddWithValue("@CDB", CustDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", CustGenderCbx.SelectedItem.ToString());

                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated");
                    con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
