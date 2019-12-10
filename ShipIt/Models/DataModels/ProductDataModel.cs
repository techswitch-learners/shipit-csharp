using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ShipIt.Models.DataModels
{
    public class DatabaseColumnName : Attribute
    {
        public string Name { get; }

        public DatabaseColumnName(string name)
        {
            Name = name;
        }
    }


    public abstract class DataModel
    {
        protected DataModel(IDataReader dataReader)
        {
            var type = GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var attribute = (DatabaseColumnName)property.GetCustomAttributes(typeof(DatabaseColumnName), false).First();
                property.SetValue(this, dataReader[attribute.Name], null);
            }
        }
    }

    public class ProductDataModel : DataModel
    {
        [DatabaseColumnName("p_id")]
        public int Id { get; set; }

        [DatabaseColumnName("gtin_cd")]
        public string Gtin { get; set; }

        [DatabaseColumnName("gcp_cd")]
        public string Gcp { get; set; }

        [DatabaseColumnName("gtin_nm")]
        public string Name { get; set; }

        [DatabaseColumnName("m_g")]
        public double Weight { get; set; }

        [DatabaseColumnName("l_th")]
        public int LowerThreshold { get; set; }

        [DatabaseColumnName("ds")]
        public int Discontinued { get; set; }

        [DatabaseColumnName("min_qt")]
        public int MinimumOrderQuantity { get; set; }

        public ProductDataModel(IDataReader dataReader) : base(dataReader)
        { }
    }

}