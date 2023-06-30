using QuanLyFastFood.DAO;
using QuanLyFastFood.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyFastFood
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            string passWord = txbPassWord.Text;
            if(Login(userName ,passWord))
            {
                Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
                fTableManager f = new fTableManager(loginAccount);

                this.Hide();

                f.ShowDialog();

                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
            }
        }
        bool Login(string userName,string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?","Thông Báo",MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;

            }

           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowPass.Checked)
            {
                txbPassWord.UseSystemPasswordChar = false;
            }
            else
            {
                txbPassWord.UseSystemPasswordChar = true;
            }
        }
    }
}
