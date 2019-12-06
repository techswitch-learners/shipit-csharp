using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Npgsql;

namespace ShipIt.Repositories
{
    public interface IEmployeeRepository
    {
        int GetCount();
        int GetWarehouseCount();
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        public static IDbConnection CreateSqlConnection()
        {
            return new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyPostgres"].ConnectionString);
        }

        public int GetCount()
        {

            using (IDbConnection connection = CreateSqlConnection())
            {
                var command = connection.CreateCommand();
                string EmployeeCountSQL = "SELECT COUNT(*) FROM em";
                command.CommandText = EmployeeCountSQL;
                connection.Open();
                var reader = command.ExecuteReader();

                try
                {
                    reader.Read();
                    return (int) reader.GetInt64(0);
                }
                finally
                {
                    reader.Close();
                }
            };
        }

        public int GetWarehouseCount()
        {
            using (IDbConnection connection = CreateSqlConnection())
            {
                var command = connection.CreateCommand();
                string EmployeeCountSQL = "SELECT COUNT(DISTINCT w_id) FROM em";
                command.CommandText = EmployeeCountSQL;
                connection.Open();
                var reader = command.ExecuteReader();

                try
                {
                    reader.Read();
                    return (int)reader.GetInt64(0);
                }
                finally
                {
                    reader.Close();
                }
            };
        }
    }
}