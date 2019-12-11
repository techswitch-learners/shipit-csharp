using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Npgsql;
using ShipIt.Models.ApiModels;

namespace ShipIt.Models.DataModels
{
    public class EmployeeDataModel : DataModel
    {
        [DatabaseColumnName("name")]
        public string Name { get; set; }
        [DatabaseColumnName("w_id")]
        public int WarehouseId { get; set; }
        [DatabaseColumnName("role")]
        public string Role { get; set; }
        [DatabaseColumnName("ext")]
        public string Ext { get; set; }

        public EmployeeDataModel(IDataReader dataReader) : base(dataReader)
        { }

    }
}