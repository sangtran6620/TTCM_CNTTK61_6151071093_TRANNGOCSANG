using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyFastFood.DAO
{
    public class DataProvider
    {
        private string connectionSTR = @"Data Source = DESKTOP-RJ5ATGR\SQLEXPRESS; Initial Catalog = QuanlyFastFood; Integrated Security = True";

        public DataTable ExcuteQuery(string query, object[] paraseter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (paraseter != null)
                {

                    string[] listPara = query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, paraseter[i]);

                            i++;
                        }
                    }


                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }
            return data;



        }
        public object ExecuteScalar(string query, object[] paraseter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (paraseter != null)
                {

                    string[] listPara = query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, paraseter[i]);

                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

              connection.Close();
            }
            return data;


        }
    }
    }
