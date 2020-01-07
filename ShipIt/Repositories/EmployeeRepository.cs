using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Npgsql;
using ShipIt.Models.ApiModels;
using ShipIt.Models.DataModels;

namespace ShipIt.Repositories
{
    public interface IEmployeeRepository
    {
        int GetCount();
        int GetWarehouseCount();
        EmployeeDataModel GetEmployeeByName(string name);
        IEnumerable<EmployeeDataModel> GetEmployeesByWarehouseId(int warehouseId);
        EmployeeDataModel GetOperationsManager(int warehouseId);
    }

    public class EmployeeRepository : RepositoryBase, IEmployeeRepository
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

        public EmployeeDataModel GetEmployeeByName(string name)
        {
            string sql = "SELECT name, w_id, role, ext FROM em WHERE name = @name";
            var parameter = new NpgsqlParameter("@name", name);
            string noProductWithIdErrorMessage = $"No employees found with name: {name}";
            return base.RunSingleGetQuery(sql, reader => new EmployeeDataModel(reader),noProductWithIdErrorMessage, parameter);
        }

        public IEnumerable<EmployeeDataModel> GetEmployeesByWarehouseId(int warehouseId)
        {

            string sql = "SELECT name, w_id, role, ext FROM em WHERE w_id = @w_id";
            var parameter = new NpgsqlParameter("@name", warehouseId);
            string noProductWithIdErrorMessage = $"No employees found with Warehouse Id: {warehouseId}";
            return base.RunGetQuery(sql, reader => new EmployeeDataModel(reader), noProductWithIdErrorMessage, parameter);
        }

        public EmployeeDataModel GetOperationsManager(int warehouseId)
        {

            string sql = $"SELECT name, w_id, role, ext FROM em WHERE w_id = @w_id AND role = @role";
            var parameters = new []
            {
                new NpgsqlParameter("@w_id", warehouseId),
                new NpgsqlParameter("@role", DataBaseRoles.OperationsManager)
            };

            string noProductWithIdErrorMessage = $"No employees found with Warehouse Id: {warehouseId}";
            return base.RunSingleGetQuery(sql, reader => new EmployeeDataModel(reader), noProductWithIdErrorMessage, parameters);
        }

        public void AddEmployees(IEnumerable<EmployeeDataModel> employees)
        {
            string sql = "INSERT INTO em (name, w_id, role, ext) VALUES(@name, @w_id, @role, @ext)";

            foreach (var employee in employees)
            {
                var parameters = employee.GetNpgsqlParameters().ToArray();
                RunQuery(sql, parameters);
            }
        }

        public void RemoveEmployee(string name)
        {
            string sql = "DELETE FROM em WHERE name = @name";
            var parameter = new NpgsqlParameter("@name", name);
            RunQuery(sql, parameter);
        }
    }
}