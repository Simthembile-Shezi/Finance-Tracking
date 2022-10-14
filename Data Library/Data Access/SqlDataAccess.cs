using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Library.Data_Access
{
    class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "Finance_Tracking Database")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static T SingleData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.QuerySingle<T>(sql);
            }
        }

        public static T Joins<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.QuerySingle<T>(sql);
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static int DeleteData(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql);
            }
        }
    }
}
