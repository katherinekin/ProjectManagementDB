using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataLibrary.Models;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        // Grabs the connection string needed to connect to the database, default name COSC3380_Group6_Project
        public static string GetConnectionString(string connectionName = "DefaultConnection")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
        // Executes the sql query string sql, loads it into model type T (such as EmployeeModel)
        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }
        public static List<T> LoadData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, data).ToList();
            }
        }
        public static List<T> LoadData<T>(string sql, EmployeeModel data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, data).ToList();
            }
            throw new NotImplementedException();
        }
        public static List<T> LoadData<T>(string sql, ProjectModel data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, data).ToList();
            }
            throw new NotImplementedException();
        }
        public static List<T> LoadData<T>(string sql, BudgetModel data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, data).ToList();
            }
            throw new NotImplementedException();
        }
        // Executes sql query with given parameters
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    return cnn.Execute(sql, data);
                }
                catch
                {
                    return 0;
                }
            }
        }        
    }
}
