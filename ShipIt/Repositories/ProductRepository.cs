using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Npgsql;

namespace ShipIt.Repositories
{
    public interface IProductRepository
    {
        int GetCount();
    }

    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public static IDbConnection CreateSqlConnection()
        {
            return new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyPostgres"].ConnectionString);
        }

        public int GetCount()
        {
            string EmployeeCountSQL = "SELECT COUNT(*) FROM gcp";
            return (int) QueryForLong(EmployeeCountSQL);
        }
    }
}