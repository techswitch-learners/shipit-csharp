using System.Configuration;
using System.Data;
using Npgsql;

namespace ShipIt.Repositories
{
    public interface ICompanyRepository
    {
        int GetCount();
    }

    public class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        public static IDbConnection CreateSqlConnection()
        {
            return new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyPostgres"].ConnectionString);
        }

        public int GetCount()
        {
            string EmployeeCountSQL = "SELECT COUNT(*) FROM gcp";
            return (int)QueryForLong(EmployeeCountSQL);
        }
    }
}