using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess 
    {
        private readonly IConfiguration _config;

        // will do data access to the db 
        //public static string GetConnectionString(string connName = "CalorieCalculatorDB")
        //{
        //    return ConfigurationManager.ConnectionStrings[connName].ConnectionString; // will get the connection string
        //    // if you don't specify a name, it will take the default connection name to return default connection string
        //} 

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadData<T, U>(string sql, U parameters, string connectionStringName, bool isStoredProcedure = false)
        {
            string connectionString = _config.GetConnectionString(connectionStringName); // get connection string 

            CommandType command = CommandType.Text; 

            if (isStoredProcedure)
            {
                command = CommandType.StoredProcedure;
            }

            using (IDbConnection conn = new SqlConnection(connectionString)) // open a sql connection
            {
                List<T> rows = conn.Query<T>(sql, parameters, commandType: command).ToList(); // returning data 
                return rows;
            }
        }

        public void SaveData<T>(string sql, T parameters, string connectionStringName, bool isStoredProcedure = false)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            CommandType command = CommandType.Text;

            if (isStoredProcedure)
            {
                command = CommandType.StoredProcedure;
            }

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, parameters, commandType: command);
            }
        }
    }
}
