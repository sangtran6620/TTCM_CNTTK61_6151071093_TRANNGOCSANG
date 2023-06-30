﻿using QuanLyFastFood.DAO;
using QuanLyFastFood.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = QuanLyFastFood.DTO.Menu;

namespace QuanLyFastFood
{
    public partial class fTableManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public fTableManager(Account acc)
        {
            

            InitializeComponent();
            this.LoginAccount = acc;

            LoadTable();
            LoadCategory();
            LoadComboboxTable(cbSwitchTable);
        }

        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += "(" + LoginAccount.DisplayName + ")";
        }
        #region Method
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instace.GetListCategory();

            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instace.GetFoodByCategoryID(id);

            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";

        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
               
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name+ Environment.NewLine+item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.YellowGreen;
                        break;
                    default:
                        btn.BackColor = Color.BlueViolet;
                        break;
                }
                flpTable.Controls.Add(btn);
                

            }
                }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach(Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;

                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            txbTotalPrice.Text = totalPrice.ToString("c",culture);

           
        }
        #endregion 

        #region Events
        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
        
        private void fTableManager_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
           
            
            
        }
        void f_UpdateAccount(object senders, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản ("+e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = LoginAccount;
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;
            f.ShowDialog();
        }

        void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag !=null)
            ShowBill((lsvBill.Tag as Table).ID);
        }

        void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }
        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)

             return;
            

            Category selected = cb.SelectedItem as Category;

            id = selected.ID;


            LoadFoodListByCategoryID(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if(table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }
            int idBill = BillDAO.Instace.GetUncheckBillIDByTableID(table.ID);
            int foodId = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instace.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instace.GetMaxIDBill(), foodId , count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodId, count);

            }
            ShowBill(table.ID);
            LoadTable();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instace.GetUncheckBillIDByTableID(table.ID);
            int discount = (int)nmDisCount.Value;
            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice/100)*discount;
            if(idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\nTổng tiền - (Tổng tiền / 100) x Giảm giá\n=> {1} - ({1} /100) X {2} = {3}",table.Name,totalPrice,discount,finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instace.CheckOut(idBill,discount,(float)finalTotalPrice);
                    ShowBill(table.ID);
                    LoadTable();
                }
            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).ID;

            int id2 = (cbSwitchTable.SelectedItem as Table).ID;

            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}",(lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {


               TableDAO.Instance.SwitchTable(id1, id2);

                LoadTable();
            }


        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCheckOut_Click(this, new EventArgs());
        }

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }

        private void cbFood_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
