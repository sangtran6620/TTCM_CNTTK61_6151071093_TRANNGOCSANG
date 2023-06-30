using QuanLyFastFood.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyFastFood.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public  static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }

        public bool Login(string userName, string passWord)
        {
            
            string query = "USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExcuteQuery(query, new object[] {userName, passWord });
            return result.Rows.Count>0;
        }
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName  , @password  , @newPassword", new object[]{ userName, displayName, pass, newPass});

            return result > 0;
        }
        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExcuteQuery("SELECT UserName, DisplayName, Type FROM Account");
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM Account WHERE userName = '" + userName + "'");
            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
            
        }
        public bool InsertAccount(string name, string displayName, int type)
        {
            string query = string.Format("INSERT Account (UserName, DisplayName, Type )VALUES ( N'{0}', N'{1}', {2})", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;

        }

        public bool EditAccount(string name, string displayName, int type)
        {
            string query = string.Format("UPDATE Account SET DisplayName = N'{1}', Type = {2} WHERE userName = N'{0}'", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;

        }
        public bool DeletetAccount(string name)
        {
            
            string query = string.Format("DELETE Account WHERE userName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;

        }
        public bool ResetPassword(string name)
        {
            string query = string.Format("UPDATE Account SET password = N'0' WHERE UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;


        }
    }
}
