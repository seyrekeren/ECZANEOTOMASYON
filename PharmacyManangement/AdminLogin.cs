using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManangement
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (AdminPass.Text=="")
            {

            }
            else if(AdminPass.Text == "Yönetici")
            {
                Sellers Obj = new Sellers();
                Obj.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Yanlış Yönetici Şifresi");
                AdminPass.Text = "";
            }
            
        }
    }
}
