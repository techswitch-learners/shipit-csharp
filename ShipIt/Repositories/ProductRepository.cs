using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Npgsql;
using ShipIt.Controllers;
using ShipIt.Models.ApiModels;
using ShipIt.Models.DataModels;

namespace ShipIt.Repositories
{
    public interface IProductRepository
    {
        int GetCount();
        ProductApiModel GetProductByGtin(String gtin);
    }

    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public int GetCount()
        {
            string EmployeeCountSQL = "SELECT COUNT(*) FROM gcp";
            return (int) QueryForLong(EmployeeCountSQL);
        }

        public Product DatabaseReader(IDataReader dataReader)
        {
            return new Product()
            {

            };
        }

        public ProductApiModel GetProductByGtin(string gtin)
        {
            string sql = "SELECT p_id, gtin_cd, gcp_cd, gtin_nm, m_g, l_th, ds, min_qt FROM gtin WHERE gtin_cd = @gtin";
            var parameter = new NpgsqlParameter("@gtin", DbType.String)
            {
                Value = gtin
            };
            var x= base.QueryForProduct(sql, reader => new ProductDataModel(reader), parameter);
            return new ProductApiModel(x);
        }
    }
}