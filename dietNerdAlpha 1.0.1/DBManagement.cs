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

namespace dietNerdAlpha_1._0._1
{
    class DBManagement
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        public int findMaxID(string sqlConnect, string table, string tableType)
        {
            int dataBaseLength = dBLength(sqlConnect, table);

            int maxID = dbMaxID(dataBaseLength, sqlConnect, table, tableType);

            return maxID;
        }

        private int dBLength(string sqlConnect, string table)
        {
            int dbSize = 0;
            int foodIdMax, previousMax;

            string SIZE_QUERY = "SELECT COUNT(*) FROM " + table;

            DataSet sizeDataSet = new DataSet();

            using (SqlConnection sizeCon = new SqlConnection(sqlConnect))
            {
                using (SqlCommand cmdGetDataCount = new SqlCommand(SIZE_QUERY, sizeCon))
                {
                    sizeCon.Open();
                    dbSize = (int)cmdGetDataCount.ExecuteScalar();
                }
                sizeCon.Close();
            }
            if (dbSize != 0)
            {
                using (SqlConnection sizeCon = new SqlConnection(sqlConnect))
                {
                    using (SqlCommand cmdGetDataCount = new SqlCommand(SIZE_QUERY, sizeCon))
                    {
                        sizeCon.Open();
                        previousMax = (int)cmdGetDataCount.ExecuteScalar();
                    }
                    sizeCon.Close();
                }
                foodIdMax = previousMax + 1;
            }
            else
            {
                foodIdMax = dbSize;
            }
            return foodIdMax;
        }

        private int dbMaxID(int dBLength, string sqlConnect, string table, string tableType)
        {
            cn = new SqlConnection(sqlConnect);
            cmd = new SqlCommand();
            cn.Open();
            cmd.Connection = cn;
            cmd.CommandText = "Select * from " + table;
            dr = cmd.ExecuteReader();

            List<int> IDList = new List<int>();
            if (tableType == "ingredent")
            {
                while (dr.Read())
                {
                    IDList.Add((int)dr["Id"]);
                }
            }
            if (tableType == "recipe")
            {
                while (dr.Read())
                {
                    IDList.Add((int)dr["Id"]);
                }
            }

            dr.Dispose();
            dr.Close();
            int IDMax = 0;
            int listCount = IDList.Count();

            if (listCount != 0)
            {
                IDMax = IDList.Max();
            }

            

            cn.Close();

            return IDMax;
        }
    }
}
