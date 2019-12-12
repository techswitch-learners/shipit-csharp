using System.Collections.Generic;
using Npgsql;
using ShipIt.Models.DataModels;

namespace ShipIt.Repositories
{
    public interface IStockRepository
    {
        int GetTrackedItemsCount();
        int GetStockHeldSum();
        IEnumerable<StockDataModel> GetStockByWarehouseId(int id);
    }

    public class StockRepository : RepositoryBase, IStockRepository
    {

        public int GetTrackedItemsCount()
        {
            string sql = "SELECT COUNT(*) FROM stock";
            return (int)QueryForLong(sql);
        }

        public int GetStockHeldSum()
        {
            string sql = "SELECT SUM(hld) FROM stock";
            return (int)QueryForLong(sql);
        }

        public IEnumerable<StockDataModel> GetStockByWarehouseId(int id)
        {
            string sql = "SELECT p_id, hld, w_id FROM stock WHERE w_id = @w_id";
            var parameter = new NpgsqlParameter("@w_id", id);
            string noProductWithIdErrorMessage = $"No stock found with w_id: {id}";
            return base.RunGetQuery(sql, reader => new StockDataModel(reader), noProductWithIdErrorMessage, parameter);
        }
    }
    
}