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
    public partial class Sellers : Form
    {
        public Sellers()
        {
            InitializeComponent();
            ShowSeller();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PharmacyManagementDb;Integrated Security=True");
        private void ShowSeller()
        {
            con.Open();
            string Query = "Select * from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void Reset()
        {
            SName.Text = "";
            SAdd.Text = "";
            SPhone.Text = "";
            SPass.Text = "";
            SGender.SelectedIndex = 0;
            key = 0;
        }
        private void SSave_Click(object sender, EventArgs e)
        {
            if (SAdd.Text == "" || SName.Text == "" || SPhone.Text == "" || SGender.SelectedIndex == -1 || SPass.Text == "")
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into SellerTbl(SName,SAdd,SPhone,SGen,SDOB,SPass)values(@SN,@SA,@SP,@SG,@SDOB,@SPA)", con);
                    cmd.Parameters.AddWithValue("@SN", SName.Text);
                    cmd.Parameters.AddWithValue("@SA", SAdd.Text);
                    cmd.Parameters.AddWithValue("@SP", SPhone.Text);
                    cmd.Parameters.AddWithValue("@SG", SGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SDOB", SDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SPA", SPass.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Added");
                    con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int key = 0;
       

        private void SDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SName.Text = SDGV.SelectedRows[0].Cells[1].Value.ToString();
            SDOB.Text = SDGV.SelectedRows[0].Cells[5].Value.ToString();
            SPhone.Text = SDGV.SelectedRows[0].Cells[4].Value.ToString();
            SAdd.Text = SDGV.SelectedRows[0].Cells[3].Value.ToString();
            SGender.SelectedItem = SDGV.SelectedRows[0].Cells[2].Value.ToString();
            SPass.Text = SDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (SName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(SDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void SDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Seller");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from SellerTbl where SNum=@SKey", con);
                    cmd.Parameters.AddWithValue("@SKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted");
                    con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void SEdit_Click(object sender, EventArgs e)
        {
            if (SAdd.Text == "" || SName.Text == "" || SPhone.Text == "" || SGender.SelectedIndex == -1 || SPass.Text == "")
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update SellerTbl set SName=@SN,SAdd=@SA,SPhone=@SP,SGen=@SG,SDOB=@SDOB,SPass=@SPA where SNum=@SKey", con);
                    cmd.Parameters.AddWithValue("@SN", SName.Text);
                    cmd.Parameters.AddWithValue("@SA", SAdd.Text);
                    cmd.Parameters.AddWithValue("@SP", SPhone.Text);
                    cmd.Parameters.AddWithValue("@SG", SGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SDOB", SDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SPA", SPass.Text);
                    cmd.Parameters.AddWithValue("@SKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Updated");
                    con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
