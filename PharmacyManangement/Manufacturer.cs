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
    public partial class Manufacturer : Form
    {
        public Manufacturer()
        {
            InitializeComponent();
            ShowMan();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PharmacyManagementDb;Integrated Security=True");
        private void ShowMan()
        {
            con.Open();
            string Query = "Select * from ManufacturerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query,con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MDTGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ManAdd.Text == "" || ManName.Text == "" || ManPhone.Text == "")
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ManufacturerTbl(ManName,ManAdd,ManPhone,ManJDate)values(@MN,@MA,@MP,@MJD)", con);
                    cmd.Parameters.AddWithValue("@MN", ManName.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAdd.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhone.Text);
                    cmd.Parameters.AddWithValue("@MJD", MJDT.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Added");
                    con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;

        private void MDTGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ManName.Text = MDTGV.SelectedRows[0].Cells[1].Value.ToString();
            ManAdd.Text = MDTGV.SelectedRows[0].Cells[2].Value.ToString();
            ManPhone.Text = MDTGV.SelectedRows[0].Cells[3].Value.ToString();
            MJDT.Text = MDTGV.SelectedRows[0].Cells[4].Value.ToString();
            if (ManName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MDTGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }


        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("Select the Manufacturer");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ManufacturerTbl where ManId=@MKey", con);
                    cmd.Parameters.AddWithValue("@MKey", key);
                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Deleted");
                    con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Reset()
        {
            ManName.Text = "";
            ManAdd.Text = "";
            ManPhone.Text = "";
            key = 0;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ManAdd.Text == "" || ManName.Text == "" || ManPhone.Text == "")
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update  ManufacturerTbl set ManName=@MN,ManAdd=@MA,ManPhone=@MP,ManJDate=@MJD where ManId=@MKey", con);
                    cmd.Parameters.AddWithValue("@MN", ManName.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAdd.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhone.Text);
                    cmd.Parameters.AddWithValue("@MJD", MJDT.Value.Date);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Update");
                    con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        
       
    }
    }


