using Microsoft.Reporting.WinForms;
using QuanLyFastFood.DAO;
using QuanLyFastFood.DTO;
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

namespace QuanLyFastFood
{
    public partial class fAdmin : Form
    {
        BindingSource categorylist = new BindingSource();
        BindingSource foodList = new BindingSource();

        BindingSource accountList = new BindingSource();

        BindingSource tableList = new BindingSource();

        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instace.SearchFoodByName(name);
            return listFood;
        }

        void Load()
        {
            dtgvCategory.DataSource = categorylist;
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            dtgvTable.DataSource = tableList;

            LoadListByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadDateTimePickerBill();
            LoadListFood();
            LoadAccount();
            LoadCategory();

            LoadCategoryIntoCombobox(cbFoodCategory);
            AddFoodBindding();
            AddAccountBinding();
            AddCategoryBinding();
            AddTableBinding();
            LoadStatusIntoCombobox(cbTableStatus);
            LoadTable();



        }
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void AddTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }
        void LoadStatusIntoCombobox(ComboBox b)
        {
            b.DataSource = TableDAO.Instance.GetTableList();
            b.DisplayMember = "status";
        }

        void AddCategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }
        void LoadCategory()
        {
            categorylist.DataSource = CategoryDAO.Instace.GetListCategory();
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void AddFoodBindding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instace.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadTable()
        {
            tableList.DataSource = TableDAO.Instance.GetTableList();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);

        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instace.GetListFood();
        }

        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");

            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
            LoadAccount();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.EditAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");

            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }
            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Vui lòng đừng xóa chính bạn chứ");
                return;
            }
            if (AccountDAO.Instance.DeletetAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");

            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }
            LoadAccount();

        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show(" Đặt lại mật khẩu thành công");

            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }

        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tpFood_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tpTable_Click(object sender, EventArgs e)
        {

        }

        private void tpAccount_Click(object sender, EventArgs e)
        {

        }
        void LoadListByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instace.GetBillListByDate(checkIn, checkOut);
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListByDate(dtpkFromDate.Value, dtpkToDate.Value);

        }

        private void tcBill_Click(object sender, EventArgs e)
        {

        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;
                    Category category = CategoryDAO.Instace.GetCategoryById(id);
                    cbFoodCategory.SelectedItem = category;
                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;

                        }
                        i++;

                    }
                    cbFoodCategory.SelectedIndex = index;
                }


            }
            catch { }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instace.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());

            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instace.UpdatetFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());

            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa món");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instace.DeletetFood(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());




            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }

        }

        private void fAdmin_Load_1(object sender, EventArgs e)
        {

        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }

        private void fAdmin_Load_2(object sender, EventArgs e)
        {

        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmAccountType.Value;

            AddAccount(userName, displayName, type);

        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmAccountType.Value;

            EditAccount(userName, displayName, type);
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            ResetPass(userName);
        }

        private void btnFirstBillPage_Click(object sender, EventArgs e)
        {
            txbPageBill.Text = "1";
        }

        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instace.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);
            int lastPage = sumRecord / 10;
            if (sumRecord % 10 != 0)
                lastPage++;
            txbPageBill.Text = lastPage.ToString();
        }

        private void txbPageBill_TextChanged(object sender, EventArgs e)
        {
            dtgvBill.DataSource = BillDAO.Instace.GetBillListByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(txbPageBill.Text));
        }

        private void btnPrevioursBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            if (page > 1)
                page--;
            txbPageBill.Text = page.ToString();
        }

        private void btnNextBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            int sumRecord = BillDAO.Instace.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);

            if (page < sumRecord)
                page++;
            txbPageBill.Text = page.ToString();
        }

        private void fAdmin_Load_3(object sender, EventArgs e)
        {

        }

        private void fAdmin_Load_4(object sender, EventArgs e)
        {

        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;


            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm bàn ăn thành công");
                LoadTable();

            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm bàn ăn");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa bàn ăn thành công");
                LoadTable();

            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa bàn ăn");
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;

            int id = Convert.ToInt32(txbTableID.Text);

            if (TableDAO.Instance.UpdateTable(name, id))
            {
                MessageBox.Show("Sửa bàn ăn thành công");
                LoadTable();

            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa bàn ăn");
            }
        }

        private void fAdmin_Load_5(object sender, EventArgs e)
        {

        }

        private void fAdmin_Load_6(object sender, EventArgs e)
        {

        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;


            if (CategoryDAO.Instace.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh muc thành công");
                LoadCategory();

            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAO.Instace.DeleteCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công");
                LoadCategory();

            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục món ăn");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            int id = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAO.Instace.UpdateCategory(name, id))
            {
                MessageBox.Show("Sửa danh mục thành công");
                LoadCategory();

            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa danh mục món ăn");
            }
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void fAdmin_Load_7(object sender, EventArgs e)
        {

        }

        private void fAdmin_Load_8(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanlyFastFoodDataSet.Bill' table. You can move, or remove it, as needed.
            this.BillTableAdapter.Fill(this.QuanlyFastFoodDataSet.Bill);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM Bill");

            ReportDataSource rds = new ReportDataSource("BillReport", data);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            rds.Value = data;
            this.reportViewer1.RefreshReport();
        }

        private void fAdmin_Load_9(object sender, EventArgs e)
        {

        }

        private void fAdmin_Load_10(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fAdmin_Load_11(object sender, EventArgs e)
        {

        }

        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fAdmin_Load_12(object sender, EventArgs e)
        {

        }
    }
}
