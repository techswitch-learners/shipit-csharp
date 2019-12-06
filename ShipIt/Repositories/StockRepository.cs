using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Npgsql;

namespace ShipIt.Repositories
{
    public interface IStockRepository
    {
        int GetTrackedItemsCount();
        int GetStockHeldSum();
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
    }
}